using System.Web.Mvc;
using OnlineShoppingTeam4.Models;
using System.Linq;
using OnlineShoppingTeam4.Context;

namespace OnlineShoppingTeam4.Controllers
{
    /// <summary>
    /// AboutController. For the page Index from AboutUs 
    /// </summary>
    public class AboutController : Controller
    {
        OnlineShoppingTeam4Database db = new OnlineShoppingTeam4Database();
       
        /// <summary>
        /// Select the first 6 employees
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            return View();
        }
	}
}