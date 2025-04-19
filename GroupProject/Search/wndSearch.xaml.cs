using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
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

namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        clsSearchLogic searchLogic = new clsSearchLogic();
        List<clsInvoice> invoiceList = new List<clsInvoice>();
        public clsInvoice selectedInvoice = new clsInvoice();


        string sSelectedNum = "";
        string sSelectedDate = "";
        string sSelectedCost = "";
        public wndSearch()
        {
            InitializeComponent();

            invoiceList.Clear();
            // call GetSQLStatement inside of the GetInvoice method, it returns a list of invoices
            invoiceList = searchLogic.GetInvoice( searchLogic.GetSQLStatement(sSelectedNum, sSelectedDate, sSelectedCost) );

            // populate comboboxes
            cboInvoiceNumber.ItemsSource = invoiceList;
            cboInvoiceDate.ItemsSource = invoiceList;
            cboInvoiceCost.ItemsSource = invoiceList;

            //populate datagrid
            dgdInvoice.ItemsSource = invoiceList;
        }

        /// <summary>
        /// triggers when the selection is changed for the Invoice Number combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sSelectedNum = cboInvoiceNumber.Text;
        }

        /// <summary>
        /// triggers when the selection is changed for the Invoice Date combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sSelectedDate = cboInvoiceDate.Text;
        }

        /// <summary>
        /// triggers when the selection is changed for the Invoice Cost combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sSelectedCost = cboInvoiceCost.Text;
        }

        /// <summary>
        /// triggers when the select invoice button is clicked, sets selected invoice to current invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            selectedInvoice = (clsInvoice)dgdInvoice.SelectedItem;
            //close window, return to main window
            this.Hide();
        }

        /// <summary>
        /// triggers when the clear filter button is clicked, sets the 3 comboboxes to null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            // set the comboboxes to null
            cboInvoiceNumber.SelectedItem = null;
            cboInvoiceDate.SelectedItem = null;
            cboInvoiceCost.SelectedItem = null;

            //invoiceList.Clear();
            string sSelectedNum = cboInvoiceNumber.Text;
            string sSelectedDate = cboInvoiceDate.Text;
            string sSelectedCost = cboInvoiceCost.Text;

            string sMySQL = searchLogic.GetSQLStatement(sSelectedNum, sSelectedDate, sSelectedCost);

            invoiceList = searchLogic.GetInvoice(sMySQL);
        }

        /// <summary>
        /// triggers when the filter button is clicked, sets the dgdInvoice datagrid 
        /// to the filtered items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            // check the bools, depending on what combination is true, call that sql statement
            //invoiceList.Clear();
            string sSelectedNum = cboInvoiceNumber.Text;
            string sSelectedDate = cboInvoiceDate.Text;
            string sSelectedCost = cboInvoiceCost.Text;

            string sMySQL = searchLogic.GetSQLStatement(sSelectedNum, sSelectedDate, sSelectedCost);

            invoiceList = searchLogic.GetInvoice(sMySQL);

            //populate datagrid
            dgdInvoice.ItemsSource = invoiceList;
        }

        private void dgdInvoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSelectInvoice.IsEnabled = true;
        }
    }
}
