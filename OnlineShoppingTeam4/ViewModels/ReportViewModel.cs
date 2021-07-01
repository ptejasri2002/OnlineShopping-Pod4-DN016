using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShoppingTeam4.ViewModels
{
    /// <summary>
    /// Raports View Model
    /// </summary>
    public class ReportViewModel
    {
        /// <summary>
        /// Constructor of ReportViewModel
        /// </summary>
        /// <param name="link">set link</param>
        /// <param name="filename">set filename</param>
        public ReportViewModel(string link, string filename)
        {
            this.Link = link;
            this.Filename = filename;
        }

        /// <summary>
        /// Link of RaportView
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// File name of RaportView
        /// </summary>
        public string Filename { get; set; }

    }

    /// <summary>
    /// Holds username and password for the report server.
    /// </summary>
    public class ReportLoginViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}