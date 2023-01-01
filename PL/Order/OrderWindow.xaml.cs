using DocumentFormat.OpenXml.Office2010.Excel;
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

namespace PL;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    /// <summary>
    /// show of only one bl
    /// </summary>
    private static readonly BLApi.IBL bl = BLApi.Factory.Get;
    public static readonly DependencyProperty OrderDependency = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(Window));
    public BO.Order? Order { get => (BO.Order)GetValue(OrderDependency); private set => SetValue(OrderDependency, value); }

    public OrderWindow(object sender, int id = 0)
    {
        InitializeComponent();
        if (id != 0)
            Order = bl.Order.OrderByID(id);
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
}
