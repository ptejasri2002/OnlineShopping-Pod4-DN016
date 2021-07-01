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
using Microsoft.AspNet.Identity;
using System.Data.SqlClient;
namespace OnlineShoppingTeam4.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private OnlineShoppingTeam4Database db = new OnlineShoppingTeam4Database();
        private log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ProductController));  //Declaring Log4Net to log errors in Event View-er in NorthwindLog Application log.

        private static Random random = new Random();
        private string CustomerId()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            if (db.Customers.Find(result) != null)
                result = CustomerId();
            return result;
        }

        // GET: Payment
        public ActionResult CreateOrder( string amount)
        {
            Transactions model = new Transactions();
            if ( User.Identity.IsAuthenticated == true)
            {

                model.Amount = amount;
                model.Email = User.Identity.GetUserName();
            }
            
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateOrder([Bind(Include = "Email,Amount,CardNo,CardHolderName,CVV,ExpiryMonth,ExpiryYear")] Transactions transactions)
        {
            if (User.Identity.IsAuthenticated == true)
            {


                //Transactions custom = new Transactions()
                //{
                //    CustomerID = CustomerId(),
                //    Email  = transactions.Email ,
                //    Amount = transactions.Amount ,
                //    CardNo = transactions.CardNo ,
                //    CardHolderName = transactions.CardHolderName,
                //    CVV = transactions.CVV,
                //    ExpiryMonth = transactions.ExpiryMonth,
                //    ExpiryYear  = transactions.ExpiryYear

                //};
                string constr = System.Configuration.ConfigurationManager. ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    string cmdstr = @"insert into Transactions (Email,Amount,CardNo,CardHolderName,CVV,ExpiryMonth,ExpiryYear) values (
  '"+ transactions .Email + "','" + transactions.Amount + "','" + transactions.CardNo + "','" + transactions.CardHolderName + "'," +
  "'" + transactions.CVV + "','" + transactions.ExpiryMonth + "','" + transactions.ExpiryYear + "')";
                    SqlCommand cm = new SqlCommand(cmdstr,con);
                    cm.ExecuteNonQuery();
                    con.Close();
                }

                //db.Transactions.Add(transactions);
                //await db.SaveChangesAsync();
            }
            return RedirectToAction("Success");
        }

       

        public ActionResult Success()
        {
            return View();
        }

       
    }
}