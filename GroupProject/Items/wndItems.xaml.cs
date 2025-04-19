using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

using static System.ComponentModel.ListSortDirection;

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

        const string sCurrencyRegex = @"^[\p{Sc}]?\s?\d{1,3}(,\d{3})*(\.\d{2})?$";

        public wndItems()
        {
            InitializeComponent();

            try
            {
                itemsLogic = new clsItemsLogic();
                items = new List<clsItem> ();

                items = itemsLogic.GetItems();
                dgItems.ItemsSource = items;

                dgItems.Items.SortDescriptions.Add(new SortDescription("ItemCode",
                    ListSortDirection.Ascending));
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
        }

        private void WndItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dgItems.SelectedIndex = -1;
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
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
                    e.Column.SortDirection = ListSortDirection.Ascending;
                }
                else if (sHeaderName == "ItemDesc")
                {
                    e.Column.Header = "Item Description";
                    e.Column.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
                }
                else e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                DataGridTextColumn dgTextColumn = e.Column as DataGridTextColumn;
                dgTextColumn.HeaderStyle = (Style) FindResource("styleDgColHead");

                if (e.PropertyType == typeof(decimal))
                {
                    Style styleDgCellDec = FindResource("styleDgCellDec") as Style;
                    if (dgTextColumn != null && styleDgCellDec != null)
                    {
                        dgTextColumn.Binding.StringFormat = "{0:C}";
                        dgTextColumn.CellStyle = styleDgCellDec;
                    }
                }
                else
                {
                    Style styleDgCellStr = FindResource("styleDgCellStr") as Style;
                    if (dgTextColumn != null && styleDgCellStr != null) 
                    {
                        dgTextColumn.CellStyle = styleDgCellStr;
                    }
                }
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
        }

        private void DgItems_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid) sender;
                var currItem = dg.CurrentCell.Item as clsItem;
                if (currItem != null)
                {
                    txtBoxItemCode.Text = currItem.ItemCode;
                    txtBoxItemDesc.Text = currItem.ItemDesc;

                    decimal decCost = currItem.Cost;
                    string sCost = currItem.Cost.ToString();
                    if (decCost == Math.Floor(decCost))
                    {
                        sCost += ".00";
                    }

                    txtBoxCost.Text = sCost;            
                }                
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }

        }

        private void BtnEditItem_Click(object sender, RoutedEventArgs e)
        {
            string sItemCode = string.Empty;
            string sItemDesc = txtBoxItemDesc.Text;
            string sCost = string.Empty;

            try
            {
                if (itemsLogic.ItemInItemDesc(txtBoxItemCode.Text)) sItemCode += txtBoxItemCode.Text;
                if (Regex.IsMatch(txtBoxCost.Text, sCurrencyRegex)) sCost +=  txtBoxCost.Text;


                if (sItemCode != string.Empty && sItemDesc.Length > 0 && sCost != string.Empty)
                {            
                    ClearTextBoxes();
                    itemsLogic.UpdateItemDesc(sItemCode, sItemDesc, sCost);

                    dgItems.ItemsSource = itemsLogic.GetItems();

                    FocusDataGridRow(sItemCode);
                }
                // TODO: Add message so that user know which TextBox Field to fix
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            string sItemCode = string.Empty;
            string sItemDesc = txtBoxItemDesc.Text;
            string sCost = string.Empty;

            try
            {
                if (!itemsLogic.ItemInItemDesc(txtBoxItemCode.Text)) sItemCode += txtBoxItemCode.Text;
                if (Regex.IsMatch(txtBoxCost.Text, sCurrencyRegex)) sCost += txtBoxCost.Text;

                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> { " + sItemCode + ", " + sItemDesc + ", " + sCost + " }");

                if (sItemCode != string.Empty && sItemDesc.Length > 0 && sCost != string.Empty)
                {
                    ClearTextBoxes();
                    itemsLogic.InsertItem(sItemCode, sItemDesc, sCost);

                    dgItems.ItemsSource = itemsLogic.GetItems();

                    FocusDataGridRow(sItemCode);
                }
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }

        }

        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string sItemCode = string.Empty;
                var currItem = dgItems.SelectedItem as clsItem;

                if (currItem != null)
                {
                    sItemCode = currItem.ItemCode;
                    if (!itemsLogic.ItemInLineItems(sItemCode))
                    {
                        itemsLogic.DeleteItem(sItemCode);
                        dgItems.ItemsSource = itemsLogic.GetItems();
                    }
                    // TODO: Add message so that user know which Invoices sItemCode belongs to
                }
                // TODO: Add message so that user know that an item has not been selected
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
        }

        private void BtnMainWindow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearTextBoxes();
                this.Close();
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
        }

        private void FocusDataGridRow(string sItemCode)
        {
            clsItem editedItem = dgItems.Items
                .Cast<clsItem>()
                .Where(item => item.ItemCode == sItemCode).First();

            if (editedItem != null)
            {
                dgItems.ScrollIntoView(editedItem);
                dgItems.SelectedItem = editedItem;
                dgItems.Focus();
            }
        }

        private void ClearTextBoxes()
        {
            try
            {
                txtBoxItemCode.Clear();
                txtBoxItemDesc.Clear();
                txtBoxCost.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
