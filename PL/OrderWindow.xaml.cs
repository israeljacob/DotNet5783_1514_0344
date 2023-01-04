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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private static readonly BLApi.IBL bl = BLApi.Factory.Get;
        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));



        public Array OrderStatuses
        {
            get { return (Array)GetValue(OrderStatusesProperty); }
            set { SetValue(OrderStatusesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderStatus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderStatusesProperty =
            DependencyProperty.Register("OrderStatuses", typeof(Array), typeof(OrderWindow), new PropertyMetadata(null));



        public string WinName
        {
            get { return (string)GetValue(WinNameProperty); }
            set { SetValue(WinNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WinName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WinNameProperty =
            DependencyProperty.Register("WinName", typeof(string), typeof(Window), new PropertyMetadata(""));
        public OrderWindow(string windowName ,int id)
        {
            InitializeComponent();
            WinName= windowName;
            OrderStatuses = Enum.GetValues(typeof(BO.StatusOfOrder));
            try
            {
                Order = bl.Order.OrderByID(id);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
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
            e.Handled = !regex.IsMatch(e.Text);
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

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// one function to see if we press "add" or "update" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        //        {
        //            Button? button = sender as Button;
        //            var bc = new BrushConverter();
        //            bool flag = true;
        //            ///to convert it to letters , if not make sure the user will see it
        //            if (!int.TryParse(idtxt.Text, out int id) || idtxt.Text == "0")
        //            {
        //                idtxtMsg.Visibility = Visibility.Visible;
        //                flag = false;
        //            }
        //            else
        //            {
        //                idtxtMsg.Visibility = Visibility.Hidden;
        //            }
        //            if (name?.Text == "" || !Regex.IsMatch(name?.Text!, "^[a-zA-Z ]"))
        //            {
        //                nameMsg.Visibility = Visibility.Visible;
        //                flag = false;
        //            }
        //            else
        //            {
        //                nameMsg.Visibility = Visibility.Hidden;
        //            }//to convert it to double, if not make sure the user will see it
        //            if (!double.TryParse(price.Text, out double priceDoble) || price.Text == "0")
        //            {
        //                priceMsg.Visibility = Visibility.Visible;
        //                flag = false;
        //            }
        //            else
        //            {
        //                priceMsg.Visibility = Visibility.Hidden;
        //            }///to convert it to int, if not make sure the user will see it
        //            if (!int.TryParse(inStock.Text, out int InStock) || inStock.Text == "0")
        //            {
        //                inStockMsg.Visibility = Visibility.Visible;
        //                flag = false;
        //            }
        //            else
        //            {
        //                inStockMsg.Visibility = Visibility.Hidden;
        //            }//to see if the user chosed somthing on the combox, if not make sure the user will see it
        //            if (CategoryBox.Text.ToString() == BO.CategoryForList.All.ToString() || CategoryBox.Text == "")
        //            {
        //                categoryBoxMsg.Visibility = Visibility.Visible;
        //                flag = false;
        //            }
        //            else
        //            {
        //                categoryBoxMsg.Visibility = Visibility.Hidden;
        //            }
        //            //if (flag)
        //            //{
        //            //    if (button?.Content.ToString() == "ADD")
        //            //        AddButton_Click(id, priceDoble, InStock);
        //            //    else
        //            //        UpdateButton_Click(id, priceDoble, InStock);
        //            //}
        //            //BO.Product product = new BO.Product
        //            //{
        //            //    UniqID = id,
        //            //    Name = name.Text,
        //            //    Price = priceDouble,
        //            //    Category = (BO.Category)CategoryBox.SelectedItem,
        //            //    InStock = InStock
        //            //};
        //            //try
        //            //{
        //            //    bl.Product.UpdateProduct(product);
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    MessageBox.Show(ex.Message);
        //            //}
        //            this.Close();
        //        }
        //        private void back_Click(object sender, RoutedEventArgs e)
        //        {
        //            this.Close();
        //        }
    }
}
