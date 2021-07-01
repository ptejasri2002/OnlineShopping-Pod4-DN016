using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingTeam4.Models.ServerClientCommunication
{

    /// <summary>
    ///  Contain data of categories that datatable need to draw a table.
    /// </summary>
    public class CategoriesData
    {
        /// <summary>
        /// The ID of category.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of category.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// The description of category.
        /// </summary>
        public string Description { get; set; }
    }
}