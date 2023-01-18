using DocumentFormat.OpenXml.Office2010.Excel;
using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window, INotifyPropertyChanged
{


    public int OrderId
    {
        get { return (int)GetValue(OrderIdProperty); }
        set { SetValue(OrderIdProperty, value); }
    }

    // Using a DependencyProperty as the backing store for OrderId.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrderIdProperty =
        DependencyProperty.Register("OrderId", typeof(int), typeof(SimulatorWindow), new PropertyMetadata(0));



    public event PropertyChangedEventHandler? PropertyChanged ;
    private BackgroundWorker bgWorker;
    private bool cancelation = true;
    Stopwatch stopWatch = new Stopwatch();
    public SimulatorWindow()
    {
        InitializeComponent();
        
        bgWorker = new BackgroundWorker();
        bgWorker.DoWork += BgWorker_DoWork!;
        bgWorker.ProgressChanged += BgWorker_ProgressChanged!;
        bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted!;

        bgWorker.WorkerReportsProgress = true;
        bgWorker.WorkerSupportsCancellation = true;
        bgWorker.RunWorkerAsync();
        stopWatch.Restart();
    }

    private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        MySimulator.RegisterToUpdateProgress(Updated);
        MySimulator.RegisterToSimulationComplete(complete);
        MySimulator.activate();
        while (!bgWorker.CancellationPending)
        {
            Thread.Sleep(1000);
            bgWorker.ReportProgress(3);
        }
    }

    private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (e.ProgressPercentage == 3)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            timerTextBlock.Text = timerText;
        }
        else if (e.ProgressPercentage >=100000)
        {
            
            int ID = e.ProgressPercentage;
        }
    }

    private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        this.Close();
    }

    private void btnStopSimulation_Click(object sender, EventArgs e)
    {
        if (!bgWorker.CancellationPending)
        {
            MySimulator.StopSimulation();
            Thread.Sleep(11000);
            MySimulator.UnregisterFromSimulationComplete(complete);
            MySimulator.UnregisterFromUpdateProgress(Updated);
            stopWatch.Stop();
            bgWorker.CancelAsync();
        }
        cancelation = false;
    }
    private void Updated(int ID, BO.StatusOfOrder? correntStatus, DateTime start, BO.StatusOfOrder? nextStatus, DateTime end)
    {
        OrderId = ID;
        bgWorker.ReportProgress(ID);
    }

    private void complete()
    {
        bgWorker.ReportProgress(1);
    }
    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = cancelation;
        if(cancelation)
            MessageBox.Show("You musn't close the window in this way");
    }
}

