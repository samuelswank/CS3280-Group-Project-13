﻿using System;
using System.Collections.Generic;
using System.Data;
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
        public wndSearch()
        {
            InitializeComponent();

            clsSearchLogic searchLogic = new clsSearchLogic();
            List<clsInvoice> invoice = new List<clsInvoice>();
            invoice = searchLogic.GetInvoice();

            // populate comboboxes
            cboInvoiceNumber.ItemsSource = invoice;
            cboInvoiceDate.ItemsSource = invoice;
            cboInvoiceCost.ItemsSource = invoice;

            
        }

        // Variables:
        // sSelectedInvoiceID - hold the ID of the currently selected invoice
        // SelectedInvoice - publicly accessable property
        // bool bInvoiceNumIsSelected = false;
        // bool bInvoiceDateIsSelected = false;
        // bool bInvoiceCostIsSelected = false;


        /// <summary>
        /// triggers when the selection is changed for the Invoice Number combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // bool bInvoiceNumIsSelected = true;
        }

        /// <summary>
        /// triggers when the selection is changed for the Invoice Date combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // bool bInvoiceDateIsSelected = true;
        }

        /// <summary>
        /// triggers when the selection is changed for the Invoice Cost combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboInvoiceCost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // bool bInvoiceCostIsSelected = true;
        }

        /// <summary>
        /// triggers when the select invoice button is clicked, sets selected invoice to current invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
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
        }
    }
}
