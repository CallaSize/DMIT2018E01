using System;
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
        #region queries
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
        #endregion

        #region Add, Update, Delete
        public int Album_Add(Album item)
        {
            using (var context = new ChinookContext())
            {
                context.Albums.Add(item);   //staging
                context.SaveChanges();      //now committed
                return item.AlbumId;        //returns new id value
            }
        }
        public int Album_Update(Album item)
        {
            using (var context = new ChinookContext())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified; //staging
                return context.SaveChanges();      //now committed //returns # records changed

            }
        }
        public int Album_Delete(Album item)
        {
            using (var context = new ChinookContext())
            {

                var existing = context.Albums.Find(albumid);
                if (existing == null)
                {
                    throw new Exception("Album not on file. Delete unnecessary.");
                }
                else
                {
                    context.Albums.Remove(existing);   //staging
                    return context.SaveChanges();      //now committed //returns # records changed
                }
            }
        }

        #endregion
    }
}

