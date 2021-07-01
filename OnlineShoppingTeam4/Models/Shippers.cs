namespace OnlineShoppingTeam4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that contains information about shippers, located in database.
    /// </summary>
    public partial class Shippers
    {

        /// <summary>
        ///  Default constructor. Initialises new empty instances for Orders.
        /// </summary>
        public Shippers()
        {
            Orders = new HashSet<Orders>();
        }


        /// <summary>
        /// The ID through which we find the shipper.
        /// </summary>
        [Key]
        public int ShipperID { get; set; }

        /// <summary>
        /// The CompanyName through which we find the shipper.
        /// </summary>
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        /// <summary>
        /// The Phone through which we find the shipper.
        /// </summary>
        [StringLength(24)]
        public string Phone { get; set; }


        /// <summary>
        /// The shipper which contains more orders.
        /// </summary>
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
