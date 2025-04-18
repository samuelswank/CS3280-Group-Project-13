using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GroupProject.Common;
using GroupProject.Items;

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        clsItemsLogic itemsLogic;
        List<clsItem> items;

        ExceptionHandler handler = new ExceptionHandler("Error.txt");

        public wndItems()
        {
            InitializeComponent();


            try
            {
                itemsLogic = new clsItemsLogic();
                items = new List<clsItem> ();

                items = itemsLogic.GetItems();
                dgItems.ItemsSource = items;
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name,
                    " -> " + ex.Message);
            }
        }

        private void DgItems_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                string sHeaderName = e.Column.Header.ToString();

                if (sHeaderName == "ItemCode")
                {
                    e.Column.Header = "Item Code";
                    e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
                else if (sHeaderName == "ItemDesc")
                {
                    e.Column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                    e.Column.Header = "Item Description";
                }
                else e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                DataGridTextColumn dgTextColumn = e.Column as DataGridTextColumn;
                dgTextColumn.HeaderStyle = (Style) FindResource("styleDgColHead");

                if (e.PropertyType == typeof(decimal))
                {
                    if (dgTextColumn != null)
                    {
                        dgTextColumn.Binding.StringFormat = "{0:C}";
                        dgTextColumn.CellStyle = (Style) FindResource("styleDgCellDec");
                    }
                }
                else
                {
                    if (dgTextColumn != null) dgTextColumn.CellStyle = (Style) FindResource("styleDgCellStr");   
                }
                
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name,
                    " -> " + ex.Message);
            }
        }

        private void DgItems_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGrid dg = (DataGrid) sender;
            clsItem currItem = (clsItem) dg.CurrentCell.Item;

            if (currItem != null)
            {
                txtBoxItemCode.Text = currItem.ItemCode;
                txtBoxItemDesc.Text = currItem.ItemDesc;
                txtBoxCost.Text = currItem.Cost.ToString();
            }
        }
    }
}
