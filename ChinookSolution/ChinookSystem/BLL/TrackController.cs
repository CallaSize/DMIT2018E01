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
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_List()
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Track_Find(int trackid)
        {
            using (var context = new ChinookContext())
            {
                return context.Tracks.Find(trackid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Track> Track_GetByAlbumId(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var results = from aRowOn in context.Tracks
                              where aRowOn.AlbumId.HasValue
                              && aRowOn.AlbumId == albumid
                              select aRowOn;
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
            using (var context = new ChinookContext())
            {


                //we have to consider that there is a string value and the type for the artist
                //for album we also have a string and the type so when we do teh lookup, my where clause, value contains....whatever my string is
                //and of a table type= x , but we can't do that with the genre or the media type, because even though it comes in as a string, I need an int and tabletype
                //the where clause will be similar pkey=int and the table type =p and we will have another one, pkey=int and tabletype =k.
                //probelm is that we want 1 return with 2 different reports. 

                //check the incoming parameters and if needed set to a default value, ODS's fire when your page comes up
                if (string.IsNullOrEmpty(tracksby))
                {
                    tracksby = ""; //mainly to catch the null situation
                }
                if (string.IsNullOrEmpty(arg))
                {
                    arg = "";
                } //now we know we  have an object, this is not going to be empty.
                //next, we set these values to a particular item (id/string)
                //create 2 local variables representing the argument as a) an integer b) as a string
                int argid = 0;
                string argstring = "zyxzz"; //<--selected something that would likely never exist as my default value
                //determine if incoming argument should be integer or string
                if (tracksby.Equals("Genre") || tracksby.Equals("MediaType"))
                {
                    argid = int.Parse(arg);
                }
                else
                {
                    argstring = arg.Trim();
                }
                var results = (from x in context.Tracks
                               where (  x.GenreId == argid && tracksby.Equals("Genre"))
                                        || (x.MediaTypeId == argid && tracksby.Equals("MediaType"))
                               select new TrackList
                               {
                                   TrackID = x.TrackId,
                                   Name = x.Name,
                                   Title = x.Album.Title,
                                   ArtistName = x.Album.Artist.Name,
                                   MediaName = x.MediaType.Name,
                                   GenreName = x.Genre.Name,
                                   Composer = x.Composer,
                                   Milliseconds = x.Milliseconds,
                                   Bytes = x.Bytes,
                                   UnitPrice = x.UnitPrice
                               }
                               ).Union(
                                        from x in context.Tracks
                                        //going to change the style in which we ask this, so now the ternary operator is up!
                                        where tracksby.Equals("Artist") ? x.Album.Artist.Name.Contains(argstring) : 
                                                tracksby.Equals("Album") ? x.Album.Title.Contains(argstring) : false
                                                  
                                        select new TrackList
                                        {
                                            TrackID = x.TrackId,
                                            Name = x.Name,
                                            Title = x.Album.Title,
                                            ArtistName = x.Album.Artist.Name,
                                            MediaName = x.MediaType.Name,
                                            GenreName = x.Genre.Name,
                                            Composer = x.Composer,
                                            Milliseconds = x.Milliseconds,
                                            Bytes = x.Bytes,
                                            UnitPrice = x.UnitPrice
                                        }
                                    );

                return results.ToList();
            }
        }//eom

       
    }//eoc
}
