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
        private static readonly BLApi.IBL bl = BLApi.Factory.Get();
        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
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
                Order = bl.Order.GetOrderByID(id);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Order.UpdateOrder(Order);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
                this.Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// set the ship date or the delivery date if it has not send yet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Order order = Order;
            Button? button = (Button)sender;
            if(button.Content.ToString() == "Sent")
                order.ShipDate= DateTime.Now;
            else
                order.DeliveryrDate= DateTime.Now;
            Order = new();
            Order = order;
        }
        /// <summary>
        /// if the customer want to update the orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem ourOrderItem = (BO.OrderItem)((Button)sender).DataContext;
            try
            {
                 Order = bl.Order.UpdateOrderItemAmount(Order,ourOrderItem);
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
