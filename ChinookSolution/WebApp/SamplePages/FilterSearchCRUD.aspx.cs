using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.Data.Entities;
#endregion

namespace WebApp.SamplePages
{
    public partial class FilterSearchCRUD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                BindArtistList();
                //set the max value for the validation control: RangeEditReleaseYear
                RangeEditReleaseYear.MaximumValue = DateTime.Today.Year.ToString();
            }
        }

        protected void BindArtistList()
        {
            //connect to your controller class
            ArtistController sysmgr = new ArtistController();
            List<Artist> info = sysmgr.Artist_List();
            info.Sort((x,y) => x.Name.CompareTo(y.Name));
            //if you want descending, you must swap the y.name.compareto(x.Name)...
            ArtistList.DataSource = info;
            ArtistList.DataTextField = nameof(Artist.Name);
            ArtistList.DataValueField = nameof(Artist.ArtistId); //this nameof thingy helps you with spelling
            ArtistList.DataBind();
            //ArtistList.Items.Insert(0, "Select...");

        }

        //in code behind, to be called from ODS
        protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //standard lookup
            GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
            //retrieve the value from a web control located within the Grid View cell
            string albumid = (agvrow.FindControl("AlbumId") as Label).Text;
            //now we actually have to do the lookup, for the time being error handling will need to be added.
            MessageUserControl.TryRun(() =>
               {
                   AlbumController sysmgr = new AlbumController();
                   Album datainfo = sysmgr.Album_Get(int.Parse(albumid));
                   if (datainfo == null)
                   {
                       //clear the controls
                       ClearControls();
                       //throw an exception
                       throw new Exception("Record no longer exists on file.");
                    }
                   else
                   {
                       EditAlbumID.Text = datainfo.AlbumId.ToString();
                       EditTitle.Text = datainfo.Title;
                       EditAlbumArtistList.SelectedValue = datainfo.ArtistId.ToString();
                       EditReleaseYear.Text = datainfo.ReleaseYear.ToString();
                       EditReleaseLabel.Text = datainfo.ReleaseLabel == null ? "" : datainfo.ReleaseLabel;
                   }

           },"Find Album","Album found."); //strings on this line are a success message

        }

        protected void ClearControls()
        {
            EditAlbumID.Text = "";
            EditTitle.Text = "";
            EditReleaseYear.Text = "";
            EditReleaseLabel.Text = "";
            EditAlbumArtistList.SelectedIndex = 0;
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string albumtitle = EditTitle.Text;
                int albumyear = int.Parse(EditReleaseYear.Text);
                string albumlabel = EditReleaseLabel.Text == "" ? 
                    null : EditReleaseLabel.Text;
                int albumartist = int.Parse(EditAlbumArtistList.SelectedValue);

                //now put them inside an instance.
                Album theAlbum = new Album();
                theAlbum.Title = albumtitle;
                theAlbum.ArtistId = albumartist;
                theAlbum.ReleaseYear = albumyear;
                theAlbum.ReleaseLabel = albumlabel;

                MessageUserControl.TryRun(() => 
                {
                    AlbumController sysmgr = new AlbumController();
                    int albumid = sysmgr.Album_Add(theAlbum);
                    EditAlbumID.Text = albumid.ToString();
                    if (AlbumList.Rows.Count > 0)
                    {
                        AlbumList.DataBind(); // reexecute the ODS for my albumlist.
                    }
                }, "Successful","Album Added");

            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {

        }

        protected void Remove_Click(object sender, EventArgs e)
        {

        }
    }
}