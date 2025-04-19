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
        /// <summary>
        /// Handles business logic for Window wndItems
        /// </summary>
        clsItemsLogic itemsLogic;
        /// <summary>
        /// Functions as ItemsSource for DataGrid
        /// </summary>
        List<clsItem> items;
        /// <summary>
        /// Stores LineItem InvoiceNums, ensures that no Item is deleted for which an Invoice currently exists
        /// </summary>
        List<int> lineItemInvoiceNums;
        /// <summary>
        /// ExceptionHandler instance, outputs errors to an external test file to aid in debugging.
        /// </summary>
        ExceptionHandler handler = new ExceptionHandler("Error.txt");
        /// <summary>
        /// Regular Expression string for validationg currency values entered in txtBoxCost
        /// </summary>
        const string sCurrencyRegex = @"^[\p{Sc}]?\s?\d{1,3}(,\d{3})*(\.\d{2})?$";

        /// <summary>
        /// Constructor for Window wndItems, Initializes components
        /// </summary>
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

        /// <summary>
        /// Event Handler for when empty space on the form is DoubleClicked, deselects DataGrid Item, clears
        /// error messages and TextBoxers
        /// </summary>
        /// <param name="sender">Window wndItems</param>
        /// <param name="e">MouseDoubleClick</param>
        private void WndItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dgItems.SelectedIndex = -1;
                lblErrorMsg.Content = string.Empty;
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Event Handler which sets Column Format for DataGrid dgItems
        /// </summary>
        /// <param name="sender">DataGrid dgItems</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Event Handler which defines behavior when the CurrentCell is changed in DataGrid dgItems, populates
        /// TextBoxes with the appropriate data
        /// </summary>
        /// <param name="sender">DataGrid dgItems</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Defines behavior when Button BtnEditItem is Clicked, edits selected Item or diplays error message
        /// </summary>
        /// <param name="sender">Button BtnEditItem</param>
        /// <param name="e">On Click</param>
        private void BtnEditItem_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMsg.Content = string.Empty;

            string sItemCode = string.Empty;
            string sItemDesc = txtBoxItemDesc.Text;
            string sCost = string.Empty;

            try
            {
                if (itemsLogic.ItemInItemDesc(txtBoxItemCode.Text)) sItemCode += txtBoxItemCode.Text;
                else lblErrorMsg.Content += "Item Code " + txtBoxItemCode.Text + " not in database.\n";

                if (sItemDesc.Length == 0) lblErrorMsg.Content += "Item Description is empty.\n";

                if (Regex.IsMatch(txtBoxCost.Text, sCurrencyRegex)) sCost +=  txtBoxCost.Text;
                else lblErrorMsg.Content += "Cost " + txtBoxCost.Text + " not a valid currency value.";


                if (sItemCode != string.Empty && sItemDesc.Length > 0 && sCost != string.Empty)
                {
                    lblErrorMsg.Content = string.Empty;
                    ClearTextBoxes();
                    itemsLogic.UpdateItemDesc(sItemCode, sItemDesc, sCost);

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

        /// <summary>
        /// Event Handler which defines behavior when Button BtnAddItem is Clicked, adds Item to Database if
        /// it does not already and if new data are valid
        /// </summary>
        /// <param name="sender">Button BtnAddItem</param>
        /// <param name="e">On Click</param>
        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMsg.Content = string.Empty;

            string sItemCode = string.Empty;
            string sItemDesc = txtBoxItemDesc.Text;
            string sCost = string.Empty;

            try
            {
                if (!itemsLogic.ItemInItemDesc(txtBoxItemCode.Text)) sItemCode += txtBoxItemCode.Text;
                else lblErrorMsg.Content += "Item Code " + txtBoxItemCode.Text + " already in database.\n";

                if (sItemDesc.Length == 0) lblErrorMsg.Content += "Item Description is empty.\n";

                if (Regex.IsMatch(txtBoxCost.Text, sCurrencyRegex)) sCost += txtBoxCost.Text;
                else lblErrorMsg.Content += "Cost " + txtBoxCost.Text + " not a valid currency value.";

                if (sItemCode != string.Empty && sItemDesc.Length > 0 && sCost != string.Empty)
                {
                    lblErrorMsg.Content = string.Empty;
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

        /// <summary>
        /// Event Handler which defines behavior when Button BtnDeleteItem is Clicked, deletes item, generates
        /// error message, or Stop MessageBox
        /// </summary>
        /// <param name="sender">Button BtnDeleteItem</param>
        /// <param name="e">On Click</param>
        private void BtnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            lblErrorMsg.Content = string.Empty;

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
                    else
                    {
                        lineItemInvoiceNums = itemsLogic.GetLineItemInvoiceNums(sItemCode);

                        string lineItemInvoiceNumsMsg = "Cannot Delete Item with Item Code " + sItemCode +
                            " because it belongs to the Invoice Numbers:\n";

                        for (int i = 0; i < lineItemInvoiceNums.Count; ++i)
                        {
                            lineItemInvoiceNumsMsg += '\t' + lineItemInvoiceNums[i].ToString() + '\n';
                        }

                        const string sCaption = "Cannot Delete Item";

                        MessageBox.Show(lineItemInvoiceNumsMsg, sCaption, MessageBoxButton.OK,
                            MessageBoxImage.Stop);

                        lineItemInvoiceNums.Clear();
                    }
                }
                else lblErrorMsg.Content = "No DataGrid Item selected.";
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Event Handler which defines behavior when Button BtnMainWindow is Clicked, returns user to
        /// MainWindow
        /// </summary>
        /// <param name="sender">Button BtnMainWindow</param>
        /// <param name="e">On Click</param>
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

        /// <summary>
        /// Helper method which puts the Focus on the DataGridRow record which has just been Inserted or
        /// Updated
        /// </summary>
        /// <param name="sItemCode">Item Code Column value for Item to be Selected</param>
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

        /// <summary>
        /// Helper Method for Clearing TextBoxes in form
        /// </summary>
        /// <exception cref="Exception">Generic Exception</exception>
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
