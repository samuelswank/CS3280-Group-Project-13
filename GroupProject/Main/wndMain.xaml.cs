//using Assignment6; commented out as it was causing errors for me -Ben
using GroupProject.Common;
using GroupProject.Items;
using GroupProject.Search;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupProject.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        /// <summary>
        /// creates a item list
        /// </summary>
        private List<clsItem> Items;
        /// <summary>
        /// creates a main logic object
        /// </summary>
        private clsMainLogic mainLogic;
        /// <summary>
        /// creates a invoice object
        /// </summary>
        private clsInvoice invoice;
        /// <summary>
        /// creates a invoice list
        /// </summary>
        private List<clsInvoice> Invoice;
        /// <summary>
        /// creates a selected item object
        /// </summary>
        private clsItem selectedItem;




        /// <summary>
        /// The constructor is initialized and also will completely shutdown if user clicks X on top right
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Items = new List<clsItem>();
            mainLogic = new clsMainLogic();
            Invoice = new List<clsInvoice>();

            LoadItems();
        }

        /// <summary>
        /// When user clicks exit inside file menu control, the program closes entirely
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        /// <summary>
        /// When user clicks this button, the search window opens up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                wndSearch wndSearch = new wndSearch();
                wndSearch.ShowDialog();

                // changed to selected invoice -Ben
                invoice = (clsInvoice)wndSearch.selectedInvoice;
                this.Show();

                //Adding this to stop an error -Ben
                if (invoice.InvoiceID != null)
                {
                    LoadInvoice(invoice.InvoiceID);
                }
                
                
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }



            //The main window will grab the invoice number from the search window property, and then load it into the datagrid and have it disabled for any edits.

        }
        /// <summary>
        /// When user clicks this button, The items window opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                wndItems wndItem = new wndItems();
                wndItem.ShowDialog();
                this.Show();
                LoadItems();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            //Once edit items window is hidden, if the value of HasItemBeenChanged is true, then update the items combo box

        }
        /// <summary>
        /// Loads items into the combobox, will check if the items have been updated or not
        /// </summary>
        private void LoadItems()
        {
            try
            {
                Items = mainLogic.AllItems();
                cboItems.ItemsSource = null;
                cboItems.ItemsSource = Items;
                cboItems.DisplayMemberPath = "ItemDesc";
                cboItems.SelectedValuePath = "ItemCode";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Loads the invoice into the datagrid, and will also load the invoice number and date into the labels
        /// </summary>
        /// <param name="InvoiceID"></param>
        private void LoadInvoice(string InvoiceID)
        {
            try
            {
                Items = mainLogic.GetInvoice(InvoiceID);
                invoiceDataGrid.ItemsSource = Items;
                invoice.InvoiceCost = mainLogic.AddTotalCost(Items).ToString();
                totalCostlbl.Content = "Total: " + invoice.InvoiceCost;
                invoiceDate.Content = invoice.InvoiceDate;
                invoiceNum.Content = invoice.InvoiceID;
                enableControls();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// When the user selects an item from the combobox, it will display the cost of the item in the label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // moved this here as if it was pressed when no item selected the program would crash -Ben
                addBtn.IsEnabled = true;

                selectedItem = (clsItem)cboItems.SelectedItem;
                itemCost.Content = selectedItem.Cost;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }
        /// <summary>
        /// When the user clicks the add button, it will add the selected item to the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               

                selectedItem = (clsItem)cboItems.SelectedItem;
                int lineItemNumber = invoiceDataGrid.Items.Count + 1;
                mainLogic.InsertLineItem(invoice.InvoiceID, lineItemNumber.ToString(), selectedItem.ItemCode);
                LoadInvoice(invoice.InvoiceID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }


        }
        /// <summary>
        /// When the user clicks the remove button, it will remove the selected item from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(selectedItem == null)
                {
                    errorlbl.Content = ("Please select an item to remove.");
                    return;
                }
                selectedItem = (clsItem)invoiceDataGrid.SelectedItem;
                mainLogic.DeleteLineItem(invoice.InvoiceID, selectedItem.ItemCode);
                LoadInvoice(invoice.InvoiceID);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }
        /// <summary>
        /// enables the controls for editing the invoice
        /// </summary>
        private void enableControls()
        {
            try
            {
                editInvoicebtn.IsEnabled = true;
                saveInvoicebtn.IsEnabled = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        /// <summary>
        /// When the user clicks the save button, it will update the invoice with the new total cost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveInvoicebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainLogic.UpdateTotalCost(invoice.InvoiceID, float.Parse(invoice.InvoiceCost));
                lockUI();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }


        }
        /// <summary>
        /// When the user clicks the edit button, it will enable the controls for editing the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editInvoicebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // diabling until an item is selected in the dropdown menu, if pressed when no item is selected a crash occurs -Ben
                //addBtn.IsEnabled = true;

                // diabling until an item is selected, if pressed when no item is selected a crash occurs -Ben
                //removeBtn.IsEnabled = true;
                cboItems.IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

        }
        /// <summary>
        /// locks the UI controls for editing the invoice
        /// </summary>
        private void lockUI()
        {
            try
            {
                editInvoicebtn.IsEnabled = false;
                saveInvoicebtn.IsEnabled = false;
                addBtn.IsEnabled = false;
                removeBtn.IsEnabled = false;
                cboItems.IsEnabled = false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        /// <summary>
        /// When the user clicks the create invoice button, it will show the date input box and the create button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createInvoicebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                createInvoiceDatetxt.Visibility = Visibility.Visible;
                newdatebtn.Visibility = Visibility.Visible;
                EnterDate.Visibility = Visibility.Visible;
                selectedItem = null;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        /// <summary>
        /// When the user clicks the create button, it will create a new invoice with the date entered in the input box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newdatebtn_Click(object sender, RoutedEventArgs e)
        {
            // hide the UI prompts
            createInvoiceDatetxt.Visibility = Visibility.Hidden;
            newdatebtn.Visibility = Visibility.Hidden;
            EnterDate.Visibility = Visibility.Hidden;

            try
            {
                string input = createInvoiceDatetxt.Text.Trim();

                // 1) Empty check
                if (string.IsNullOrEmpty(input))
                {
                    MessageBox.Show(
                        "Please enter a date",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    return;
                }

                
                DateTime parsedDate;
                string[] formats = { "MM/dd/yyyy" };
                if (!DateTime.TryParseExact(
                        input,
                        formats,
                        CultureInfo.CurrentCulture,
                        DateTimeStyles.None,
                        out parsedDate))
                {
                    MessageBox.Show(
                        "Invalid date format.\nPlease enter a valid date like MM/dd/yyyy.",
                        "Error",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                    createInvoiceDatetxt.Focus();
                    createInvoiceDatetxt.SelectAll();
                    return;
                }

                
                mainLogic.InsertInvoice(parsedDate.ToString("yyyy-MM-dd"));

                
                LoadItems();
                invoiceDate.Content = parsedDate.ToString("MM/dd/yyyy");
                invoiceNum.Content = "TBD";
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }
        /// <summary>
        /// Handles exceptions and displays an error message to the user
        /// </summary>
        /// <param name="ex"></param>
        private void HandleException(Exception ex)
        {
            string message = $"An error occured:\n{ex.Message}";
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void invoiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  Added this to stop an error -Ben
            removeBtn.IsEnabled = true;
        }
    }
}