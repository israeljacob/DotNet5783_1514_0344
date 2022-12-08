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
    
    IBL bl = new Bl();
    public ProductWindow(object sender, int id=0)
    {
        
        InitializeComponent();
        List<BO.Category> categories = Enum.GetValues(typeof(BO.Category)).Cast<BO.Category>().ToList();
        foreach (BO.Category category in categories)
            if (category != BO.Category.all)
                categoryBox.Items.Add(category);
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
    private void AddButton_or_UpdateButton_Click(object sender, RoutedEventArgs e)
    {
        var bc = new BrushConverter();
        bool flag = true;
        if (!int.TryParse(idtxt.Text, out int id))
        {
            idtxtMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            idtxtMsg.Content = "Enter a integer !";
            flag = false;
        }
        else
        {
            idtxtMsg.Background = (Brush)bc.ConvertFrom("#FFFFFFFF")!;
            idtxtMsg.Content = "";
        }
        if (name?.Text == "" || !Regex.IsMatch(name?.Text!, "^[a-zA-Z ]"))
        {
            nameMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            nameMsg.Content = "Enter only alphabetical characters";
            flag = false;
        }
        else
        {
            nameMsg.Background = (Brush)bc.ConvertFrom("#FFFFFFFF")!;
            nameMsg.Content = "";
        }
        if (!double.TryParse(price.Text, out double priceDoble))
        {

            priceMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            priceMsg.Content = "Enter a Double !";
            flag = false;
        }
        else
        {
            priceMsg.Background = (Brush)bc.ConvertFrom("#FFFFFFFF")!;
            priceMsg.Content = "";
        }
        if (!int.TryParse(inStock.Text, out int InStock))
        {
            inStockMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            inStockMsg.Content = "Enter a integer !";
            flag = false;
        }
        else
        {
            inStockMsg.Background = (Brush)bc.ConvertFrom("#FFFFFFFF")!;
            inStockMsg.Content = "";
        }
        if (categoryBox.Text.ToString() == BO.Category.all.ToString() || categoryBox.Text == "")
        {
            categoryBoxMsg.Background = (Brush)bc.ConvertFrom("#DD4A48")!;
            categoryBoxMsg.Content = "Enter a Category!";
            flag = false;
        }
        else
        {
            categoryBoxMsg.Background = (Brush)bc.ConvertFrom("#FFFFFFFF")!;
            categoryBoxMsg.Content = "";
        }
        if (flag)
        {
            //MessageBox.Show(sender.ToString());
            if (sender.Equals(AddButton))
                AddButton_Click(id, priceDoble, InStock);
            else
                UpdateButton_Click(id, priceDoble, InStock);
        }
    }
    private void AddButton_Click(int id, double priceDouble, int InStock)
    {
        
         try
         {
             bl.Product.AddProduct(id, name?.Text!, priceDouble, (BO.Category)categoryBox.SelectedItem, InStock);
             new ProductListWindow().Show();
             this.Close(); 
         }
         catch (Exception ex)
         {
                 if (ex.GetType() == typeof(IdAlreadyExistException)) { idtxt.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DD4A48")!; }
                 MessageBox.Show(ex.Message);
         }
    }


    private void UpdateButton_Click(int id, double priceDouble, int InStock)
    {
        Product product = new Product
        {
            UniqID = id,
            Name = name.Text,
            Price = priceDouble,
            Category = (BO.Category)categoryBox.SelectedItem,
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
        Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "productListWindow")!;
        if (win != null) { win.Close(); }
        new ProductListWindow().Show();
        this.Close();
    }

    private void name_PreviewKeyDown(object sender, KeyEventArgs e)
    {


        // TextChanged = "name_TextChanged"
        //if (!(System.Text.RegularExpressions.Regex.IsMatch(name.Text, "^[a-zA-Z ]")||
        //    e.Key== Key.Delete||
        //    e.Key == Key.Right ||
        //    e.Key == Key.Left ||
        //     e.Key == Key.Back||
        //      e.Key == Key.Enter))
        //{
        //    MessageBox.Show("This textbox accepts only alphabetical characters");
        //    e.Handled = true;
        //}
        
    }
}
