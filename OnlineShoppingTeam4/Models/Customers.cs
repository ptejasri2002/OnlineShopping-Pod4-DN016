namespace OnlineShoppingTeam4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The entity that holds all the information from the Customers table in the database.
    /// </summary>
    public partial class Customers
    {

        /// <summary>
        /// Default constructor. Initialises new empty instances for Orders and CustomerDemographics..
        /// </summary>
        public Customers()
        {
            Orders = new HashSet<Orders>();
            CustomerDemographics = new HashSet<CustomerDemographics>();
        }

        /// <summary>
        /// The id of the customer.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "ID Client este obligatoriu.")]
        [StringLength(5)]
        public string CustomerID { get; set; }

        /// <summary>
        /// The name of the company which is the customer.
        /// </summary>
        [Required(ErrorMessage ="Nume Companie este obligatoriu.")]
        [StringLength(40)]
        public string CompanyName { get; set; }

        /// <summary>
        /// The name of the contact person from the company.
        /// </summary>
        [StringLength(30)]
        public string ContactName { get; set; }

        /// <summary>
        /// The function of the contact person.
        /// </summary>
        [StringLength(30)]
        public string ContactTitle { get; set; }

        /// <summary>
        /// The adress of the customer.
        /// </summary>
        [StringLength(60)]
        public string Address { get; set; }

        /// <summary>
        /// The customer's city.
        /// </summary>
        [StringLength(15)]
        public string City { get; set; }

        /// <summary>
        /// The customer's region.
        /// </summary>
        [StringLength(15)]
        public string Region { get; set; }

        /// <summary>
        /// The customer's postal code.
        /// </summary>
        [StringLength(10)]
        public string PostalCode { get; set; }

        /// <summary>
        /// The customer's country.
        /// </summary>
        [StringLength(15)]
        public string Country { get; set; }

        /// <summary>
        /// The phone number of the customer.
        /// </summary>
        [StringLength(24)]
        public string Phone { get; set; }

        /// <summary>
        /// The fax of the customer.
        /// </summary>
        [StringLength(24)]
        public string Fax { get; set; }

        /// <summary>
        /// The orders of the customer.
        /// </summary>
        public virtual ICollection<Orders> Orders { get; set; }

        /// <summary>
        /// The demography of the customer.
        /// </summary>
        public virtual ICollection<CustomerDemographics> CustomerDemographics { get; set; }
    }
}
