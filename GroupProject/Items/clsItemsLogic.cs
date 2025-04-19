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
    internal class clsItemsLogic
    {
        static string sConnectionString = "Invoice.mdb";
        clsDBAccess db = new clsDBAccess(sConnectionString);

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
                // create new ClsFlight class
                clsItem item = new clsItem();

                // fill class with data
                item.ItemCode = ds.Tables[0].Rows[i][0].ToString();
                item.ItemDesc = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                item.Cost = (decimal) ds.Tables[0].Rows[i].ItemArray[2];

                // add flight object to flights list
                ItemsList.Add(item);
            }

            return ItemsList;
        }

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
