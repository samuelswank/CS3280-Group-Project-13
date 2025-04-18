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
using Assignment6;
using GroupProject.Common;
using GroupProject.Items;

namespace GroupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        ExceptionHandler handler = new ExceptionHandler("Error.txt");

        public wndItems()
        {
            InitializeComponent();

            clsItemsLogic itemsLogic = new clsItemsLogic();
            List<clsItem> items = new List<clsItem>();
            items = itemsLogic.GetItems();

            dgItems.ItemsSource = items;
        }

        private void DgItems_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                if (e.PropertyType == typeof(decimal))
                {
                    DataGridTextColumn dgTextColumn = e.Column as DataGridTextColumn;
                    if (dgTextColumn != null) dgTextColumn.Binding.StringFormat = "{0:C}";
                    
                }
            }
            catch (Exception ex)
            {
                handler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name,
                    " -> " + ex.Message);
            }

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
