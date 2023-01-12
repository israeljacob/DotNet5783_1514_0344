using BO;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {

        BLApi.IBL bL = BLApi.Factory.Get;
        public ProductItem ProductItem
        {
            get { return (ProductItem)GetValue(OrderItemProperty); }
            set { SetValue(OrderItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderItemProperty =
            DependencyProperty.Register("ProductItem", typeof(ProductItem), typeof(ProductItemWindow), new PropertyMetadata(null));



        /// <summary>
        /// find the product id to catch it 
        /// </summary>
        /// <param name="productItems"></param>
        /// <param name="id"></param>
        public ProductItemWindow( ObservableCollection<ProductItem> productItems, int id)
        {
            InitializeComponent();
            ProductItem = productItems.FirstOrDefault(pro => pro.UniqID==id)!;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
