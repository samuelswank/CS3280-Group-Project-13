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
    internal class clsItemsSQL
    {
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

        public static string GetLineItemInvoiceNums(string sItemCode)
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceNum) FROM LineItems WHERE ItemCode = " + sItemCode + ';';
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

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

        public static string InsertItem(string sItemCode, string sItemDesc, string sCost)
        {
            try
            {
                string sSQL = "INSERT INTO ItemDesc(ItemCode, ItemDesc, Code) Values(" + sItemCode + ", "
                    + sItemDesc + ", " + sCost + ");";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string DeleteItem(string sItemCode)
        {
            try
            {
                string sSQL = "DELETE FROM ItemDesc WHERE ItemCode = " + sItemCode + ';';
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string GetItemCodeCount(string sItemCode)
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
    }
}
