using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.POCOs;
//using WebApp.Security;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
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
                string username = "HansenB";
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
            //code to go here
 
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
 
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here
 
        }

        protected void TracksSelectionList_ItemCommand(object sender, 
            ListViewCommandEventArgs e)
        {
            //code to go here
            
        }

    }
}