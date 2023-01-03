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

namespace PL;


/// <summary>
/// Interaction logic for OrderTrackWindow.xaml
/// </summary>
public partial class OrderTrackWindow : Window
{
    private static readonly BLApi.IBL bL = BLApi.Factory.Get;
    public int OrderID
    {
        get { return (int)GetValue(OrderIDProperty); }
        set { SetValue(OrderIDProperty, value); }
    }

    // Using a DependencyProperty as the backing store for OrderID.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrderIDProperty =
        DependencyProperty.Register("OrderID", typeof(int), typeof(Window), new PropertyMetadata(0));
    BO.OrderTracking orderTracking = new();
    public OrderTrackWindow()
    {
        InitializeComponent();
        OrderIDTB.DataContext = OrderID;
    }

    private void Track_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            orderTracking = bL.Order.OrderTrack(int.Parse(OrderIDTB.Text));
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        new TrackDetailsWindow(orderTracking).Show();
        this.Close();
    }

    private void OrderID_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new("^[0-9]+");
        e.Handled = !regex.IsMatch(e.Text);
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        this.Close();
    }
}
