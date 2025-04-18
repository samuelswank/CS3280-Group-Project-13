using System;
using System.Collections.Generic;
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

        bool DgItems_CurrentCellChangedEnabled;

        const string sCurrencyRegex = @"^(\$)?(([1 - 9]\d{0,2}(\,\d{3})*)| ([1 - 9]\d *)| (0))(\.\d{2})?$";


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
                    Style styleDgCellDec = FindResource("styleDgCellDec") as Style;
                    if (dgTextColumn != null)
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

                DgItems_CurrentCellChangedEnabled = true;
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
                if (DgItems_CurrentCellChangedEnabled == true)
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
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }

        }

        private void BtnEditItem_Click(object sender, RoutedEventArgs e)
        {
            DgItems_CurrentCellChangedEnabled = false;

            string sItemCode = null;
            string sItemDesc = txtBoxItemDesc.Text;
            string sCost = null;

            try
            {
                int iNumRows = itemsLogic.GetItemCodeCount(txtBoxItemCode.Text);
                if (iNumRows > 0) sItemCode = txtBoxItemCode.Text;
                if (Regex.IsMatch(txtBoxCost.Text, sCurrencyRegex)) sCost = '$' + txtBoxCost.Text;

                if (sItemCode != null && sItemDesc.Length > 0 && sCost != null)
                {
                    handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                        MethodInfo.GetCurrentMethod().Name, " -> " + sItemCode + ' ' + sItemDesc + ' ' + sCost);

                    ClearTextBoxes();
                    dgItems.Items.Clear();
                    itemsLogic.UpdateItemDesc(sItemCode, sItemDesc, sCost);
                    dgItems.ItemsSource = itemsLogic.GetItems();
                }
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, " -> " + ex.Message);
            }
            finally
            {
                DgItems_CurrentCellChangedEnabled = false;
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
