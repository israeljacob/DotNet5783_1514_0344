using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {

        BLApi.IBL bl = BLApi.Factory.Get;
        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(CartWindow), new PropertyMetadata(null));



        public bool NameCheck
        {
            get { return (bool)GetValue(NameCheckProperty); }
            set { SetValue(NameCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NameCheck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameCheckProperty =
            DependencyProperty.Register("NameCheck", typeof(bool), typeof(CartWindow), new PropertyMetadata(false));

        public bool MailCheck
        {
            get { return (bool)GetValue(MailCheckProperty); }
            set { SetValue(MailCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NameCheck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MailCheckProperty =
            DependencyProperty.Register("MailCheck", typeof(bool), typeof(CartWindow), new PropertyMetadata(false));

        public bool AddresCheck
        {
            get { return (bool)GetValue(AddresCheckProperty); }
            set { SetValue(AddresCheckProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NameCheck.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddresCheckProperty =
            DependencyProperty.Register("AddresCheck", typeof(bool), typeof(CartWindow), new PropertyMetadata(false));


        public CartWindow(BO.Cart cart)
        {
            InitializeComponent();
            MyCart= cart;
        }

        /// <summary>
        /// complete the purches side
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            //make sure the name is completeed
            if(!MyCart.CustomerName!.Contains(" "))
            {
                flag = true;
                NameCheck = true;
            }
            else
            {
                NameCheck = false;
            }
            //make sure the mail is completeed
            if (!MyCart.CustomerEmail!.Contains("@") || !MyCart.CustomerEmail!.Contains("."))
            {
                flag = true;
                MailCheck = true;
            }
            else
            {
                MailCheck = false;
            }
            //make sure the address is completeed
            if (MyCart.CustomerAdress == "")
            {
                flag = true;
                AddresCheck = true;
            }
            else
            {
                AddresCheck = false;
            }
            if (!flag)
            {
                try
                {
                    //if email,adrees,name completed  do the orders
                    int orderId= bl.Cart.ExecuteOrder(MyCart);
                    MessageBox.Show($"Your order has been successfully orderred. Your track order number is: {orderId}. Thank you for shopping with us.");
                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.MainWindowTitle.Contains("Cart"))
                        {
                            process.CloseMainWindow();
                        }
                    }
                    this.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
