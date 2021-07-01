using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingTeam4.ViewModels
{
    /// <summary>
    /// This class is to parse data from products and category table in view
    /// </summary>
    public class ViewProductCategoryS
    {
        //take ProductID, ProductName and CategoryName  all as string
        /// <summary>
        /// The ID of product
        /// </summary>
        public string ProductID { get; set; }   

        /// <summary>
        /// The name of product.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// The name of category.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Suppliers name
        /// </summary>
        public string SuppliersName { get; set; }

        /// <summary>
        /// Stock
        /// </summary>
        public string Stock { get; set; }

        /// <summary>
        /// On order
        /// </summary>
        public string OnOrder { get; set; }
        
        /// <summary>
        /// If this product is disponible this will be false
        /// </summary>
        public bool Discontinued { get; set; }
    }
}