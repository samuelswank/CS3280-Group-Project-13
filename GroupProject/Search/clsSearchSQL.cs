using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Search
{
    internal class clsSearchSQL
    {
        /// <summary>
        /// Select fron invoices when given: nothing
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoices()
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Select fron invoices when given: InvoiceNum
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceNum(string sInvoiceNum)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Select fron invoices when given: InvoiceNum and InvoiceDate
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceNumDate(string sInvoiceNum, string sInvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceNum + " AND InvoiceDate = #" + sInvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Select fron invoices when given: InvoiceNum, InvoiceDate, and InvoiceCost
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <param name="sInvoiceDate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceNumDateCost(string sInvoiceNum, string sInvoiceDate, decimal dInvoiceCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceNum + " AND InvoiceDate = #" + sInvoiceDate + "# AND TotalCost = " + dInvoiceCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Select fron invoices when given: InvoiceCost
        /// </summary>
        /// <param name="dInvoiceCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceCost(decimal dInvoiceCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + dInvoiceCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Select fron invoices when given: InvoiceDate and InvoiceCost
        /// </summary>
        /// <param name="sInvoiceNum"></param>
        /// <param name="sInvoiceDate"></param>
        /// <param name="dInvoiceCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceDateCost(string sInvoiceDate, decimal dInvoiceCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + sInvoiceDate + "# AND TotalCost = " + dInvoiceCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Select fron invoices when given: InvoiceDate
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <param name="dInvoiceCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoiceDate(string sInvoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + sInvoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// return a list of all InvoiceNums
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctNum()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// return a list of all InvoiceDates
        /// </summary>
        /// <param name="sInvoiceDate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctDate()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// return a list of all TotalCosts
        /// </summary>
        /// <param name="dInvoiceCost"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctCost()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
