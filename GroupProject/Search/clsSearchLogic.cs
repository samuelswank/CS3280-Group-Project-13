using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static GroupProject.Search.clsSearchLogic;
using System.Windows;
using GroupProject.Common;

namespace GroupProject.Search
{
    internal class clsSearchLogic
    {
        // GetDistinctInvoiceNumber()
        // GetDistinctInvoiceDate()
        // GetDistinctInvoiceCost()

        // GetInvoices(InvoiceNumber, InvoiceDate, InvoiceCost) - returns List<clsInvoices>
        //      GetInvoiceNumberSQL
        //      GetInvoiceDateSQL
        //      GetInvoiceCostSQL
       
        static string sConnectionString = "Invoice.mdb";

        clsDBAccess db = new clsDBAccess(sConnectionString);



        /// <summary>
        /// Gets a list of all invoices, As of right now, mainly used for testing SQL statements
        /// </summary>
        /// <returns></returns>
        //public List<clsInvoice> GetInvoice(string sSQL)
        public List<clsInvoice> GetInvoice(string sSQL)
        {
            //create list of Invoices
            List<clsInvoice> InvoiceList = new List<clsInvoice>();

            //Create a DataSet to hold the data
            DataSet ds;

            //Number of return values
            int iRet = 0;

            //Get all the values from the Invoices table
            try
            {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            //Loop through all the values returned
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // create new clsInvoice class
                clsInvoice invoice = new clsInvoice();

                // fill class with data
                invoice.InvoiceID = ds.Tables[0].Rows[i][0].ToString();
                invoice.InvoiceDate = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                invoice.InvoiceCost = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                // add invoice object to Invoicelist
                InvoiceList.Add(invoice);
            }
            // return list of invoices
            return InvoiceList;
        }

        public string GetSQLStatement(string sNum, string sDate, string sCost)
        {
            decimal dCost = 0;
            // convert string to decimal
            if (sCost != "")
            {
                dCost = Convert.ToDecimal(sCost);
            }

            // sSQL string holds the sql statement
            string sMySQL = "";

            // if only num seleted
            if ((sNum != "") && !(sDate != "") && !(dCost != 0))
            {
                sMySQL = clsSearchSQL.GetInvoiceNum(sNum);
            }

            // if only date selected
            else if (!(sNum != "") && (sDate != "") && !(dCost != 0))
            {
                sMySQL = clsSearchSQL.GetInvoiceDate(sDate);
            }

            // if only cost selected
            else if (!(sNum != "") && !(sDate != "") && (dCost != 0))
            {
                sMySQL = clsSearchSQL.GetInvoiceCost(dCost);
            }

            // num and date selected
            else if ((sNum != "") && (sDate != "") && !(dCost != 0))
            {
                sMySQL = clsSearchSQL.GetInvoiceNumDate(sNum, sDate);
            }

            // num and cost selected
            else if ((sNum != "") && !(sDate != "") && (dCost != 0))
            {
                sMySQL = clsSearchSQL.GetInvoiceNumCost(sNum, dCost);
            }

            // date and cost selected
            else if (!(sNum != "") && (sDate != "") && (dCost != 0))
            {
                sMySQL = clsSearchSQL.GetInvoiceDateCost(sDate, dCost);
            }

            // all selected
            else if ((sNum != "") && (sDate != "") && (dCost != 0))
            {
                sMySQL = clsSearchSQL.GetInvoiceNumDateCost(sNum, sDate, dCost);
            }

            // nothing selected
            else
            {
                // sSQL string holds the sql statement from getInvoices
                sMySQL = clsSearchSQL.GetInvoices();
                return sMySQL;
            }

            //populate datagrid
            return sMySQL;

        }

    }
}
