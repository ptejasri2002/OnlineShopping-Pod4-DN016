using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineShoppingTeam4.Models.WishList
{
    /// <summary>
    /// Contains the response for shopcart from server
    /// </summary>
    /// <typeparam name="DataType">IQueriable if we have a proper database or string of json from another database</typeparam>
    public class ProductShopResponse<DataType>
    {
        /// <summary>
        /// contains all errors
        /// </summary>
        public enum  ErrorType
        {
            /// <summary>
            /// no error found
            /// </summary>
            NoError,
            /// <summary>
            /// a unknown type of error ocure
            /// </summary>
            UnknownError,
            /// <summary>
            /// order contains some invalid data
            /// </summary>
            InvalidOrder
        }

        /// <summary>
        /// true if server cannot send back a proper response
        /// </summary>
        ErrorType Error { get; set; } = ErrorType.NoError;

        /// <summary>
        /// Title of message
        /// </summary>
        string MessageTitle { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        string MessageText { get; set; }

        /// <summary>
        /// Data about product that will be sent to client
        /// </summary>
        DataType data { get; set; }
    }
}