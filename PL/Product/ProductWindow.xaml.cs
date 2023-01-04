using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Collections.ObjectModel;

namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    /// <summary>
    /// show of only one bl
    /// </summary>
    private static readonly BLApi.IBL bl = BLApi.Factory.Get;
    public BO.Product Product
    {
        get { return (BO.Product)GetValue(ProductProperty); }
        set { SetValue(ProductProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProductProperty =
        DependencyProperty.Register("Product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));

    public Array Categories
    {
        get { return (Array)GetValue(CategoriesProperty); }
        set { SetValue(CategoriesProperty, value); }
    }
    // Using a DependencyProperty as the backing store for Categories.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CategoriesProperty =
        DependencyProperty.Register("Categories", typeof(Array), typeof(ProductWindow), new PropertyMetadata(null));

    public bool myIDCheck
    {
        get { return (bool)GetValue(myIDCheckProperty); }
        set { SetValue(myIDCheckProperty, value); }
    }

    // Using a DependencyProperty as the backing store for myIDCheck.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty myIDCheckProperty =
        DependencyProperty.Register("myIDCheck", typeof(bool), typeof(ProductWindow), new PropertyMetadata(false));

    public bool myNameCheck
    {
        get { return (bool)GetValue(myNameCheckProperty); }
        set { SetValue(myNameCheckProperty, value); }
    } 

    // Using a DependencyProperty as the backing store for myNameCheck.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty myNameCheckProperty =
        DependencyProperty.Register("myNameCheck", typeof(bool), typeof(ProductWindow), new PropertyMetadata(false));

    public bool myPriceCheck
    {
        get { return (bool)GetValue(myPriceCheckProperty); }
        set { SetValue(myPriceCheckProperty, value); }
    }

    // Using a DependencyProperty as the backing store for myPriceCheck.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty myPriceCheckProperty =
        DependencyProperty.Register("myPriceCheck", typeof(bool), typeof(ProductWindow), new PropertyMetadata(false));
    public bool myCategoryCheck
    {
        get { return (bool)GetValue(myCategoryCheckProperty); }
        set { SetValue(myCategoryCheckProperty, value); }
    }

    // Using a DependencyProperty as the backing store for myCategoryCheck.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty myCategoryCheckProperty =
        DependencyProperty.Register("myCategoryCheck", typeof(bool), typeof(ProductWindow), new PropertyMetadata(false));

    public bool myInStockCheck
    {
        get { return (bool)GetValue(myInStockCheckProperty); }
        set { SetValue(myInStockCheckProperty, value); }
    }

    // Using a DependencyProperty as the backing store for myInStockCheck.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty myInStockCheckProperty =
        DependencyProperty.Register("myInStockCheck", typeof(bool), typeof(ProductWindow), new PropertyMetadata(false));


    public ProductWindow(int id = 0)
    {
        InitializeComponent();
        if (id != 0)
            Product = bl.Product.ProductItemForManagger(id);
        else
        {
            BO.Product product = new();
            product.Category = BO.Category.all;
            Product = product;
        }
        Categories = Enum.GetValues(typeof(BO.Category));
        
    }
   
    /// <summary>
    /// one function to see if we press "add" or "update" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_or_UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        Button? button = sender as Button;
        var bc = new BrushConverter();
        bool flag = true;
        //MessageBox.Show(Product.Price.ToString());
        ///to convert it to letters , if not make sure the user will see it
        if ( Product.UniqID==0)
        {
            myIDCheck = true;
            flag = false;
        }
        else
        {
            myIDCheck = false;
        }
        if (Product.Name == "" || !Regex.IsMatch(Product.Name!, "^[a-zA-Z]"))
        {
            myNameCheck= true;
            flag = false;
        }
        else
        {
            myNameCheck = false;
        }
        ///to convert it to double, if not make sure the user will see it
        if (Product.Price == 0|| price.Text == "")
        {
            myPriceCheck = true;
            flag = false;
        }
        else
        {
            myPriceCheck = false;
        }
        ///to convert it to int, if not make sure the user will see it
        if (Product.InStock == 0 || inStock.Text == "")
        {
            myInStockCheck= true;
            flag = false;
        }
        else
        {
            myInStockCheck = false;
        }
        //to see if the user chosed somthing on the combox, if not make sure the user will see it
        if (Product.Category == BO.Category.all)
        {
            myCategoryCheck= true;
            flag = false;
        }
        else
        {
            myCategoryCheck = false;
        }
        if (flag)
        {
            try
            {
                if (button?.Content.ToString() == "ADD")
                    bl.Product.AddProduct(Product.UniqID, Product.Name!, Product.Price, (BO.Category)Product.Category!,Product.InStock);
                else
                    bl.Product.UpdateProduct(Product);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }
    }
    /// <summary>
    /// Go back to the last window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void back_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}


