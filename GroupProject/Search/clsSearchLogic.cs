using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public class clsDBAccess
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
                sConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\Invoice.mdb";
            }

            /// <summary>
            /// This method takes an SQL statment that is passed in and executes it.  The resulting values
            /// are returned in a DataSet.  The number of rows returned from the query will be put into
            /// the reference parameter iRetVal.
            /// </summary>
            /// <param name="sSQL">The SQL statement to be executed.</param>
            /// <param name="iRetVal">Reference parameter that returns the number of selected rows.</param>
            /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
            public DataSet ExecuteSQLStatement(string sSQL, ref int iRetVal)
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
                            // I had to change this in order to access the specific tables
                            adapter.Fill(ds, "Invoices");
                            //adapter.Fill(ds, "Flight_Passenger_Link");
                            //adapter.Fill(ds, "Passenger");
                        }
                    }

                    //

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
        }
    }
}
