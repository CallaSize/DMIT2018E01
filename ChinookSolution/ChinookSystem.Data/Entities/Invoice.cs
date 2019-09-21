namespace ChinookSystem.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            InvoiceLines = new HashSet<InvoiceLine>();
        }

        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        [StringLength(70, ErrorMessage = "Billing Address is limited to 70 characters")]
        public string BillingAddress { get; set; }

        [StringLength(40, ErrorMessage = "BillingCity is limited to 40 characters")]
        public string BillingCity { get; set; }

        [StringLength(40, ErrorMessage = "Billing State is limited to 40 characters")]
        public string BillingState { get; set; }

        [StringLength(40, ErrorMessage = "Billing Country is limited to 40 characters")]
        public string BillingCountry { get; set; }

        [StringLength(10, ErrorMessage = "Billing Postal Code is limited to 10 characters")]
        public string BillingPostalCode { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Total { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
