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
    List<BO.Category> categories = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>().ToList();
    BO.Category removedItem = BO.Category.all;
    public ProductListWindow()
    {
        InitializeComponent();
        ProductListview.ItemsSource = bl.Product.GetListOfProducts();
        categories.Remove(BO.Category.all);
        CategorySelector.ItemsSource = categories;
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        categories.Add(removedItem);
        categories.Remove((BO.Category)CategorySelector.SelectedItem);
        CategorySelector.ItemsSource = categories;
        if ((BO.Category)CategorySelector.SelectedItem==BO.Category.all)
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
         if (ourProduct != null) { new ProductWindow(sender).Show();}
    }

    //private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{

    //}

    //private void ProductListview_Selected(object sender, RoutedEventArgs e)
    //{

    //}

    //private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //{
    //    BO.Product ourProduct = (Product)ProductListview.SelectedItem;
    //   // if (ourProduct != null) { new ProductWindow().Show(); }
    //}
}
