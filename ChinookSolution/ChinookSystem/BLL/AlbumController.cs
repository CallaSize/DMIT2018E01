using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Data.Entities;
using System.ComponentModel;
using DMIT2018Common.UserControls;
using ChinookSystem.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{   [DataObject]
    public class AlbumController
    {
        #region Class Variables
        private List<string> reasons = new List<string>();
        #endregion
        #region queries

        [DataObjectMethod(DataObjectMethodType.Select, false)]
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
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<AlbumsOfArtist> Album_AlbumsOfArtist(string artistname)
        {
            using (var context = new ChinookContext())
            {
                //unlike Linqpad which is Linq to SQL, within our application, it is Linq to Entities.
                var results = from x in context.Albums
                              where x.Artist.Name.Contains(artistname)
                              orderby x.ReleaseYear, x.Title
                              select new AlbumsOfArtist
                              {
                                  Title = x.Title,
                                  ArtistName = x.Artist.Name,
                                  RYear = x.ReleaseYear,
                                  Label = x.ReleaseLabel
                              };
                return results.ToList();
            }
        }


        #endregion


        #region Add, Update, Delete
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public int Album_Add(Album item)
        {
            using (var context = new ChinookContext())
            {
                if (CheckReleaseYear(item))
                {                    
                    context.Albums.Add(item);   //staging
                    context.SaveChanges();      //now committed
                    return item.AlbumId;        //returns new id value
                }
                else
                {
                    throw new BusinessRuleException("Validation Error", reasons);
                }
            }
        }
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Album_Update(Album item)
        {
            using (var context = new ChinookContext())
            {
                if (CheckReleaseYear(item))
                {
                    context.Entry(item).State = System.Data.Entity.EntityState.Modified; //staging
                    return context.SaveChanges();      //now committed //returns # records changed
                }
                else
                {
                    throw new BusinessRuleException("Validation Error", reasons);
                }

            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public int Album_Delete(Album item)
        {
            return Album_Delete(item.AlbumId);
        }


        public int Album_Delete(int albumid)
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
        #region Support Methods
        private bool CheckReleaseYear(Album item)
        {
            bool isValid = true;
            int releaseYear;
            if (string.IsNullOrEmpty(item.ReleaseYear.ToString()))
            {
                isValid = false;
                reasons.Add("Release Year is required.");
            }
            //this is just an example that we don't neeD:
            else if (!int.TryParse(item.ReleaseYear.ToString(), out releaseYear))
            {
                isValid = false;
                reasons.Add("Release Year is not a number.");
            }
            else if (releaseYear<1950 || releaseYear> DateTime.Today.Year)
            {
                isValid = false;
                reasons.Add(string.Format("Album release year of {0} invalid. Must be between 1950 and today", releaseYear));
            }
            return isValid;
        }
        #endregion
    }
}

