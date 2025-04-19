using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GroupProject.Common;
using Microsoft.Windows.Themes;

namespace GroupProject.Items
{
    /// <summary>
    /// Handles business logic for Window wndItems
    /// </summary>
    internal class clsItemsLogic
    {
        /// <summary>
        /// Connection string for accessing the Invoice Microsoft Database
        /// </summary>
        static string sConnectionString = "Invoice.mdb";
        /// <summary>
        /// Instance of class clsDBAccess, handles database connection, queries, non-scalar returns, and other
        /// CRUD functionality for the application
        /// </summary>
        clsDBAccess db = new clsDBAccess(sConnectionString);

        /// <summary>
        /// Getter for Item data
        /// </summary>
        /// <returns>Contents of Items table from Invoice Database</returns>
        /// <exception cref="Exception">Generic Exception</exception>
        public List<clsItem> GetItems()
        {
            string sSQL = clsItemsSQL.GetItems();

            List<clsItem> ItemsList = new List<clsItem>();

            DataSet ds;

            int iRet = 0;

            try
            {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                clsItem item = new clsItem();

                item.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                item.ItemDesc = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                item.Cost = (decimal) ds.Tables[0].Rows[i].ItemArray[2];

                ItemsList.Add(item);
            }

            return ItemsList;
        }

        /// <summary>
        /// Getter for LineItem Invoice Numbers corresponding to ItemCode foreign key
        /// </summary>
        /// <param name="sItemCode">ItemCode foreign key in LineItems Table </param>
        /// <returns>Line Item Invoice Numbers from LineItems Table in Invoice Database</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public List<int> GetLineItemInvoiceNums(string sItemCode)
        {
            string sSQL = clsItemsSQL.GetLineItemInvoiceNums(sItemCode);

            List<int> lineItemInvoiceNums = new List<int>();

            DataSet ds;

            int iRet = 0;

            try
            {
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
            {
                lineItemInvoiceNums.Add((int) ds.Tables[0].Rows[i][0]);
            }

            return lineItemInvoiceNums;
        }

        /// <summary>
        /// Updates record in ItemDesc table of Invoice Database with sItemCode primary key with a new ItemDesc,
        /// sItemCode and Cost sCost
        /// </summary>
        /// <param name="sItemCode">Primary key of Item record to update</param>
        /// <param name="sItemDesc">New Item Description</param>
        /// <param name="sCost">New Cost</param>
        /// <returns>int value representing how many rows have been changed in Database,
        /// see GroupProject.Common.clsDBAccess for more information</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public int UpdateItemDesc(string sItemCode, string sItemDesc, string sCost)
        {
            string sSQL = clsItemsSQL.UpdateItemDesc(sItemCode, sItemDesc, sCost);
            int iNumRows;

            try
            {
                iNumRows = db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return iNumRows;
        }

        public int InsertItem(string sItemCode, string sItemDesc, string sCost)
        {
            string sSQL = clsItemsSQL.InsertItem(sItemCode, sItemDesc, sCost);
            int iNumRows;

            try
            {
                iNumRows = db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return iNumRows;
        }

        /// <summary>
        /// Deletes item with primary key ItemCode sItemCode from Invoice Database
        /// </summary>
        /// <param name="sItemCode">Primary key in ItemDesc Table of Inovice Database</param>
        /// <returns>int value representing how many items have been deleted</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public int DeleteItem(string sItemCode)
        {
            string sSQL = clsItemsSQL.DeleteItem(sItemCode);
            int iNumRows;

            try
            {
                iNumRows = db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return iNumRows;
        }

        /// <summary>
        /// Tests for presence of Item with Primary Key sItemCode in ItemDesc Table in InvoiceDatabase
        /// </summary>
        /// <param name="sItemCode">Primary Key in ItemDesc Table</param>
        /// <returns>boolean value indicating whether the Item is already in the Database</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public bool ItemInItemDesc(string sItemCode)
        {
            string sSQL = clsItemsSQL.ItemInItemDesc(sItemCode);
            bool itemInItemDesc = false;
            try
            {
               string sNumRows = db.ExecuteScalarSQL(sSQL);
               int iNumRows = int.Parse(sNumRows);
               if (iNumRows == 1)  itemInItemDesc = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return itemInItemDesc;
        }
        
        /// <summary>
        /// Tests for presence of Foreign Key sItemCode in LineItems Table of Invoice Database
        /// </summary>
        /// <param name="sItemCode">Foreign Key in LineItem Database</param>
        /// <returns>boolean value indicating whether the sItemCode is already in LineItems table</returns>
        /// <exception cref="Exception">Generic exception</exception>
        public bool ItemInLineItems(string sItemCode)
        {
            string sSQL = clsItemsSQL.ItemInLineItems(sItemCode);
            bool itemInLineItems = true;
            try
            {
                string sNumRows = db.ExecuteScalarSQL(sSQL);
                int iNumRows = int.Parse(sNumRows);
                if (iNumRows == 0) itemInLineItems = false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            return itemInLineItems;
        }
    }
}
