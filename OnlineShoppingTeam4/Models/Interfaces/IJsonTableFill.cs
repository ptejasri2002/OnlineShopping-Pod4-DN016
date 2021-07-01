using System.Web.Mvc;

namespace OnlineShoppingTeam4.Models.Interfaces
{
    /// <summary>
    /// Basic interface of datatable comunication (server side)
    /// </summary>
    public interface IJsonTableFillServerSide
    {
        /// <summary>
        /// get data from server. Use it if you need a server side datatable
        /// </summary>
        /// <param name="draw">Current draw on client</param>
        /// <param name="start">Start from "start"</param>
        /// <param name="length">Take "lenght" elements</param>
        /// <returns>You must return data as datatable need, see https://datatables.net/examples/data_sources/server_side.html </returns>
        JsonResult JsonTableFill(int draw, int start, int length);
    }

    /// <summary>
    /// Basic interface of datatable comunication (client side)
    /// </summary>
    public interface IJsonTableFill
    {
        /// <summary>
        /// get data from server. Use it if you need a client side datatable
        /// </summary>
        /// <returns>You can return data as a list of objects see https://datatables.net for more information </returns>
        JsonResult JsonTableFill();
    }
}
