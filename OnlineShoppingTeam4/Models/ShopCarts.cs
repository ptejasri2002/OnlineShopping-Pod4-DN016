namespace OnlineShoppingTeam4.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The entity that holds all the information from the ShopCarts table in the database.
    /// </summary>
    [Table("ShopCarts")]
    public partial class ShopCarts
    {
        /// <summary>
        /// The name of the user who added a product in the shopcart
        /// </summary>
        [Key]
        [StringLength(256)]
        [Column(Order = 1)]
        public string UserName { get; set; }

        /// <summary>
        /// The id of the product added.
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }

        /// <summary>
        /// The quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The products contained by the shopcart
        /// </summary>
        public virtual Products Products { get; set; }
    }
    [Table("WishLists")]
    public partial class WishLists
    {
        /// <summary>
        /// The name of the user who added a product in the shopcart
        /// </summary>
        [Key]
        [StringLength(256)]
        [Column(Order = 1)]
        public string UserName { get; set; }

        /// <summary>
        /// The id of the product added.
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }

        /// <summary>
        /// The quantity of the product.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The products contained by the shopcart
        /// </summary>
        public virtual Products Products { get; set; }
    }

    [Table("Transactions")]
    public partial class Transactions
    {
        /// <summary>
        /// The name of the user who added a product in the shopcart
        /// </summary>
        [Key]
       
        public int TransactionID { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        /// <summary>
        /// The id of the product added.
        /// </summary>
       
        public string Amount { get; set; }

        /// <summary>
        /// The quantity of the product.
        /// </summary>
        public string CardNo { get; set; }

        public string CardHolderName { get; set; }

        public string CVV { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        /// <summary>
        /// The products contained by the shopcart
        /// </summary>
        public virtual Transactions Transaction { get; set; }
    }
}