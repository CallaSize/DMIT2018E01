using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ChinookSystem.Data.Entities
{
    [Table("Artists")]
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        private string _Name;
        public string Name { // example of a fully-implemented property
            get
            {
                return _Name;
            }
             set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Name = null;
                }
                else
                {
                    //if it does have something then I'll actually store the value.
                    _Name = value;
                }//eof (end of if) also try eol end of loop
            }
        }//eop(end of property)

        //navigational properties
        public virtual ICollection<Album> Albums { get; set; }


    }//eoc (end of class)
}//eon (end of namespace)
