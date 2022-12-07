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

namespace PL;

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
        CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if((DO.Category)CategorySelector.SelectedItem==DO.Category.all)
        {
            ProductListview.ItemsSource = bl.Product.GetListOfProducts();

        }
        else
        {
            Func<BO.ProductForList?, bool> func = product => product?.Category == (BO.Category)CategorySelector.SelectedItem;
            ProductListview.ItemsSource = bl.Product.GetListOfProducts(func);
        }
    }

    private void AddProdct_Click(object sender, RoutedEventArgs e)
    {
       new ProductWindow(sender).Show();
        
        this.Close();
    }

    private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductForList ourProduct = (BO.ProductForList)ProductListview.SelectedItem;
         if (ourProduct != null) { new ProductWindow(sender,ourProduct.UniqID).Show();}
         Close();
    }

   
}
