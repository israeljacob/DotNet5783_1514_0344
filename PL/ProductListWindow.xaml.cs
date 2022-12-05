using BLApi;
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
using BlImplementation;
using System.Reflection;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        
        private IBL bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListview.ItemsSource = bl.Product.GetListOfProducts();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(DO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((DO.Category)CategorySelector.SelectedItem==DO.Category.all)
            {
                ProductListview.ItemsSource = bl.Product.GetListOfProducts();

            }
            else
            {
                InitializeComponent();
                Func<DO.Product?, bool> func = product => product?.Category == (DO.Category)CategorySelector.SelectedItem;
                ProductListview.ItemsSource = bl.Product.GetListOfProducts(func);
            }
        }

        private void AddProdct_Click(object sender, RoutedEventArgs e)
        {
           new ProductWindow().Show();
        }
    }
}
