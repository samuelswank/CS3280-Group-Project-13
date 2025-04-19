using GroupProject.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace GroupProject.Main
{
    public class clsMainLogic
    {
        /// <summary>
        /// Handles accessing the database
        /// </summary>
        private clsMainSQL db;

        public clsMainLogic()
        {
            db = new clsMainSQL();
        }
        /// <summary>
        /// Gets all items from the database.
        /// </summary>
        /// <returns></returns>
        public List<clsItem> AllItems()
        {
            int items = 0;
            DataSet ds = db.GetAllItem(ref items);
            List<clsItem> AllItems = new List<clsItem>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                clsItem item = new clsItem
                {
                    ItemCode = row["ItemCode"].ToString(),
                    ItemDesc = row["ItemDesc"].ToString(),
                    Cost = (decimal) row["Cost"]
                };

                AllItems.Add(item);
            }



            return AllItems;
        }
        /// <summary>
        /// Gets all line items for a specific invoice.
        /// </summary>
        /// <param name="InvoiceID"></param>
        /// <returns></returns>
        public List<clsItem> GetInvoice(string InvoiceID)
        {

            DataSet ds = db.GetInvoiceLineItems(InvoiceID);
            List<clsItem> Invoice = new List<clsItem>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                clsItem inv = new clsItem
                {
                    ItemCode = row["ItemCode"].ToString(),
                    ItemDesc = row["ItemDesc"].ToString(),
                    Cost = (decimal) row["Cost"]
                };
                Invoice.Add(inv);
            }

            return Invoice;


        }
        /// <summary>
        /// Inserts a new line item into an invoice.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int InsertLineItem(string invoiceNum, string lineItemNum, string itemCode)
        {
            return db.InsertLineItems(invoiceNum, lineItemNum, itemCode);
        }
        /// <summary>
        /// Deletes a line item from an invoice.
        /// </summary>
        /// <param name="InvoiceNum"></param>
        /// <param name="LineItemNum"></param>
        /// <returns></returns>
        public int DeleteLineItem(string InvoiceNum, string LineItemNum)
        {
            return db.DeleteLineItemFromInvoice(InvoiceNum, LineItemNum);
        }
        /// <summary>
        /// Calculates the total cost of all items in a list.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public decimal AddTotalCost(List<clsItem> items)
        {
            decimal total = 0;
            foreach(clsItem item in items)
            {
                total += item.Cost;
            }
            return total;
        }
        /// <summary>
        /// Updates the total cost of a specific invoice.
        /// </summary>
        /// <param name="invoicenum"></param>
        /// <param name="updatedCost"></param>
        /// <returns></returns>
        public int UpdateTotalCost(string invoicenum, float updatedCost)
        {
            return db.UpdateTotal(invoicenum, updatedCost);
        }
        /// <summary>
        /// Inserts a new invoice into the database.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public int InsertInvoice(string invoiceDate, int totalCost = 0)
        {
            return db.InsertInvoice(invoiceDate, totalCost);
        }
    }
}
