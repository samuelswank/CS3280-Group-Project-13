using GroupProject.Common;
using GroupProject.Items;
using GroupProject.Search;
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
        private List<clsItem> Items;
        private clsMainLogic getItems;
        
        /// <summary>
        /// The constructor is initialized and also will completely shutdown if user clicks X on top right
        /// </summary>
        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Items = new List<clsItem>();
            getItems = new clsMainLogic();
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
            this.Hide();
            wndSearch wndSearch = new wndSearch();
            wndSearch.ShowDialog();
            this.Show();
            
            //The main window will grab the invoice number from the search window property, and then load it into the datagrid and have it disabled for any edits.

        }
        /// <summary>
        /// When user clicks this button, The items window opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditItems_Click(object sender, RoutedEventArgs e)
        {
            //Once edit items window is hidden, if the value of HasItemBeenChanged is true, then update the items combo box
            this.Hide();
            wndItems wndItem = new wndItems();
            wndItem.ShowDialog();
            this.Show();
        }

        private void LoadItems()
        {
            try
            {
                Items = getItems.AllItems();
                cboItems.ItemsSource = null;
                cboItems.ItemsSource = Items;
                cboItems.DisplayMemberPath = "ItemDesc";
                cboItems.SelectedValuePath = "ItemCode";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cboItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (clsItem)cboItems.SelectedItem;
            itemCost.Content = selectedItem.Cost;
        }
    }
}