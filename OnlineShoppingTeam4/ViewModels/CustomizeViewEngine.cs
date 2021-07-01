using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Web;  
using System.Web.Mvc;  
   
namespace OnlineShoppingTeam4.ViewModels
{
    /// <summary>
    /// Navigation link format
    /// </summary>
    public class CustomizeViewEngine : RazorViewEngine
    {   /// <summary>
        ///  identifies the location of the pages in the project
        /// </summary>
        public CustomizeViewEngine()
        {
            //here we are resetting the ViewLocationFormats  
            ViewLocationFormats = new string[] { "~/Views/{1}/{0}.cshtml"};
        }
    }
}