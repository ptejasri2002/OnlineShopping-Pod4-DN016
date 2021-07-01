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
using OnlineShoppingTeam4.Models.ServerClientCommunication;
using OnlineShoppingTeam4.Models.ExceptionHandler;
using OnlineShoppingTeam4.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace OnlineShoppingTeam4.Controllers
{
    /// <summary>
    /// Contains all the methods neccessary for CRUD on the suppliers table in the database.
    /// </summary>
    [Authorize(Roles="Admin")]
    public class SuppliersController : Controller, IJsonTableFillServerSide
    {
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SuppliersController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.
        private OnlineShoppingTeam4Database db = new OnlineShoppingTeam4Database();
        private ApplicationUserManager _userManager;
        ApplicationUserManager userManager;
        
        public SuppliersController()
        {
            
        }
        public SuppliersController(ApplicationUserManager userManager)
        {
            UserManager = userManager;

        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Displays a page containing a datatable with all the suppliers in the database.
        /// </summary>
        /// <returns>Suppliers index view.</returns>
        public ActionResult Index()
        
        {
            var _list = (from s in db.Suppliers where s.SupplierID != 0 select s).ToList();
            return View(_list);
        }

        /// <summary>
        /// Displays a page containing all the information about one supplier.
        /// </summary>
        /// <param name="id">The id of the supplier whose information will be shown.</param>
        /// <returns>Suppliers details view.</returns>
        public async Task<ActionResult> Details(int? id)
        {
            //if a vendor has been selected
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.FindAsync(id);
            //if a vendor has been found
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        /// <summary>
        /// Displays a page containing a form neccessary to create a new supplier.
        /// </summary>
        /// <returns>Suppliers create view.</returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Adds a new supplier in the database.
        /// </summary>
        /// <param name="suppliers">The supplier entity to be added.</param>
        /// <returns>Suppliers index view.</returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Email,Password")] Suppliers suppliers)
        {//if inputs data correspond to the model
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = suppliers.Email, Email = suppliers.Email };
                var result = await   UserManager.CreateAsync(user, suppliers.Password);
                if (result.Succeeded)
                {
                    await this.UserManager.AddToRoleAsync(user.Id, "Retailer");

                    db.Suppliers.Add(suppliers);
                    await db.SaveChangesAsync();
                }
                //Assign Role to user Here 
               
                return RedirectToAction("Index");
            }

            return View(suppliers);
        }
       
        /// <summary>
        /// Displays a page containing a form neccessary to edit an existing supplier.
        /// </summary>
        /// <param name="id">The id of the supplier that is going to be edited.</param>
        /// <returns>Suppliers edit view.</returns>
        public async Task<ActionResult> Edit(int? id)
        {//if a vendor has been selected
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.FindAsync(id);
            //if a vendor has been found
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        /// <summary>
        /// Updates the information of a supplier in the database.
        /// </summary>
        /// <param name="suppliers">The supplier entity with the updated information.</param>
        /// <returns>Suppliers index view</returns>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SupplierID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax,HomePage")] Suppliers suppliers)
        {//if inputs data correspond to the model
            if (ModelState.IsValid)
            {
                db.Entry(suppliers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(suppliers);
        }

        /// <summary>
        /// Displays a confirmation page for the following delete operation.
        /// </summary>
        /// <param name="id">The id of the supplier that is going to be deleted.</param>
        /// <returns>Suppliers delete view.</returns>
        public async Task<ActionResult> Delete(int? id)
        {//if a vendor has been selected
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suppliers suppliers = await db.Suppliers.FindAsync(id);
            //if a vendor has been found
            if (suppliers == null)
            {
                return HttpNotFound();
            }
            return View(suppliers);
        }

        /// <summary>
        /// Deletes a supplier from the database.
        /// </summary>
        /// <param name="id">The id of the supplier that is going to be deleted.</param>
        /// <returns>Suppliers index view.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {//if the supplier exists and can be deleted
            try
            {
                Suppliers suppliers = await db.Suppliers.FindAsync(id);
                db.Suppliers.Remove(suppliers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch
            {
                string error = "Nu puteti sterge un furnizor cu comenzi alocate: ";
                var orders = db.Orders.Where(o => o.ShipVia == id).Select(o => new { o.OrderID });
                foreach (var order in orders)
                {
                    error += order.OrderID.ToString() + " ";
                }
                logger.Error(error.ToString());
                throw new DeleteException(error);
            }
        }

        /// <summary>
        /// Send back a JsonDataTableObject as json with all the information that we need to populate datatable
        /// </summary>
        /// <param name="draw">Draw order. Client send a draw id in request to keep track of asyncron response</param>
        /// <param name="start">Start from this item</param>
        /// <param name="length">Take a list with "lenght" (if exists) objects inside.</param>
        /// <returns>JsonDataTableObject</returns>
        public JsonResult JsonTableFill(int draw, int start, int length)
        {
            const int TOTAL_ROWS = 999;

            string search = "";
            try
            {
                search = Request.QueryString["search[value]"] ?? "";
            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());

            }
            int sortColumn = -1;
            string sortDirection = "asc";
            if (length == -1)
            {
                length = TOTAL_ROWS;
            }

            // note: we only sort one column at a time
            try
            {
                if (Request.QueryString["order[0][column]"] != null)
                {
                    sortColumn = int.Parse(Request.QueryString["order[0][column]"]);
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());

            }
            try
            {
                if (Request.QueryString["order[0][dir]"] != null)
                {
                    sortDirection = Request.QueryString["order[0][dir]"];
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception.ToString());

            }

            //list of product that contain "search"
            var suppliersInfo = db.Suppliers.OrderBy(x => x.SupplierID).Where(s => s.CompanyName.Contains(search) || s.ContactName.Contains(search)
            || s.ContactTitle.Contains(search) || s.Address.Contains(search) || s.City.Contains(search) || s.Country.Contains(search) || s.Phone.Contains(search));


            //order list
            switch (sortColumn)
            {
                case -1: //sort by first column
                    goto FirstColumn;
                case 0: //first column
                    FirstColumn:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.CompanyName);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.CompanyName);
                    }
                    break;
                case 1: //second column
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.ContactName);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.ContactName);
                    }
                    break;
                case 2: // and so on
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.ContactTitle);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.ContactTitle);
                    }
                    break;
                case 3:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.Address);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.Address);
                    }
                    break;
                case 4:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.City);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.City);
                    }
                    break;
                case 5:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.Country);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.Country);
                    }
                    break;
                case 6:
                    if (sortDirection == "asc")
                    {
                        suppliersInfo = suppliersInfo.OrderBy(x => x.Phone);
                    }
                    else
                    {
                        suppliersInfo = suppliersInfo.OrderByDescending(x => x.Phone);
                    }
                    break;
            }

            //objet that whill be sent to client
            JsonDataTable dataTableData = new JsonDataTable()
            {
                draw = draw,
                recordsTotal = db.Suppliers.Count(),
                data = suppliersInfo.Skip(start).Take(length).Select(x => new
                {
                    SupplierID = x.SupplierID,
                    CompanyName = x.CompanyName,
                    ContactName = x.ContactName,
                    ContactTitle = x.ContactTitle,
                    Address = x.Address,
                    City = x.City,
                    Country = x.Country,
                    Phone = x.Phone
                }),
                recordsFiltered = suppliersInfo.Count(), //need to be below data(ref recordsFiltered)
            };
            return Json(dataTableData, JsonRequestBehavior.AllowGet);
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
