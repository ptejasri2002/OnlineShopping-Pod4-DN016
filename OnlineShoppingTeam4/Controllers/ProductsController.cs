using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingTeam4.Context;
using PagedList;

namespace OnlineShoppingTeam4.Controllers
{
    public class ProductsController : Controller
    {
        private OnlineShoppingTeam4Database db = new OnlineShoppingTeam4Database();
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ProductsController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products(string category, string search = "", int? page = 1)
        {
            var products = db.Products as IQueryable<ViewModels.ViewProductCategoryS>;
            ViewBag.title = ViewBag.category = "Products";
            ViewBag.search = search;
            int categID = 0;

            int count = 0;
            foreach (var a in db.Categories)
            {
                count++;
                if (category == a.CategoryName)
                {
                    ViewBag.title = ViewBag.category = a.CategoryName;
                    categID = count;
                }
            }

            //Gets all products filtered by category and by name from the database.
            products = from prod in db.Products
                       join cat in db.Categories on prod.CategoryID equals cat.CategoryID
                       join supp in db.Suppliers on prod.SupplierID equals supp.SupplierID
                       where (categID > 0 ? prod.CategoryID == categID : true) && prod.ProductName.Contains(search)
                       orderby prod.ProductName ascending
                       select new ViewModels.ViewProductCategoryS
                       {
                           CategoryName = cat.CategoryName,
                           ProductName = prod.ProductName,
                           ProductID = prod.ProductID.ToString(),
                           ProductPrice = prod.UnitPrice ?? 0,
                           OnOrder = prod.UnitsOnOrder.ToString(),
                           Stock = prod.UnitsInStock.ToString(),
                           SuppliersName = supp.CompanyName,
                           Discontinued = prod.Discontinued
                       };
            products = products.OrderBy(x => x.Discontinued).ThenBy(y => y.ProductName);
            int pageSize;
            try
            {
                pageSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"]);
            }
            catch
            {
                logger.Error("Exista o eroare in configurare, key pageSize trebuie sa fie un numar");
                pageSize = 10;
            }
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

    }
}