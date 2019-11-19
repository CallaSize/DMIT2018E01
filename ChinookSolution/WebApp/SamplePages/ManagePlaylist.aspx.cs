using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using DMIT2018Common.UserControls;
using WebApp.Security;
//using WebApp.Security;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
           if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Customers") || User.IsInRole("Customer Service"))
                {
                    //to get the customernumber im going to have to go to my security controller, call the method, what this needs as input is my username
                    var username = User.Identity.Name;
                    SecurityController securitymgr = new SecurityController();
                    //first off, the ids are nullable ints, you cannot just pull it into an int, you have to pull it into a nullable int and then owkr on it from there
                    int? customerid = securitymgr.GetCurrentUserCustomerId(username);
                    //my customer controller needs a real int not a nullable int, so lets check if we got a number coming back, what if it is the employee that is trying to look things up
                    if (customerid.HasValue)
                    {
                        //if the customer number does exist then you haVE TO go and get the customer name, but we need error handling
                        MessageUserControl.TryRun(() => 
                        {
                            //do a standard lookup
                            CustomerController sysmgr = new CustomerController();
                            Customer info = sysmgr.Customer_Get(customerid.Value);
                            CustomerName.Text = info.FullName;

                        });
                    }
                    else
                    {
                        MessageUserControl.ShowInfo("unregistered user", "this user is not a registered customer");
                        CustomerName.Text = "Unregistered User";
                    }

                }
                else
                {
                    //redirect to a page that states no authorization fot the request action
                    Response.Redirect("~/Security/AccessDenied.aspx");
                }
            }
            else
            {
                //redirect to login page
                Response.Redirect("~/Account/Login.aspx");
            }

        }

        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void ArtistFetch_Click(object sender, EventArgs e)
        {

            //first thing we probably want to do is validation, if we don't have that already on the form.
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                //using MessageUserControl to display a message
                MessageUserControl.ShowInfo("You done f*cked up", "Enter a partial artist name.");
            }
            else
            {
                //do a lookup by the artist name and bind our code.
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = ArtistName.Text;
                    TracksBy.Text = "Artist";
                    TracksSelectionList.DataBind(); //this line causes the ODS to execute. So if I need to do it again, then I can just do a data bind.
                },"Track Search", "Select from the following list to add to your playlist.");
            }

          }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {

            //we don't need to test to see if ther eis something that is needed becaus eit is a drop down list. 
            //if you put a prompt line, you would have to check for it, but he didn't add one so no check is necessary
            MessageUserControl.TryRun(() =>
            {
                SearchArg.Text = MediaTypeDDL.SelectedValue;
                TracksBy.Text = "MediaType";
                TracksSelectionList.DataBind(); //this line causes the ODS to execute. So if I need to do it again, then I can just do a data bind.
            }, "Track Search", "Select from the following list to add to your playlist.");


        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            //we don't need to test to see if ther eis something that is needed becaus eit is a drop down list. 
            //if you put a prompt line, you would have to check for it, but he didn't add one so no check is necessary
            MessageUserControl.TryRun(() =>
            {
                SearchArg.Text = GenreDDL.SelectedValue;
                TracksBy.Text = "Genre";
                TracksSelectionList.DataBind(); //this line causes the ODS to execute. So if I need to do it again, then I can just do a data bind.
            }, "Track Search", "Select from the following list to add to your playlist.");

        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            //first thing we probably want to do is validation, if we don't have that already on the form.
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                //using MessageUserControl to display a message
                MessageUserControl.ShowInfo("Missing Data", "Enter a partial album name.");
            }
            else
            {
                //do a lookup by the album name and bind our code.
                MessageUserControl.TryRun(() =>
                {
                    SearchArg.Text = AlbumTitle.Text;
                    TracksBy.Text = "Album";
                    TracksSelectionList.DataBind(); //this line causes the ODS to execute. So if I need to do it again, then I can just do a data bind.
                }, "Track Search", "Select from the following list to add to your playlist.");
            }

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //verify if the data is there
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Required Data", "Play list name is required.");
            }
            else
            {
                //call BLL
                string playlistname = PlaylistName.Text;
                //until we do security, we will use a hard coded username
                //string username = "HansenB"; once, security is implemented, you can obtain the user name from user.identity class property .name
                string username = User.Identity.Name;
                //do a standard query lookup to your controller, use MessageUserControl for error handling
                MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = datainfo; //if nothing is coming back it will show the template
                    PlayList.DataBind();
                }, "Playlist Tracks", "See current tracks on playlist below"
                );
            }
 
        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();
            //is there a playlist?
            //no? then msg
            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("There is no playlist present.");
            }

                //yes then is there a playlistname?
                //no  then msg
                if (string.IsNullOrEmpty(PlaylistName.Text))
                {
                    reasons.Add("You must have a playlist name");
                }
                int trackid = 0;
                int tracknumber = 0;
                int rowsSelected = 0;
                CheckBox playlistselection = null;  //?CheckBox?

            //yes then traverse playlist to collect selected row(s)
            //if > 1 selected? 
            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //access the control on the indexed GRidView row
                //set the checkbox pointer to this checkbox control
                playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistselection.Checked)
                {
                    //increase selected number of rows
                    rowsSelected++;
                    //gather the data neeeded for the BLL call
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label ).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);

                }
            }
            if (rowsSelected != 1)
            {
                reasons.Add("Select only 1 track to move.");
            }
            if (tracknumber ==PlayList.Rows.Count)
            {
                reasons.Add("Last track cannot be moved down.");
            }

            // bad msg
            //check if last track.
            // bad msg
            // validation good no: display all errors, yes:then move track
            if (reasons.Count ==0)
            {
                MoveTrack(trackid, tracknumber, "down");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    throw new BusinessRuleException("Track Move Errors:", reasons);
                });
            }


        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            List<string> reasons = new List<string>();

            if (PlayList.Rows.Count == 0)
            {
                reasons.Add("There is no playlist present.");
            }

            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                reasons.Add("You must have a playlist name");
            }
            int trackid = 0;
            int tracknumber = 0;
            int rowsSelected = 0;
            CheckBox playlistselection = null;  //?CheckBox?

            for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
            {
                //access the control on the indexed GRidView row
                //set the checkbox pointer to this checkbox control
                playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                if (playlistselection.Checked)
                {
                    //increase selected number of rows
                    rowsSelected++;
                    //gather the data neeeded for the BLL call
                    trackid = int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text);
                    tracknumber = int.Parse((PlayList.Rows[rowindex].FindControl("TrackNumber") as Label).Text);

                }
            }
            if (rowsSelected != 1)
            {
                reasons.Add("Select only 1 track to move.");
            }
            if (tracknumber == 1)
            {
                reasons.Add("Last track cannot be moved up.");
            }
            if (reasons.Count == 0)
            {
                MoveTrack(trackid, tracknumber, "up");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    throw new BusinessRuleException("Track Move Errors:", reasons);
                });
            }

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
            MessageUserControl.TryRun(()=> {
                PlaylistTracksController sysmgr = new PlaylistTracksController();
                sysmgr.MoveTrack(User.Identity.Name, PlaylistName.Text, trackid, tracknumber, direction);
                List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
                PlayList.DataSource = datainfo; //if nothing is coming back it will show the template
                PlayList.DataBind();
            },"Success","Track has been moved");
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //do we have the playlist name
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                //if we don't...
                MessageUserControl.ShowInfo("Required Data", "You need a play list name to add a track.");
            }
            else
            {
                //have a playlist?
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Required Data", "No play list is available. Retrieve your playlist");
                }
                else
                {
                    //traverse the grid view and collect the list of tracks to remove
                    List<int> trackstodelete = new List<int>();
                    int rowselected = 0;
                    CheckBox playlistselection = null;
                    for (int rowindex = 0; rowindex < PlayList.Rows.Count; rowindex++)
                    {
                        //access the checkbox control on teh indexed Gridviewrow
                        //set the checkbox pointer to this checkbox control
                        playlistselection = PlayList.Rows[rowindex].FindControl("Selected") as CheckBox;
                        if (playlistselection.Checked)
                        {
                            //increase selected nnumber of rows
                            rowselected++;
                            //gather the data needed for the BLL call
                            trackstodelete.Add(int.Parse((PlayList.Rows[rowindex].FindControl("TrackID") as Label).Text));
                        }
                    }
                    if (rowselected==0)
                    {
                        MessageUserControl.ShowInfo("Required Data","You must select as least one track to remove");
                    }
                    else
                    {
                        //send list of tracks to be removed by BLL
                        MessageUserControl.TryRun(() =>
                        {
                            PlaylistTracksController sysmgr = new PlaylistTracksController();
                            //there is ONLY one call to add the data to the database.
                            sysmgr.DeleteTracks(User.Identity.Name,PlaylistName.Text, trackstodelete);
                            //the REFRESH of the playlist is a READ.
                            List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(PlaylistName.Text, User.Identity.Name);
                            PlayList.DataSource = datainfo;
                            PlayList.DataBind();
                        }, "Deleting a Track", "Success! Track has been removed from playlist!");
                    }
                }
            }


        }

        //this is different than a straight click. Notice the CommandEventArgs parameter.
        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //do we have the playlist name
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                //if we don't...
                MessageUserControl.ShowInfo("Required Data","You need a play list name to add a track.");
            }
            else
            {
                //collect the required data for the event
                string playlistname = PlaylistName.Text;
                //the user name will come from the form security, so instead we will just use a hardcoded string until we add the security
                //string username = "HansenB";
                string username = User.Identity.Name;
                //obtain the track id from the ListView. 
                //the track ID will be in the CommandArg property of the ListViewCommandEventArgs e instance
                int trackid = int.Parse(e.CommandArgument.ToString()); //<-- what comes back is actually an object, so we need to type cast this thing.
                                                                       //the Commandarg in e is returned as an object. Cast it to a string, then you can Parse the string.

                //using the obtained data, issue your  call to the BLL method, this work will be done within a TryRun
                MessageUserControl.TryRun(() => 
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //there is ONLY one call to add the data to the database.
                    sysmgr.Add_TrackToPLaylist(playlistname, username, trackid);
                    //refresh the playlist, and we already have the method to do that, so we are borrowing from our playlist fetch
                    //the REFRESH of the playlist is a READ.
                    List<UserPlaylistTrack> datainfo = sysmgr.List_TracksForPlaylist(playlistname, username);
                    PlayList.DataSource = datainfo;
                    PlayList.DataBind();
                },"Adding a Track","Success! Track added to playlist!");
            }
            
        }

    }
}