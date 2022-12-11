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
using System.Windows.Controls.Primitives;

namespace PL;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    
    private IBL bl = new Bl();
    BO.Category removedItem = BO.Category.all;
    
    public ProductListWindow()
    {
        InitializeComponent();
        try
        {
            ProductListview.ItemsSource = bl.Product.GetListOfProducts();
        }
        catch(Exception ex) { MessageBox.Show(ex.Message); }
        List<BO.Category> categories = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>().ToList();
        foreach (BO.Category category1 in categories)
            if (category1 != BO.Category.all)
                CategorySelector.Items.Add(category1);
    }
    
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
        if ( CategorySelector.SelectedItem != null)
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
            string text = CategorySelector.SelectedItem.ToString();
            CategorySelector.Items.Add(removedItem);
            removedItem = (BO.Category)CategorySelector.SelectedItem;
            CategorySelector.Items.Remove(removedItem);
            CategorySelector.Text= text;
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
    }

   
}
