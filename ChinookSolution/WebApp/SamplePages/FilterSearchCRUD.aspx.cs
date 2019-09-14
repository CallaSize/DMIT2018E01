﻿using System;
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

        protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //standard lookup
            GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
            //retrieve the value from a web control located within the Grid View cell
            string albumid = (agvrow.FindControl("AlbumId") as Label).Text;
            //now we actually have to do the lookup, for the time being error handling will need to be added.
            AlbumController sysmgr = new AlbumController();
            Album datainfo = sysmgr.Album_Get(int.Parse(albumid));
            if(datainfo==null)
            {
                //clear the controls
                //throw an exception
            }
            else
            {
                EditAlbumID.Text = datainfo.AlbumId.ToString();
                EditTitle.Text = datainfo.Title;
                EditAlbumArtistList.SelectedValue = datainfo.ArtistId.ToString();
                EditReleaseYear.Text = datainfo.ReleaseYear.ToString();
                EditReleaseLabel.Text = datainfo.ReleaseLabel == null ? "" : datainfo.ReleaseLabel;
            }

        }
    }
}