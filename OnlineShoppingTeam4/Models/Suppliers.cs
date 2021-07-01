namespace OnlineShoppingTeam4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that holds all the information from the Suppliers table in the database.
    /// </summary>
    public partial class Suppliers
    {

        /// <summary>
        /// Default constructor. Initialises new empty instances for Products.
        /// </summary>
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        /// <summary>
        /// The id of the supplier.
        /// </summary>
        [Key]
        public int SupplierID { get; set; }

        /// <summary>
        /// The name of the company who is the supplier.
        /// </summary>
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        /// <summary>
        /// The name of the contact person from the company.
        /// </summary>
        [StringLength(30)]
        public string ContactName { get; set; }

        /// <summary>
        /// The function of the contact person.
        /// </summary>
        [StringLength(30)]
        public string ContactTitle { get; set; }

        /// <summary>
        /// The address of the supplier.
        /// </summary>
        [StringLength(100)]
        public string Address { get; set; }

        /// <summary>
        /// The supplier's city.
        /// </summary>
        [StringLength(15)]
        public string City { get; set; }

        /// <summary>
        /// The supplier's region.
        /// </summary>
        [StringLength(15)]
        public string Region { get; set; }

        /// <summary>
        /// The supplier's postal code.
        /// </summary>
        [StringLength(10)]
        public string PostalCode { get; set; }

        /// <summary>
        /// The supplier's country.
        /// </summary>
        [StringLength(15)]
        public string Country { get; set; }

        /// <summary>
        /// The phone number of the supplier.
        /// </summary>
        [StringLength(24)]
        public string Phone { get; set; }

        /// <summary>
        /// The fax of the supplier.
        /// </summary>
        [StringLength(24)]
        public string Fax { get; set; }

        /// <summary>
        /// The supplier's web site.
        /// </summary>
        [Column(TypeName = "ntext")]
        public string HomePage { get; set; }

        [StringLength(24)]
        public string Email { get; set; }

        /// <summary>
        /// The supplier's city.
        /// </summary>
        [StringLength(50)]
        public string Password { get; set; }
        /// <summary>
        /// The products delivered by the supplier.
        /// </summary>
        public virtual ICollection<Products> Products { get; set; }
    }
}
