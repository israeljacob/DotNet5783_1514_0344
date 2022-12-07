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

namespace PL;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    
    IBL bl = new Bl();
    public ProductWindow(object sender, int id=0)
    {
        
        InitializeComponent();
        
        categoryBox.ItemsSource = Enum.GetValues(typeof(DO.Category));
        Button? button = sender as Button;
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
    private void id_PreviewKeyDown1(object sender, KeyEventArgs e)
    {
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
            e.Key != Key.Left  &&
            e.Key != Key.Decimal)
        {
            e.Handled = true;
        }

    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        
        var bc = new BrushConverter();
        if (!int.TryParse(idtxt.Text, out int id))
        {
            idtxtMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            idtxtMsg.Content = "Enter a integer !";
        }else
        if (name ==null) { nameMsg.Background = (Brush)bc.ConvertFrom("#DD4A48"); nameMsg.Content = "This textbox accepts only alphabetical characters"; }
        if (!double.TryParse(price.Text, out double priceInt))
        {

            priceMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            priceMsg.Content = "Enter a Double !";
        }else
        if (!int.TryParse(inStock.Text, out int InStock))
        {
            inStockMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            inStockMsg.Content = "Enter a integer !";
        }else
        if(categoryBox.Text.ToString()==BO.Category.all.ToString() || categoryBox.Text =="")
        {
            categoryBoxMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            categoryBoxMsg.Content = "Enter a Category except all !";
        }
        else
        {
            try
            {
                bl.Product.AddProduct(id, name.Text, priceInt, (BO.Category)categoryBox.SelectedItem, InStock);
                //Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ProductListWindow")!;
              //  if (win != null) { win.Close(); }
                new ProductListWindow().Show();
                this.Close(); 
              
                   

            }
            catch (Exception ex)
            {
                
                    if (ex.GetType() == typeof(IdAlreadyExistException)) { idtxt.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DD4A48")!; }
                    MessageBox.Show(ex.ToString());
                
            }
        }
    }

    //private void name_TextChanged(object sender, TextChangedEventArgs e)
    //{
    //    if (name.Text == "") return;
    //    if (!System.Text.RegularExpressions.Regex.IsMatch(name.Text, "^[a-zA-Z ]"))
    //    {
    //        MessageBox.Show("This textbox accepts only alphabetical characters");
    //        name.Text.Remove(name.Text.Length - 10);
    //    }

    //}

    private void UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        var bc = new BrushConverter();


        if (!double.TryParse(price.Text, out double priceInt))
        {

            priceMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            priceMsg.Content = "Enter a Double !";
        }
        else
   if (!int.TryParse(inStock.Text, out int InStock))
        {
            inStockMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            inStockMsg.Content = "Enter a integer !";
        }
        else
   if (categoryBox.Text.ToString() == BO.Category.all.ToString() || categoryBox.Text == "")
        {
            categoryBoxMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            categoryBoxMsg.Content = "Enter a Category except all !";
        }
        else
        {
           // try
          //  {
                int.TryParse(idtxt.Text, out int id);
            Product product = new Product();
            product.UniqID = id;
            product.Name = name.Text;
            product.Price = priceInt;
            product.Category = (BO.Category)categoryBox.SelectedItem;
            product.InStock=InStock;
                bl.Product.UpdateProduct(product);
                
                new ProductListWindow().Show();
                this.Close();



          
        }
    }

    private void name_PreviewKeyDown(object sender, KeyEventArgs e)
    {
       // TextChanged = "name_TextChanged"
        if (name.Text == "") return;
        if (!System.Text.RegularExpressions.Regex.IsMatch(name.Text, "^[a-zA-Z ]"))
        {
            MessageBox.Show("This textbox accepts only alphabetical characters");
            name.Text.Remove(name.Text.Length - 1);
        }
    }
}
