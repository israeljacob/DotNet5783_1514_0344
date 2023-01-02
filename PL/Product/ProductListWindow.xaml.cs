﻿using System;
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
using System.Collections.Specialized;
using System.ComponentModel;
using BO;
using System.Collections.ObjectModel;

namespace PL;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    /// <summary>
    /// show of only one bl
    /// </summary>
    BLApi.IBL bl = BLApi.Factory.Get;


    public ObservableCollection<ProductForList> Products
    {
        get { return (ObservableCollection<ProductForList>)GetValue(ProductsProperty); }
        set { SetValue(ProductsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Products.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProductsProperty =
        DependencyProperty.Register("Products", typeof(ObservableCollection<ProductForList>), typeof(Window), new PropertyMetadata(null));



    public ObservableCollection<OrderForList> Orders
    {
        get { return (ObservableCollection<OrderForList>)GetValue(OrdersProperty); }
        set { SetValue(OrdersProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrdersProperty =
        DependencyProperty.Register("Orders", typeof(ObservableCollection<OrderForList>), typeof(Window), new PropertyMetadata(null));


    /// <summary>
    /// add the products to the list in according to combox click
    /// </summary>
    /// <param name="category"></param>
    public ProductListWindow(BO.Category? category= null)
    {
        InitializeComponent();
        
        try
        {
            var tempProduct = bl.Product.GetListOfProducts();
            Products=new ObservableCollection<ProductForList>(tempProduct!); 
            var tempOrder = bl.Order.GetListOfOrders();
            Orders = new ObservableCollection<OrderForList>(tempOrder!);
        }
        catch(Exception ex) { MessageBox.Show(ex.Message); }
        this.CategorySelector.DataContext = Enum.GetValues(typeof(BO.Category));
        ProductListview.DataContext = Products;
        OrderListview.DataContext = Orders;
    }
    
    
    /// <summary>
    /// if the user click on one of the item combox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = sender as ComboBox ?? null!;
        ///if we select somthing
      
        if ((BO.Category)CategorySelector.SelectedItem == BO.Category.all)
        {
            try
            {
                Products = new ObservableCollection<ProductForList>(bl.Product.GetListOfProducts()!);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        else
        {
            Func<BO.ProductForList?, bool> func = product => product?.Category == (BO.Category)CategorySelector.SelectedItem;
            try
            {
                Products = new ObservableCollection<ProductForList>(bl.Product.GetListOfProducts(func)!);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        
        
    }

    /// <summary>
    /// add product button will open new window to add a product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProdct_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(sender).ShowDialog();
        try
        {
            Products = new ObservableCollection<ProductForList>(bl.Product.GetListOfProducts()!);
        }
        catch (Exception ex) { MessageBox.Show(ex.Message); }
    }

    /// <summary>
    /// if the user click on one of the product in the list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductForList ourProduct = (BO.ProductForList)ProductListview.SelectedItem;
         if (ourProduct != null)
         {
            new ProductWindow(sender,ourProduct.UniqID).ShowDialog();
            try
            {
                Products = new ObservableCollection<ProductForList>(bl.Product.GetListOfProducts()!);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
         }
    }

    private void back_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }

    private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.OrderForList ourOrder = (BO.OrderForList)OrderListview.SelectedItem;
        if (ourOrder != null)
        {
            new OrderWindow().ShowDialog();
            try
            {
                Orders = new(bl.Order.GetListOfOrders()!);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
