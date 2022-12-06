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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBL bl = new Bl();
        public ProductWindow()
        {
            InitializeComponent();
            categoryBox.ItemsSource = Enum.GetValues(typeof(DO.Category));

            //bl.Product.AddProduct(1, "", 1, BO.Category.shoes, 1);
        }
        ProductWindow(IBL bl, int id)
        {


        }
        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}

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
            ///Only numbers..
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            if (!int.TryParse(idtxt.Text, out int id))
            {
                idtxtMsg.Background = (Brush)bc.ConvertFrom("#DD4A48");
                idtxtMsg.Content = "Enter a integer !";
            }
          //  if (id < 0) { txtBoxId.Background = (Brush)bc.ConvertFrom("#DD4A48"); txtBoxId.Text = "Positive integer only !"; }
            if (!double.TryParse(price.Text, out double priceInt))
            {

                priceMsg.Background = (Brush)bc.ConvertFrom("#DD4A48");
                priceMsg.Content = "Enter a Double !";
            }
            if (!int.TryParse(inStock.Text, out int InStock))
            {
                inStockMsg.Background = (Brush)bc.ConvertFrom("#DD4A48");
                inStockMsg.Content = "Enter a integer !";
            }
            if(categoryBox.Text.ToString()==BO.Category.all.ToString() || categoryBox.Text =="")
            {
                categoryBoxMsg.Background = (Brush)bc.ConvertFrom("#DD4A48");
                categoryBoxMsg.Content = "Enter a Category except all !";
            }
            else
            {
                try
                {
                    bl.Product.AddProduct(id, name.Text, priceInt, (BO.Category)categoryBox.SelectedItem, InStock);
                    this.Close(); Window win = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "ProductListWindow");
                    if (win != null) { win.Close(); }
                    new ProductListWindow().Show();
                        //.CustomerList(bl).Show();

                }
                catch (AggregateException ex)
                {
                    foreach (Exception innerException in ex.InnerExceptions)
                    {
                        if (innerException.GetType() == typeof(IdAlreadyExistException)) { idtxt.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#DD4A48"); }
                        MessageBox.Show(innerException.ToString());
                    }
                }
            }
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (name.Text == "") return;
            if (!System.Text.RegularExpressions.Regex.IsMatch(name.Text, "^[a-zA-Z ]"))
            {
                MessageBox.Show("This textbox accepts only alphabetical characters");
                name.Text.Remove(name.Text.Length - 1);
            }

        }
    }
}
