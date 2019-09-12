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
            ArtistList.Items.Insert(0, "Select...");

        }
    }
}