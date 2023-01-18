using BO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.ComponentModel;

namespace PL;

/// <summary>
/// Interaction logic for NewOrderWindow.xaml
/// </summary>
public partial class NewOrderWindow : Window
{
    BLApi.IBL bl = BLApi.Factory.Get();

    private string groupName = "Category";
    PropertyGroupDescription propertyGroupDescription;
    public ICollectionView CollectionViewProductItemList { set; get; }
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

       ObservableCollection<ProductItem> products = new();
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
        CollectionViewProductItemList = CollectionViewSource.GetDefaultView(ProductItems);

        propertyGroupDescription = new PropertyGroupDescription(groupName);
        CollectionViewProductItemList.GroupDescriptions.Add(propertyGroupDescription);
    }

    private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ComboBox comboBox = sender as ComboBox ?? null!;
        ///if we select somthing
        foreach (ProductItem item in ProductItems)
        {
            bool flag = false;
            foreach (ProductItem it in products)
            {
                if (item.UniqID == it.UniqID)
                {
                    it.Amount = item.Amount;
                    flag = true;
                }
            }
            if (!flag)
                products.Add(item);
        }

        if ((BO.Category)(sender as ComboBox)?.SelectedItem! == BO.Category.all)
        {
            try
            {
                ProductItems = new(bl.Product.GetListOfProductItems()!);
                foreach (ProductItem item in products)
                    foreach (ProductItem it in ProductItems)
                    {
                        if(item.UniqID == it.UniqID)
                            it.Amount = item.Amount;
                    }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        else
        {
            Func<BO.ProductItem?, bool> func = product => product?.Category == (BO.Category)(sender as ComboBox)?.SelectedItem!;
            try
            {
                ProductItems = new(bl.Product.GetListOfProductItems(func)!);
                foreach (ProductItem item in products)
                    foreach (ProductItem it in ProductItems)
                    {
                        if (item.UniqID == it.UniqID)
                            it.Amount = item.Amount;
                    }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }

    private void back_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
    /// <summary>
    /// to see all the item the customer choose
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cart_Click(object sender, RoutedEventArgs e)
    {
        BO.Cart MyCart = new BO.Cart();
        MyCart.CustomerName = "";
        MyCart.CustomerEmail = "";
        MyCart.CustomerAdress = "";
        MyCart.OrderItems = new();
        foreach (ProductItem item in ProductItems)
        {
            if (item != null && item.Amount > 0)
            {
                try
                {
                    bl.Cart.AddOrUpdateCart(MyCart, item);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        new CartWindow(MyCart).ShowDialog();
    }
    /// <summary>
    /// if we want to add item/product press on the @+@ button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        ObservableCollection<ProductItem> products = ProductItems;
        int id = ((ProductItem)((Button)sender).DataContext).UniqID;
        foreach (var item in products)
        {
            if (item.UniqID == id)
                item.Amount++;
        }
        ProductItems = new(from ProductItem productItems in products
                           where productItems != null
                           select productItems);
    }
    /// <summary>
    /// if we want to substruct item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReduceButton_Click(object sender, RoutedEventArgs e)
    {
        ObservableCollection<ProductItem> products = ProductItems;
        int id = ((ProductItem)((Button)sender).DataContext).UniqID;
        foreach (var item in products)
        {
            if (item.UniqID == id && item.Amount > 0)
                item.Amount--;
        }
        ProductItems = new(from ProductItem productItems in products
                           where productItems != null
                           select productItems);
    }
    /// <summary>
    /// whenever  we press double time on the list view we call this function
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        BO.ProductItem ourProduct = (BO.ProductItem)(sender as ListView)?.SelectedItem!;
        new ProductItemWindow(ProductItems, ourProduct.UniqID).ShowDialog();
    }
}
