namespace ChinookSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(40,ErrorMessage = "First Name is limited to 40 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(20, ErrorMessage = "Last Name is limited to 20 characters")]
        public string LastName { get; set; }

        [StringLength(80, ErrorMessage = "Company is limited to 80 characters")]
        public string Company { get; set; }

        [StringLength(70, ErrorMessage = "Address is limited to 70 characters")]
        public string Address { get; set; }

        [StringLength(40, ErrorMessage = "City is limited to 40 characters")]
        public string City { get; set; }

        [StringLength(40, ErrorMessage = "State is limited to 40 characters")]
        public string State { get; set; }

        [StringLength(40, ErrorMessage = "Country is limited to 40 characters")]
        public string Country { get; set; }

        [StringLength(10, ErrorMessage = "PostalCode is limited to 10 characters")]
        public string PostalCode { get; set; }

        [StringLength(24, ErrorMessage = "Phone is limited to 24 characters")]
        public string Phone { get; set; }

        [StringLength(24, ErrorMessage = "Fax is limited to 24 characters")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(60, ErrorMessage = "Email is limited to 60 characters")]
        public string Email { get; set; }

        public int? SupportRepId { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }

        }
    }
}
