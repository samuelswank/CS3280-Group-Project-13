using System;
using System.Collections.Generic;
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
using GroupProject.Items;

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        // bool itemEdited -> passed to wndMain on submit
        // bool itemEdited = false;

        public wndItems()
        {
            InitializeComponent();

            clsItemsLogic itemsLogic = new clsItemsLogic();
            List<clsItem> items = new List<clsItem>();
            items = itemsLogic.GetItems();

            dgItems.ItemsSource = items;
        }

        private void BtnCancelItems_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSubmitItems_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
