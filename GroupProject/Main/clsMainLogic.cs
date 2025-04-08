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
        private clsMainSQL db;
        
        public clsMainLogic()
        {
            db = new clsMainSQL();
        }

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
                    Cost = row["Cost"].ToString()
                };

                AllItems.Add(item);
            }



            return AllItems;
        }

        //public List<clsInvoice> AllInvoices()
        //{
        //    int invoices = 0;
        //    DataSet ds = db.
        //}

        
    }
}
