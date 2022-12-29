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
    public static readonly DependencyProperty ProductDependency = DependencyProperty.Register(nameof(Product), typeof(BO.Product), typeof(Window));
    public BO.Product? Product { get => (BO.Product)GetValue(ProductDependency); private set => SetValue(ProductDependency, value); }

    public ProductWindow(object sender, int id=0)
    {
        if(id!=0)
            Product = bl.Product.ProductItemForManagger(id);
        InitializeComponent();
        ///get the enums to combobox
        List<BO.Category> categories = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>().ToList();
        foreach (BO.Category category in categories)
            if (category != BO.Category.all)
                CategoryBox.Items.Add(category);///add in to the opened list
        Button? button = sender as Button;
        ///to see if we press on update or add button
        if (button != null)
            UpdateButton.Visibility = Visibility.Hidden;
        else
        {
            AddButton.Visibility = Visibility.Hidden;
            idtxt.IsEnabled = false;
        }

    }
    /// <summary>
    /// check evrey type of the user and make sure only numbers will take place in the box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void int_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        ///Only numbers..
        Regex regex = new("^[0-9]+");
        e.Handled= !regex.IsMatch(e.Text);
    }
    /// <summary>
    /// check evrey type of the user and make sure only numbers and dote (for the price) will take place in the box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void double_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        ///Only numbers with period
        Regex regex = new("^[0-9.]+");
        e.Handled = !regex.IsMatch(e.Text);
    }
    private void name_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new("^[A-Z,a-z]+[0-9]*");
        e.Handled = !regex.IsMatch(e.Text);
    }
    /// <summary>
    /// one function to see if we press "add" or "update" button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_or_UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        var bc = new BrushConverter();
        bool flag = true;
        ///to convert it to letters , if not make sure the user will see it
        if (!int.TryParse(idtxt.Text, out int id))
        {
            idtxtMsg.Visibility = Visibility.Visible;
            flag = false;
        }
        else
        {
            idtxtMsg.Visibility = Visibility.Hidden;
        }
        if (name?.Text == "" || !Regex.IsMatch(name?.Text!, "^[a-zA-Z ]"))
        {
            nameMsg.Visibility = Visibility.Visible;
            flag = false;
        }
        else
        {
           nameMsg.Visibility = Visibility.Hidden;
        }///to convert it to double, if not make sure the user will see it
        if (!double.TryParse(price.Text, out double priceDoble))
        {
            priceMsg.Visibility = Visibility.Visible;
            flag = false;
        }
        else
        {
            priceMsg.Visibility = Visibility.Hidden;
        }///to convert it to int, if not make sure the user will see it
        if (!int.TryParse(inStock.Text, out int InStock))
        {
            inStockMsg.Visibility = Visibility.Visible;
            flag = false;
        }
        else
        {
            inStockMsg.Visibility = Visibility.Hidden;
        }//to see if the user chosed somthing on the combox, if not make sure the user will see it
        if (CategoryBox.Text.ToString() == BO.Category.all.ToString() || CategoryBox.Text == "")
        {
            idtxtMsg.Visibility = Visibility.Visible;
            flag = false;
        }
        else
        {

        }
        if (flag)
        {
            
            if (sender.Equals(AddButton))
                AddButton_Click(id, priceDoble, InStock);
            else
                UpdateButton_Click(id, priceDoble, InStock);
        }
    }
    /// <summary>
    /// add to the list new product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="priceDouble"></param>
    /// <param name="InStock"></param>
    private void AddButton_Click(int id, double priceDouble, int InStock)
    {
        
         try
         {
             bl.Product.AddProduct(id, name?.Text!, priceDouble, (BO.Category)CategoryBox.SelectedItem, InStock);
             this.Close(); 
         }
         catch (Exception ex)
         {
                 if (ex.GetType() == typeof(BO.CatchetDOException)) { idtxt.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DD4A48")!; }
                 MessageBox.Show(ex.Message);
         }
    }

    /// <summary>
    /// update product in the chosen item in the list, if m=not make sure the user will see it
    /// </summary>
    /// <param name="id"></param>
    /// <param name="priceDouble"></param>
    /// <param name="InStock"></param>
    private void UpdateButton_Click(int id, double priceDouble, int InStock)
    {
        BO.Product product = new BO.Product
        {
            UniqID = id,
            Name = name.Text,
            Price = priceDouble,
            Category = (BO.Category)CategoryBox.SelectedItem,
            InStock = InStock
        };
        try
        {
            bl.Product.UpdateProduct(product);
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        this.Close();
    }
    private void back_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}


