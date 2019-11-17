using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.DTOs;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
using ChinookSystem.BLL;
//using DMIT2018Common.UserControls;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                //what would happen if there is no match for the incoming parameter value, we need to ensure that the results have a valid value, this value will need to 
                //resolve to either a null or an IEnumerable<T> collection
                // to achive a valid value, you will need to determine using .FirstOrDefault() whether data exists or naaaaahtt
                //before I go grab the results, does the playlist exist??????
                var results = (from x in context.Playlists
                               where x.UserName.Equals(username) && x.Name.Equals(playlistname)
                               select x).FirstOrDefault();
                //if the playlist does NOT exist, .FirstorDefault returns null
                if (results==null)
                {
                    return null;
                }
                else
                {
                    //if the playlist does exist, query for the playlist tracks
                    var theTracks = from x in context.PlaylistTracks
                                    where x.PlaylistId.Equals(results.PlaylistId)
                                    orderby x.TrackNumber
                                    select new UserPlaylistTrack
                                    {
                                        TrackID = x.TrackId,
                                        TrackNumber = x.TrackNumber,
                                        TrackName = x.Track.Name,
                                        Milliseconds = x.Track.Milliseconds,
                                        UnitPrice = x.Track.UnitPrice
                                    };
                    return theTracks.ToList();
                }                              
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookContext())
            {
                //use the BusinessRuleExecption to throw errors to the webpage. Local list of all the errors to throw at end.
                List<string> reasons = new List<string>();
                PlaylistTrack newTrack = null;
                int tracknumber = 0;

                //Part One
                //determine if the playlist exists
                //query the table using the playlistname and username
                //if the playlist exists, one will get a record.
                //if the playlist does not exist, one will get a null.
                //to ensure these results, the query will be wrapped in a .FirstOrDefault() <-- return the first occurence of what it finds, or if it doesn't find, it returns null value.
                //Playlist exists = context.Playlists
                //                    .Where(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                //                    && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase))
                //                    .Select(x => x)
                //                    .FirstOrDefault();
                //now see in query-syntax:
                Playlist exists = (from x in context.Playlists
                                   where x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                                         && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)
                                   select x).FirstOrDefault();

                //does the playlist exist?
                if (exists==null)
                {
                    //this is a new playlist so i need to create the playlist record
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    //stage the add
                    exists = context.Playlists.Add(exists);
                    //since this is a new playlist the track number will be equal to 1
                    tracknumber = 1;
                }
                else
                {
                    //since the playlist exists, so may the track exist on the playlisttracks
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if (newTrack == null)
                    {
                        tracknumber = exists.PlaylistTracks.Count() + 1;
                    }
                    else
                    {
                        reasons.Add("Track already exists on the playlist");
                    }

                }
                //Part two
                //create the playlistTrack entry
                //if there are any reasons not to create then, throw the businessRuleExceptio
                if (reasons.Count() > 0)
                {
                    //issue with adding the track
                    throw new BusinessRuleException("Adding track to playlist", reasons);
                }
                else
                {
                    //use the Playlist navigation to PlaylistTracks to do the add to the PlaylistTracks
                    newTrack = new PlaylistTrack();
                    newTrack.TrackId = trackid;
                    newTrack.TrackNumber = tracknumber;
                    //how do i fill the playlist id? if the playlist is brandnew?! What's a girl gonna do? A brand new playlist, does not yet have an id!
                    //heres the secret NOTE: tthe pkey for PlaylistID may not yet exist. using the nvaigational property on the PlayList entity, one can
                    //let HashSet handle the PlaylistId pkey value to be properly create on PlaList adnplaced correctly in the "child" record of PlaylistTracks
                    //what is wrong is to attempt this:
                    //newTrack.PlaylistId = exists.PlaylistId
                    exists.PlaylistTracks.Add(newTrack); // playlist track staging

                    //physically add any/all data to the database
                    //commit
                    context.SaveChanges();
                }


            }
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookContext())
            {
                //get playlist id
                var exists = (from x in context.Playlists
                              where x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                              && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)
                              select x).FirstOrDefault();
                //validation that is going to be coded as if then elses - one error at a time -
                if (exists == null)
                {
                    throw new Exception("Playlist does not exist");
                }
                else
                {
                    PlaylistTrack moveTrack = (from x in exists.PlaylistTracks
                                               where x.TrackId == trackid
                                               select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        throw new Exception("Playlist track does not exist");
                    }
                    else
                    {
                        PlaylistTrack otherTrack = null;
                        //up or down
                        if (direction.Equals("up"))
                        {
                            //up
                            if (tracknumber==1)
                            {
                                throw new Exception("Track 1 cannot be moved up");
                            }
                            else
                            {
                                //find the other track
                                otherTrack = (from x in exists.PlaylistTracks
                                                where x.TrackNumber == moveTrack.TrackNumber - 1
                                                select x).FirstOrDefault();
                                if (otherTrack ==null)
                                {
                                    //someone has really been messing with you!
                                    throw new Exception("Playlist is corrupt. Fetch playlist again");
                                }
                                else
                                {
                                    moveTrack.TrackNumber -= 1;
                                    otherTrack.TrackNumber += 1;
                                    //this has nothing to do with the database!!!!! it's all about the display.
                                }
                            }

                        }
                        else
                        {
                            //down
                            if (tracknumber == exists.PlaylistTracks.Count)
                            {
                                throw new Exception("Last Track cannot be moved down");
                            }
                            else
                            {
                                //find the other track
                                otherTrack = (from x in exists.PlaylistTracks
                                              where x.TrackNumber == moveTrack.TrackNumber + 1
                                              select x).FirstOrDefault();
                                if (otherTrack == null)
                                {
                                    //someone has really been messing with you!
                                    throw new Exception("Playlist is corrupt. Fetch playlist again");
                                }
                                else
                                {
                                    moveTrack.TrackNumber += 1;
                                    otherTrack.TrackNumber -= 1;
                                    //this has nothing to do with the database!!!!! it's all about the display.
                                }
                            }

                        }//eof up or down
                        //staging
                        context.Entry(moveTrack).Property(y => y.TrackNumber).IsModified = true;
                        context.Entry(otherTrack).Property(y => y.TrackNumber).IsModified = true;
                        //commit
                        context.SaveChanges();
                    }
                }

            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookContext())
            {
                //playlist exists?
                var exists = (from x in context.Playlists where x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase) select x).FirstOrDefault();
                if (exists == null)
                {
                    //no: message
                    throw new Exception("Playlist has been removed from the system");
                }
                else
                {

                    //yes: create a list of playlisttracks that are to be kept.
                    List<PlaylistTrack> trackskept = exists.PlaylistTracks
                                    .Where(tr => !trackstodelete.Any(tod => tr.TrackId == tod))
                                    .Select(tr => tr)
                                    .ToList();
                    //       stage the removal of tracks
                    PlaylistTrack item = null;
                    foreach (var dtrackid in trackstodelete)
                    {
                        item = exists.PlaylistTracks
                            .Where(tr => tr.TrackId == dtrackid).FirstOrDefault();
                        if (item != null)
                        {
                            //stage it
                            exists.PlaylistTracks.Remove(item);

                        }
                    }
                    //       renumbering all of kept tracks and stage update. 
                    int number = 1;
                    trackskept.Sort((x, y) => x.TrackNumber.CompareTo(y.TrackNumber));
                    foreach (var tkept in trackskept)
                    {
                        tkept.TrackNumber = number;
                        context.Entry(tkept).Property(y => y.TrackNumber).IsModified = true; //field update, not the entity update
                        number++;

                    }
                    //       commit
                    context.SaveChanges();
                }

            }
        }//eom
    }
}
