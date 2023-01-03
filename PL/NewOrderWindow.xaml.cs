using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for NewOrderWindow.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        BLApi.IBL bl = BLApi.Factory.Get;
        public ObservableCollection<BO.ProductItem> ProductItems
        {
            get { return (ObservableCollection<BO.ProductItem>)GetValue(ProductItemsProperty); }
            set { SetValue(ProductItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductItemsProperty =
            DependencyProperty.Register("ProductItems", typeof(ObservableCollection<BO.ProductItem>), typeof(Window), new PropertyMetadata(null));


        public NewOrderWindow()
        {
            InitializeComponent();
            try
            {
                var tempProductItem = bl.Product.GetListOfProductItems();
                ProductItems = new(tempProductItem);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            ProductItemListview.DataContext = ProductItems;
        }
    }
}
