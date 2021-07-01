namespace OnlineShoppingTeam4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that holds all the information from the Order_Details table in the database.
    /// </summary>
    [Table("Order Details")]
    public partial class Order_Details
    {
        /// <summary>
        /// The order id of the order-detail.
        /// </summary>
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        /// <summary>
        /// The product id of the order-detail.
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        /// <summary>
        /// The unit price of the product.
        /// </summary>
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The product's quantity.
        /// </summary>
        public short Quantity { get; set; }

        /// <summary>
        /// The product's discount.
        /// </summary>
        public float Discount { get; set; }

        /// <summary>
        /// The order of the order-detail.
        /// </summary>
        public virtual Orders Order { get; set; }

        /// <summary>
        /// The product of the order-detail.
        /// </summary>
        public virtual Products Product { get; set; }
    }
}
