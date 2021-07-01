using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShoppingTeam4.Models;
using OnlineShoppingTeam4.Models.ExceptionHandler;
using OnlineShoppingTeam4.Context;

namespace OnlineShoppingTeam4.Controllers
{

    /// <summary>
    /// Categories Controller. For table Categories
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class CategoriesController : Controller
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private OnlineShoppingTeam4Database db = new OnlineShoppingTeam4Database();

        /// <summary>
        /// Displays a page with all the categories existing in the database.
        /// </summary>
        /// <param name="search">The search look to find something asked</param>
        /// <returns>Categories index view</returns>
        public async Task<ActionResult> Index(string search = "")
        {
            return View(await db.Categories.Where(x => x.CategoryName.Contains(search)).ToListAsync());
        }

        /// <summary>
        /// Displays a page showing all the information about one category.
        /// </summary>
        /// <param name="id">The id of the category whose information to show</param>
        /// <returns>Categories details view</returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new category.
        /// </summary>
        /// <returns>Create view.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Inserts a category into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="categories">The category entity to be inserted</param>
        /// <returns>If successful returns categories index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryID,CategoryName,Description")] Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return View(categories);
            }

            if (db.Categories.Where(c => c.CategoryName == categories.CategoryName).Count()!=0)
            {
                ModelState.AddModelError("CategoryName", "Numele de categorie este deja folosit");
                return View(categories);
            }
            db.Categories.Add(categories);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing category.
        /// </summary>
        /// <param name="id">The id of the category that is going to be edited</param>
        /// <returns>Categories edit view</returns>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        /// <summary>
        /// Updates the database changing the fields of the category whose id is equal to the id of the provided category parameter to those of the parameter.
        /// </summary>
        /// <param name="categories">The changed category.</param>
        /// <returns>Categories index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryID,CategoryName,Description")] Categories categories)
        {
            if (!ModelState.IsValid)
            {
                return View(categories);

            }
            if (db.Categories.Where(c => (c.CategoryName == categories.CategoryName)&&(c.CategoryID!= categories.CategoryID)).Count()!=0)
            {
                ModelState.AddModelError("CategoryName", "Numele de categorie este deja folosit");
                return View(categories);
            }
            db.Entry(categories).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The category that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);
            if (categories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        /// <summary>
        /// Deletes a category from the database. The category must not have products.
        /// </summary>
        /// <param name="id">The id of the category that is going to be deleted</param>
        /// <returns>Categories index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //take details of Categories
            Categories categories = await db.Categories.FindAsync(id);

            try
            {
                db.Categories.Remove(categories);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());
                string list = "";
                var productId = db.Products.Include(x => x.Category).Where(x => x.CategoryID == id).Select(x => new { x.ProductName });
                foreach (var i in productId)
                {
                    //loop in ProductName
                    list = list + i.ProductName.ToString() + ",\n ";
                }
                throw new DeleteException("Nu poti sterge categoria deoarece are produse cu numele:\n" + list + "\nPentru a putea sterge aceasta categorie trebuie sterse produsele.");
            }
        }



        /// <summary>
        /// Function used to control the dashboard datatables local
        /// </summary>
        /// <param name="search"></param>
        /// <returns>A JSON filtered categories list.</returns>
        public JsonResult JsonTableFill(string search = "")
        {
            var categories = db.Categories.Include(p => p.Description).Where(x => x.CategoryName.Contains(search)).OrderBy(x => x.CategoryID);

            //Select what wee need in table
            return Json(
               categories.Select(x => new OnlineShoppingTeam4.Models.ServerClientCommunication.CategoriesData
               {
                   ID = x.CategoryID,
                   CategoryName = x.CategoryName,
                   Description = x.Description

               })
                , JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}