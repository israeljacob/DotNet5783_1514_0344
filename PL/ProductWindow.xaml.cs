using BLApi;
using BlImplementation;
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
using BO;
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
    IBL bl = new Bl();
    public ProductWindow(object sender, int id=0)
    {
        
        InitializeComponent();
        ///get the enums to combobox
        List<BO.Category> categories = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>().ToList();
        foreach (BO.Category category in categories)
            if (category != BO.Category.all)
                CategoryBox.Items.Add(category);///add in to the opened list
        Button? button = sender as Button;
        ///to see if we press on update or add button
        if(button !=null)
            UpdateButton.Visibility = Visibility.Hidden;
        else
        {
            AddButton.Visibility = Visibility.Hidden;

            string title = id.ToString();
             idtxt.Text= title;
             idtxt.IsEnabled = false;
        }

    }
   
    private void categoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
    }

    /// <summary>
    /// check evrey type of the user and make sure only numbers will take place in the box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void id_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        ///Only numbers..
        if( e.Key != Key.D0 && 
            e.Key != Key.D1 && 
            e.Key != Key.D2 &&
            e.Key != Key.D3 && 
            e.Key != Key.D4 &&
            e.Key != Key.D5 &&
            e.Key != Key.D6 &&
            e.Key != Key.D7 && 
            e.Key != Key.D8 &&
            e.Key != Key.D9 && 
            e.Key != Key.Back && 
            e.Key != Key.NumPad0 && 
            e.Key != Key.NumPad1 && 
            e.Key != Key.NumPad2 && 
            e.Key != Key.NumPad3 && 
            e.Key != Key.NumPad4 && 
            e.Key != Key.NumPad5 && 
            e.Key != Key.NumPad6 && 
            e.Key != Key.NumPad7 && 
            e.Key != Key.NumPad8 && 
            e.Key != Key.NumPad9 && 
            e.Key != Key.Delete && 
            e.Key != Key.Right && 
            e.Key != Key.Left)
        {
            e.Handled = true;
            
        }

    }
    /// <summary>
    /// check evrey type of the user and make sure only numbers and dote (for the price) will take place in the box
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void id_PreviewKeyDown1(object sender, KeyEventArgs e)
    {
        TextBox textBox = sender as TextBox ?? null!;
        ///Only numbers with period
        if (e.Key != Key.D0 &&
            e.Key != Key.D1 &&
            e.Key != Key.D2 &&
            e.Key != Key.D3 &&
            e.Key != Key.D4 &&
            e.Key != Key.D5 &&
            e.Key != Key.D6 &&
            e.Key != Key.D7 &&
            e.Key != Key.D8 &&
            e.Key != Key.D9 &&
            e.Key != Key.Back &&
            e.Key != Key.NumPad0 &&
            e.Key != Key.NumPad1 &&
            e.Key != Key.NumPad2 &&
            e.Key != Key.NumPad3 &&
            e.Key != Key.NumPad4 &&
            e.Key != Key.NumPad5 &&
            e.Key != Key.NumPad6 &&
            e.Key != Key.NumPad7 &&
            e.Key != Key.NumPad8 &&
            e.Key != Key.NumPad9 &&
            e.Key != Key.Delete &&
            e.Key != Key.Right &&
            e.Key != Key.Left &&
            (e.Key != Key.Decimal || textBox!.Text.Contains(".")))
        {
            e.Handled = true;
        }
    }
    private void name_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.A &&
            e.Key != Key.B &&
            e.Key != Key.C &&
            e.Key != Key.D &&
            e.Key != Key.E &&
            e.Key != Key.F &&
            e.Key != Key.G &&
            e.Key != Key.H &&
            e.Key != Key.I &&
            e.Key != Key.J &&
            e.Key != Key.K &&
            e.Key != Key.L &&
            e.Key != Key.M &&
            e.Key != Key.N &&
            e.Key != Key.P &&
            e.Key != Key.Q &&
            e.Key != Key.R &&
            e.Key != Key.S &&
            e.Key != Key.T &&
            e.Key != Key.U &&
            e.Key != Key.V &&
            e.Key != Key.W &&
            e.Key != Key.X &&
            e.Key != Key.Y &&
            e.Key != Key.Z &&
            e.Key != Key.Back &&
            e.Key != Key.Delete &&
            e.Key != Key.Right &&
            e.Key != Key.Left &&
            e.Key != Key.Space &&
            e.Key != Key.LeftShift &&
            e.Key != Key.RightShift)
        {
            e.Handled = true;
        }
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
                 if (ex.GetType() == typeof(CatchetDOException)) { idtxt.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DD4A48")!; }
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
        Product product = new Product
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


