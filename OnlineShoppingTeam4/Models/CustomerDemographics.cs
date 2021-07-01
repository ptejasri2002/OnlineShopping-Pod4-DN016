namespace OnlineShoppingTeam4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that holds all the information from the CustomerDemographics table in the database.
    /// </summary>
    public partial class CustomerDemographics
    {

        /// <summary>
        /// Default constructor. Initialises new empty instances for Customers.
        /// </summary>
        public CustomerDemographics()
        {
            Customers = new HashSet<Customers>();
        }

        /// <summary>
        /// The id of the customer.
        /// </summary>
        [Key]
        [StringLength(10)]
        public string CustomerTypeID { get; set; }


        /// <summary>
        /// The description of the customer.
        /// </summary>
        [Column(TypeName = "ntext")]
        public string CustomerDesc { get; set; }

        /// <summary>
        /// The customers with demographics.
        /// </summary>
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
