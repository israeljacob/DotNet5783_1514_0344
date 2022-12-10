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
    /// <summary>
    /// show of only one bl
    /// </summary>
    private IBL bl = new Bl();
    BO.Category removedItem = BO.Category.all;

    /// <summary>
    /// add the products to the list in according to combox click
    /// </summary>
    /// <param name="category"></param>
    public ProductListWindow(BO.Category? category= null)
    {
        InitializeComponent();
        try
        {
            ProductListview.ItemsSource = bl.Product.GetListOfProducts();
        }
        catch(Exception ex) { MessageBox.Show(ex.Message); }
        List<BO.Category> categories = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>().ToList();
        foreach (BO.Category category1 in categories)
            if(category1 != BO.Category.all)
                CategorySelector.Items.Add(category1);
    }
    
    /// <summary>
    /// if the user click on one of the item combox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ///if we select somthing
        if (CategorySelector.Items.Count > 0 && CategorySelector.SelectedItem != null)
        {
            if ((BO.Category)CategorySelector.SelectedItem==BO.Category.all)
            {
                try
                {
                    ProductListview.ItemsSource = bl.Product.GetListOfProducts();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                Func<BO.ProductForList?, bool> func = product => product?.Category == (BO.Category)CategorySelector.SelectedItem;
                try
                {
                    ProductListview.ItemsSource = bl.Product.GetListOfProducts(func);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

            CategorySelector.Items.Add(removedItem);
            removedItem = (BO.Category)CategorySelector.SelectedItem;
            CategorySelector.Items.Remove(removedItem);
        }


    }

    /// <summary>
    /// add product button will open new window to add a product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProdct_Click(object sender, RoutedEventArgs e)
    {
       new ProductWindow(sender).Show();
        
        this.Close();
    }

    /// <summary>
    /// if the user click on one of the product in the list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductForList ourProduct = (BO.ProductForList)ProductListview.SelectedItem;
         if (ourProduct != null) { new ProductWindow(sender,ourProduct.UniqID).Show();}
    }

   
}
