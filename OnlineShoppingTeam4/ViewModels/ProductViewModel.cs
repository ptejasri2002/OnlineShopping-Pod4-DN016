using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShoppingTeam4.Models;

namespace OnlineShoppingTeam4.ViewModels
{
    /// <summary>
    /// The entity that holds all the information from the products table in the database.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Default constructor. Initialises new empty instances for Orders Details.
        /// </summary>
        public ProductViewModel()
        {
            Order_Details = new HashSet<Order_Details>();
        }
        /// <summary>
        /// The id of the product.
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// The productname of the product
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// The id of the supplier.
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// The id of the category.
        /// </summary>
        public int CategoryID { get; set; }
        /// <summary>
        /// The QuantityPerUnit of the products.
        /// </summary>
        public string QuantityPerUnit { get; set; }
        /// <summary>
        /// The UnitPrice of the products
        /// </summary>
        public decimal? UnitPrice { get; set; }
        /// <summary>
        /// How many Units are in stock for this product
        /// </summary>
        public short? UnitsInStock { get; set; }
        /// <summary>
        /// How many Units are on orders
        /// </summary>
        public short? UnitsOnOrder { get; set; }
        /// <summary>
        /// when the quantity in stock reaches this level , a new order is sent.
        /// </summary>
        public short? ReorderLevel { get; set; }
        /// <summary>
        /// True if the product is available.
        /// </summary>
        public bool Discontinued { get; set; }
        /// <summary>
        /// The category of this product
        /// </summary>
        public virtual Categories Category { get; set; }
        /// <summary>
        /// A list containing all the order details that this product is a part of.
        /// </summary>
        public virtual ICollection<Order_Details> Order_Details { get; set; }
        /// <summary>
        /// The Suppliers of this product.
        /// </summary>
        public virtual Suppliers Supplier { get; set; }
        /// <summary>
        /// The image of this product.
        /// </summary>
        public HttpPostedFileBase ProductImage { get; set; }
    }
}
