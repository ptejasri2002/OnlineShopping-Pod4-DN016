namespace OnlineShoppingTeam4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    /// <summary>
    /// The entity that holds all the information from the Employees table in the database.
    /// </summary>
    public partial class Employees
    {
        /// <summary>
        /// Default constructor. Initialises new empty instances for Employees1, Orders and Territories.
        /// </summary>
        public Employees()
        {
            Employees1 = new HashSet<Employees>();
            Orders = new HashSet<Orders>();
           
        }

        /// <summary>
        /// The id of the employee.
        /// </summary>
        [Key]
        public int EmployeeID { get; set; }

        /// <summary>
        /// The lastname of the employee.
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        /// <summary>
        /// The firstname of the employee.
        /// </summary>
        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        /// <summary>
        /// The function of the employee.
        /// </summary>
        [StringLength(30)]
        public string Title { get; set; }

        /// <summary>
        /// The title of courtesy of the employee(Dra.,Dr. or Dl.).
        /// </summary>
        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        /// <summary>
        /// The date of birth of the employee.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// The date when the employee was hired.
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// The adress of the employee.
        /// </summary>
        [StringLength(60)]
        public string Address { get; set; }

        /// <summary>
        /// The city where the employee lives.
        /// </summary>
        [StringLength(15)]
        public string City { get; set; }

        /// <summary>
        /// The region where the employee lives.
        /// </summary>
        [StringLength(15)]
        public string Region { get; set; }

        /// <summary>
        /// The postal code from the city where the employee lives.
        /// </summary>
        [StringLength(10)]
        public string PostalCode { get; set; }

        /// <summary>
        /// The country where the employee lives.
        /// </summary>
        [StringLength(15)]
        public string Country { get; set; }

        /// <summary>
        /// The phone number of the employee.
        /// </summary>
        [StringLength(24)]
        public string HomePhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(4)]
        public string Extension { get; set; }

        /// <summary>
        /// The photo of the employee.
        /// </summary>
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        /// <summary>
        /// Some information about the employee.
        /// </summary>
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        
        /// <summary>
        /// The id of the employee responsible for this employee.
        /// </summary>
        public int? ReportsTo { get; set; }

        /// <summary>
        /// The employees that this employee is responsible for.
        /// </summary>
        public virtual ICollection<Employees> Employees1 { get; set; }

        /// <summary>
        /// The employee that is responsible for this employee.
        /// </summary>
        public virtual Employees Employee1 { get; set; }

        /// <summary>
        /// The orders of the employee.
        /// </summary>
        public virtual ICollection<Orders> Orders { get; set; }

      
    }
}
