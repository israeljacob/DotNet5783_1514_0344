using System;
using System.Collections.Generic;
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
    /// Interaction logic for TrackDetailsWindow.xaml
    /// </summary>
    public partial class TrackDetailsWindow : Window
    {


        public BO.OrderTracking TrackOrder
        {
            get { return (BO.OrderTracking)GetValue(TrackOrderProperty); }
            set { SetValue(TrackOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrackOrder. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackOrderProperty =
            DependencyProperty.Register("TrackOrder", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


        public TrackDetailsWindow(BO.OrderTracking orderTracking)
        {
            InitializeComponent();
            TrackOrder= orderTracking;
        }

        /// <summary>
        /// after the id was typed send the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow("TrackDetailsWindow", TrackOrder.UniqID).Show();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
