using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineShoppingTeam4.ViewModels
{
    /// <summary>
    /// New customer from ShopCart
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// The name of the company which is the customer.
        /// </summary>
        
        [StringLength(40)]
        [Display(Name = "Nume Companie")]
        public string CompanyName { get; set; }

        
        /// <summary>
        /// The function of the contact person.
        /// </summary>
        [StringLength(30)]
        [Display(Name = "Pozitie")]
        public string ContactTitle { get; set; }

        /// <summary>
        /// The adress of the customer.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [StringLength(60)]
        [Display(Name = "Adresa")]
        public string Address { get; set; }

        /// <summary>
        /// The customer's city.
        /// </summary>
        [StringLength(15)]
        [Display(Name = "Oras")]
        public string City { get; set; }

        /// <summary>
        /// The customer's region.
        /// </summary>
        [StringLength(15)]
        [Display(Name = "Regiune")]
        public string Region { get; set; }

        /// <summary>
        /// The customer's postal code.
        /// </summary>
        [StringLength(10)]
        [Display(Name = "Cod Postal")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The customer's country.
        /// </summary>
        [StringLength(15)]
        [Display(Name = "Tara")]
        public string Country { get; set; }

        /// <summary>
        /// The phone number of the customer.
        /// </summary>
        [Required(ErrorMessage = "Acest camp '{0}' este obligatoriu!")]
        [StringLength(24)]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        /// <summary>
        /// The fax of the customer.
        /// </summary>
        [StringLength(24)]
        [Display(Name = "Fax")]
        public string Fax { get; set; }
    }
}