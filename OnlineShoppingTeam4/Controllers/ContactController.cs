using OnlineShoppingTeam4.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using OnlineShoppingTeam4.Context;

namespace OnlineShoppingTeam4.Controllers
{
    /// <summary>
    /// Contact Controller. For table Persons which is used in Contact index view
    /// </summary>
    public class ContactController : Controller
    {
        private OnlineShoppingTeam4Database db = new OnlineShoppingTeam4Database();

        /// <summary>
        /// Displays a Contact index page.
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Contact index view</returns>
        public ActionResult Index(string status)
        {
            if (!String.IsNullOrEmpty(status))
            {
                ViewBag.Status = status;

            }
            return View();
        }

       
    }
}