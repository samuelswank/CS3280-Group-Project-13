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
        public class clsDBAccess // Will be merged with clsDBAccess in clsSearchLogic at a later stage
        {
            /// <summary>
            /// Connection string to the database.
            /// </summary>
            private string sConnectionString;

            /// <summary>
            /// Constructor that sets the connection string to the database
            /// </summary>
            public clsDBAccess()
            {
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() +
                    "\\Invoice.mdb";
            }

            /// <summary>
            /// This method takes an SQL statment that is passed in and executes it.  The resulting values
            /// are returned in a DataSet.  The number of rows returned from the query will be put into
            /// the reference parameter iRetVal.
            /// </summary>
            /// <param name="sSQL">The SQL statement to be executed.</param>
            /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
            /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
            public DataSet ExecuteSQLStatement(string tableName, string sSQL, ref int iRetVal)
            {
                try
                {
                    //Create a new DataSet
                    DataSet ds = new DataSet();

                    using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                    {

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                        {

                            //Open the connection to the database
                            conn.Open();

                            //Add the information for the SelectCommand using the SQL statement and the connection object
                            adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                            adapter.SelectCommand.CommandTimeout = 0;

                            // Fill up the DataSet with data
                            // I had to change this in order to access the specific tables
                            // tableName allows the user to access both ItemsDesc and LineItems Tables
                            adapter.Fill(ds, tableName);
                        }
                    }

                    //Set the number of values returned
                    iRetVal = ds.Tables[0].Rows.Count;

                    //return the DataSet
                    return ds;
                }
                catch (Exception ex)
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                        MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                }
            }
        }

        public List<clsItem> GetItems()
        {
            clsDBAccess db;
            db = new clsDBAccess();

            string sSQL = clsItemsSQL.GetItems();

            List<clsItem> ItemsList = new List<clsItem>();

            DataSet ds;

            int iRet = 0;

            try
            {
                ds = db.ExecuteSQLStatement("ItemsDesc", sSQL, ref iRet);
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
                item.Cost = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                // add flight object to flights list
                ItemsList.Add(item);
            }

            return ItemsList;
        }

        public List<string> GetLineItemInvoiceNums(string sItemCode)
        {
            clsDBAccess db;
            db = new clsDBAccess();

            string sSQL = clsItemsSQL.GetLineItemInvoiceNums(sItemCode);

            List<string> ItemInvoiceNumsList = new List<string>();

            DataSet ds;

            int iRet = 0;

            try
            {
                ds = db.ExecuteSQLStatement("ItemsDesc", sSQL, ref iRet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
            {
                ItemInvoiceNumsList.Add(ds.Tables[0].Rows[i][0].ToString());
            }

            return ItemInvoiceNumsList;
        }

        // TODO: Implement UpdateItemDesc, InsertItem, and DeleteItem
        //  NOTE: This requires another method to be added to the
        //        clsDBAccess class that will address UPDATE, INSERT, and
        //        DELETE operations...likely returning a bool indicating
        //        whether the operation was successfull or not
    }
}
