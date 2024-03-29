﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    /// <summary>
    /// if the user preesed the admin button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Admin_click(object sender, RoutedEventArgs e)
    {
        ///show the list of all the products
        new AdminWindow().Show();
        this.Close();
    }
    private void New_order_click(object sender, RoutedEventArgs e)
    {
        new NewOrderWindow().Show();
        this.Close();
    }

    private void Track_order_click(object sender, RoutedEventArgs e)
    {
        new OrderTrackWindow().Show();
        this.Close();
    }

    private void Simulator_click(object sender, RoutedEventArgs e)
    {
        new SimulatorWindow().Show();
    }
}
