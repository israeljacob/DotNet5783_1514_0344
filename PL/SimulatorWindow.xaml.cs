using DocumentFormat.OpenXml.Office2010.Excel;
using Simulator;
using System;
using System.Collections;
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
public partial class SimulatorWindow : Window
{


    public int ForProgress
    {
        get { return (int)GetValue(ForProgressProperty); }
        set { SetValue(ForProgressProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ForProgress.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ForProgressProperty =
        DependencyProperty.Register("ForProgress", typeof(int), typeof(SimulatorWindow), new PropertyMetadata(0));



    public bool Completed
    {
        get { return (bool)GetValue(CompletedProperty); }
        set { SetValue(CompletedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Completed.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CompletedProperty =
        DependencyProperty.Register("Completed", typeof(bool), typeof(SimulatorWindow), new PropertyMetadata(false));



    public int OrderID
    {
        get { return (int)GetValue(OrderIDProperty); }
        set { SetValue(OrderIDProperty, value); }
    }

    // Using a DependencyProperty as the backing store for OrderID.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty OrderIDProperty =
        DependencyProperty.Register("OrderID", typeof(int), typeof(SimulatorWindow), new PropertyMetadata(0));



    public BO.StatusOfOrder? Corrent
    {
        get { return (BO.StatusOfOrder?)GetValue(CorrentProperty); }
        set { SetValue(CorrentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Corrent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CorrentProperty =
        DependencyProperty.Register("Corrent", typeof(BO.StatusOfOrder?), typeof(SimulatorWindow), new PropertyMetadata(null));

    public BO.StatusOfOrder? Next
    {
        get { return (BO.StatusOfOrder?)GetValue(NextProperty); }
        set { SetValue(NextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Begin.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NextProperty =
        DependencyProperty.Register("Next", typeof(BO.StatusOfOrder?), typeof(SimulatorWindow), new PropertyMetadata(null));



    public DateTime Begin
    {
        get { return (DateTime)GetValue(BeginProperty); }
        set { SetValue(BeginProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Begin.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BeginProperty =
        DependencyProperty.Register("Begin", typeof(DateTime), typeof(SimulatorWindow), new PropertyMetadata(DateTime.MinValue));

    public DateTime Finish
    {
        get { return (DateTime)GetValue(FinishProperty); }
        set { SetValue(FinishProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Begin.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FinishProperty =
        DependencyProperty.Register("Finish", typeof(DateTime), typeof(SimulatorWindow), new PropertyMetadata(DateTime.MinValue));


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
            int dif = Finish.Second - Begin.Second;
            ForProgress += 100 / dif;
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            timerTextBlock.Text = timerText;
        }
        else if (e.ProgressPercentage >=100000)
        {
            Completed = false;
            ForProgress = 0;
            ArrayList arrayList = (ArrayList)e.UserState!;
            OrderID = e.ProgressPercentage;
            Corrent = (BO.StatusOfOrder?)arrayList[1];
            Begin = (DateTime)arrayList[2]!;
            Next = (BO.StatusOfOrder?)arrayList[3];
            Finish = (DateTime)arrayList[4]!;
        }
        else
        {
            Completed= true;
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
        ArrayList arrayList= new ArrayList();
        arrayList.Add(ID);
        arrayList.Add(correntStatus);
        arrayList.Add(start);
        arrayList.Add(nextStatus);
        arrayList.Add(end);
        bgWorker.ReportProgress(ID, arrayList);
    }

    private void complete()
    {
        bgWorker.ReportProgress(1);
    }
    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = cancelation;
        if(cancelation)
            MessageBox.Show("You can close the window by pressing the stop simlation button only");
    }
}

