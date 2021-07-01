using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlineShoppingTeam4.Context;

namespace OnlineShoppingTeam4.Models.WishList
{
    /// <summary>
    /// contain basic info about shopcart
    /// </summary>
    public class ProductWishList
    {
        /// <summary>
        /// product id from shopcart
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// quantity of product
        /// </summary>
        public int Quantity { get; set; } = 1;
        /// <summary>
        /// total price
        /// </summary>
        public string TotalPrice { get; set; } 
        public string ProductName { get; set; }
        public string UnitPrice { get; set; } 
    }
    
    public class wishlistDetails
    {
     public   List<OnlineShoppingTeam4.Models.Shippers> shippers { get; set; }
     public List<OnlineShoppingTeam4.Models.WishList.ProductWishList> cart { get; set; }
    }

    /// <summary>
    /// ProductShopCartDetailed have aditional information about the product as against ProductShopCart
    /// </summary>
    public class ProductWishListDetailed : ProductWishList
    {

        /// <summary>
        /// Get category of this product
        /// </summary>
        public string ProductName
        {
            get
            {
                return (new OnlineShoppingTeam4Database()).Products.Find(ID).ProductName;
            }
            set
            {

            }
        }

        /// <summary>
        /// get unit price of this product
        /// </summary>
        public decimal UnitPrice
        {
            get
            {
                return (new OnlineShoppingTeam4Database()).Products.Find(ID).UnitPrice ?? 99999999;
            }
            set
            {

            }
        }

        /// <summary>
        /// get the category of this product
        /// </summary>
        public string Category
        {
            get
            {
                return (new OnlineShoppingTeam4Database()).Products.Find(ID).Category.CategoryName;
            }
            set
            {

            }
        }

    }
}