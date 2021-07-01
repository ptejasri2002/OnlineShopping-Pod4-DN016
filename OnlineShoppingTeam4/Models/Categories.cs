namespace OnlineShoppingTeam4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that contains information about categories, located in database.
    /// </summary>
    public partial class Categories
    {
        /// <summary>
        /// Default constructor. Initialises new empty instances for Products.
        /// </summary>
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        /// <summary>
        /// The ID through which we find the category.
        /// </summary>
        [Key]
        public int CategoryID { get; set; }

        /// <summary>
        /// The name through which we find the category.
        /// </summary>
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }

        /// <summary>
        /// The description through which we find the category.
        /// </summary>
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        /// <summary>
        /// A category may contains more products.
        /// </summary>
        public virtual ICollection<Products> Products { get;  }
    }
}
