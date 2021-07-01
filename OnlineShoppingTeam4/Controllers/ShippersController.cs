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
using OnlineShoppingTeam4.Models.Interfaces;
using OnlineShoppingTeam4.Models.ExceptionHandler;
using OnlineShoppingTeam4.Context;

namespace OnlineShoppingTeam4.Controllers
{

    /// <summary>
    /// Shippers Controller. For table Shippers
    /// </summary>
    [Authorize(Roles = "Admins, Managers")]
    public class ShippersController : Controller, IJsonTableFill
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HomeController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private OnlineShoppingTeam4Database db = new OnlineShoppingTeam4Database();

        /// <summary>
        /// Displays a page with all the shippers existing in the database.
        /// </summary>
        /// <param name="search">The search look to find something asked</param>
        /// <returns>Shippers index view</returns>
        public async Task<ActionResult> Index(string search = "")
        {
            return View(await db.Shippers.Where(x => x.CompanyName.Contains(search)).ToListAsync());
        }

        /// <summary>
        /// Displays a page showing all the information about one shipper.
        /// </summary>
        /// <param name="id">The id of the shipper whose information to show</param>
        /// <returns>Shippers details view</returns>
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return HttpNotFound();
            }
            return View(shippers);
        }

        /// <summary>
        /// Returns the view containing the form neccesary for creating a new shipper.
        /// </summary>
        /// <returns>Create view.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Inserts a shipper into the database table. If it fails, goes back to the form.
        /// </summary>
        /// <param name="shippers">The shipper entity to be inserted</param>
        /// <returns>If successful returns shippers index view, else goes back to form.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ShipperID,CompanyName,Phone")] Shippers shippers)
        {
            if (ModelState.IsValid)
            {
                db.Shippers.Add(shippers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(shippers);
        }

        /// <summary>
        /// Returns the view containing the form necessary for editing an existing shipper.
        /// </summary>
        /// <param name="id">The id of the shipper that is going to be edited</param>
        /// <returns>Shippers edit view</returns>
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return HttpNotFound();
            }
            return View(shippers);
        }

        /// <summary>
        /// Updates the database changing the fields of the shipper whose id is equal to the id of the provided shippers parameter to those of the parameter.
        /// </summary>
        /// <param name="shippers">The changed shipper.</param>
        /// <returns>Shippers index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ShipperID,CompanyName,Phone")] Shippers shippers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(shippers);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete.
        /// </summary>
        /// <param name="id">The shipper that is going to be deleted.</param>
        /// <returns>Delete view</returns>
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return HttpNotFound();
            }
            return View(shippers);
        }

        /// <summary>
        /// Deletes a shipper from the database. The shipper must not have id in employees.
        /// </summary>
        /// <param name="id">The id of the shipper that is going to be deleted</param>
        /// <returns>Shippers index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            //take details of Shipper
            Shippers shippers = await db.Shippers.FindAsync(id);
            
            try 
            {
                db.Shippers.Remove(shippers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch(Exception exception)
            {
                logger.Error(exception.ToString());
                string list = "";
                var orderid = db.Orders.Include(x => x.Shipper).Where(x => x.ShipVia == id).Select(x => new { x.OrderID });
                foreach (var i in orderid)
                {   //lopp in all OrderID
                    list = list + i.OrderID.ToString() + ", ";
                }
                throw new DeleteException("You cannot delete the sender because it contains employees with IDs:\n" + list + "\n In order to be able to delete this sender, their orders and details must be deleted.");
            }
        }

        /// <summary>
        /// Function used to control the dashboard datatables local
        /// </summary>
        /// <returns>A JSON filtered shipper list.</returns>
        public JsonResult JsonTableFill()
        {
            var shippers = db.Shippers.OrderBy(x => x.ShipperID);

            //Select what wee need in table
            return Json(
                shippers.Select(x => new OnlineShoppingTeam4.Models.ServerClientCommunication.ShipperData
                {
                    ID = x.ShipperID,
                    CompanyName = x.CompanyName,
                    Phone = x.Phone
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
