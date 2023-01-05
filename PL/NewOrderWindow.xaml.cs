using BO;
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
            DependencyProperty.Register("ProductItems", typeof(ObservableCollection<BO.ProductItem>), typeof(NewOrderWindow), new PropertyMetadata(null));



        public Array Categories
        {
            get { return (Array)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Categories.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoriesProperty =
            DependencyProperty.Register("Categories", typeof(Array), typeof(NewOrderWindow), new PropertyMetadata(null));


        public NewOrderWindow()
        {
            InitializeComponent();
            try
            {
                var tempProductItem = bl.Product.GetListOfProductItems();
                ProductItems = new(tempProductItem);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            Categories = Enum.GetValues(typeof(BO.Category));
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox ?? null!;
            ///if we select somthing

            if ((BO.Category)CategorySelector.SelectedItem == BO.Category.all)
            {
                try
                {
                    ProductItems = new (bl.Product.GetListOfProductItems()!);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                Func<BO.ProductItem?, bool> func = product => product?.Category == (BO.Category)CategorySelector.SelectedItem;
                try
                {
                    ProductItems = new(bl.Product.GetListOfProductItems(func)!);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void ProductItemListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.ProductItem ourProduct = (BO.ProductItem)ProductItemListview.SelectedItem;
            if (ourProduct != null)
            {
                new ProductItemWindow(ProductItems, ourProduct.UniqID).ShowDialog();
                try
                {
                    ProductItems = new(bl.Product.GetListOfProductItems()!);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}
