﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{   [DataObject]
    public class AlbumController
    {
        public List<Album> Album_List()
        {
            using (var context = new ChinookSystem.DAL.ChinookContext())
            {
                return context.Albums.ToList();
            }
        }

        public Album Album_Get(int albumid)
        {
            using (var context = new ChinookContext())
            {
                return context.Albums.Find(albumid);
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Album_GetByArtist(int artistid)
        {
            using (var context = new ChinookContext())
            {
                //1st Linq query example. We are going to be using our navigational properties.
                var results = from x in context.Albums
                              where x.ArtistId == artistid
                              select x;
                //throw new Exception("Boom!");
                return results.ToList();

            }
                                
            
            
        }
    }
}

