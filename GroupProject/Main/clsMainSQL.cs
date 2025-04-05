using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace GroupProject.Main
{
    public class clsMainSQL
    {
        private string sConnectionString;

        public clsMainSQL()
        {
            sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
        }
        /// <summary>
        /// Updates the total cost of a specific invoice.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="newTotal"></param>
        /// <returns></returns>
        public int UpdateTotal(int invoiceNum, double newTotal)
        {
            string sSQL = $"UPDATE Invoices SET TotalCost = {newTotal} WHERE InvoiceNum = {invoiceNum}";
            return ExecuteNonQuery(sSQL);
        }
        /// <summary>
        /// Inserts a new line item into an invoice.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <param name="lineItemNum"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int InsertLineItems(int invoiceNum, int lineItemNum, string itemCode)
        {
            string sSQL = $"INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES ({invoiceNum}, {lineItemNum}, '{itemCode})'";
            return ExecuteNonQuery(sSQL);
        }
        /// <summary>
        /// Inserts a new invoice.
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="totalCost"></param>
        /// <returns></returns>
        public int InsertInvoice(string invoiceDate, int totalCost)
        {
            string sSQL = $"INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES (#{invoiceDate}#, {totalCost})'";
            return ExecuteNonQuery(sSQL);
        }
        /// <summary>
        /// Gets invoice details by InvoiceNum.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public DataSet GetInvoice(int invoiceNum)
        {
            int iRetVal = 0;
            string sSQL = $"SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = {invoiceNum}";
            return ExecuteSQLStatement(sSQL, ref iRetVal);
        }
        /// <summary>
        /// Gets all available items from ItemDesc table.
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllItem(ref int iRetVal)
        {
            //int iRetVal = 0;
            string sSQL = $"SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
            return ExecuteSQLStatement(sSQL, ref iRetVal);
        }
        /// <summary>
        /// Gets all line items for a specific invoice.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public DataSet GetInvoiceLineItems(int invoiceNum)
        {
            int iRetVal = 0;
            string sSQL = $"SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                          $"FROM LineItems, ItemDesc " +
                          $"WHERE LineItems.ItemCode = ItemDesc.ItemCode " +
                          $"AND LineItems.InvoiceNum = {invoiceNum}";
            return ExecuteSQLStatement(sSQL, ref iRetVal);
        }
        /// <summary>
        /// Deletes all line items from a specific invoice.
        /// </summary>
        /// <param name="invoiceNum"></param>
        /// <returns></returns>
        public int DeleteInvoiceLineItems(int invoiceNum)
        {
            string sSQL = $"DELETE FROM LineItems WHERE InvoiceNum = {invoiceNum}";
            return ExecuteNonQuery(sSQL);
        }



        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter iRetVal.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
        private DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
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

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                iRetVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
        private string ExecuteScalarSQL(string sSQL)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sSQL, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statment that is a non query and executes it.
        /// </summary>
        /// <param name="sSQL">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        private int ExecuteNonQuery(string sSQL)
        {
            try
            {
                //Number of rows affected
                int iNumRows;

                using (OleDbConnection conn = new OleDbConnection(sConnectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sSQL, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    iNumRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return iNumRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
