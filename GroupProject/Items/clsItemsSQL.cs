using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using GroupProject.Common;

namespace GroupProject.Items
{
    /// <summary>
    /// Generates SQL queries through static methods
    /// </summary>
    internal class clsItemsSQL
    {
        /// <summary>
        /// Generates SQL query for returning all Items FROM the ItemDesc Table
        /// </summary>
        /// <returns>SQL query</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public static string GetItems()
        {
            try
            {
                string sSQL = "SELECT * FROM ItemDesc;";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Generates SQL query for returning all LineItemInvoiceNums associated with Foreign Key sItemCode
        /// </summary>
        /// <param name="sItemCode">Foreign Key in LineItems Database</param>
        /// <returns>SQL query</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public static string GetLineItemInvoiceNums(string sItemCode)
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceNum) FROM LineItems WHERE ItemCode = '" + sItemCode +
                    "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Generates SQL query for to UPDATE ItemDesc Table record in Invoice Database
        /// </summary>
        /// <param name="sItemCode">Primary Key of ItemDesc Table for the record to be updated</param>
        /// <param name="sItemDesc">New ItemDesc</param>
        /// <param name="sCost">New Cost</param>
        /// <returns>SQL query</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public static string UpdateItemDesc(string sItemCode, string sItemDesc, string sCost)
        {
            try
            {
                string sSQL = "UPDATE ItemDesc SET ItemDesc = '" + sItemDesc + "', Cost = " + sCost +
                    " WHERE ItemCode = '" + sItemCode + "';";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Generates SQL query to INSERT a new record INTO ItemDesc Table of Invoice Database
        /// </summary>
        /// <param name="sItemCode">New Primary Key</param>
        /// <param name="sItemDesc">New ItemDesc</param>
        /// <param name="sCost">New COst</param>
        /// <returns>SQL query</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public static string InsertItem(string sItemCode, string sItemDesc, string sCost)
        {
            try
            {
                string sSQL = "INSERT INTO ItemDesc(ItemCode, ItemDesc, Cost) Values('" +
                    sItemCode + "', '" + sItemDesc + "', " + sCost + ");";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Generates SQL query to DELETE a record from the ItemDesc Table in the Invoice Database
        /// </summary>
        /// <param name="sItemCode">Primary Key for Item to be deleted</param>
        /// <returns>SQL query</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public static string DeleteItem(string sItemCode)
        {
            try
            {
                string sSQL = "DELETE FROM ItemDesc WHERE ItemCode = '" + sItemCode + "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string ItemInItemDesc(string sItemCode)
        {
            try
            {
                string sSQL = "SELECT COUNT(*) FROM ItemDesc WHERE ItemCode = '" + sItemCode + "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Generates SQL query for testing if Item with Foreign Key sItemCode is in LineItems Table
        /// </summary>
        /// <param name="sItemCode">Foreign Key in LineItems Table</param>
        /// <returns>SQL query</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public static string ItemInLineItems(string sItemCode)
        {
            try
            {
                string sSQL = "SELECT COUNT(*) FROM LineItems WHERE ItemCode = '" + sItemCode + "';";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
