﻿using DocumentFormat.OpenXml.Office2010.Excel;
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
/// It creates an instance of the BackgroundWorker class,
/// and assigns event handlers to its DoWork, ProgressChanged, and RunWorkerCompleted events. 
/// The BackgroundWorker is configured to report progress and support cancellation.
/// </summary>
public partial class SimulatorWindow : Window
{


    public string Percent
    {
        get { return (string)GetValue(PercentProperty); }
        set { SetValue(PercentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Percent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PercentProperty =
        DependencyProperty.Register("Percent", typeof(string), typeof(SimulatorWindow), new PropertyMetadata("0%"));



    public string TimerShow
    {
        get { return (string)GetValue(TimerShowProperty); }
        set { SetValue(TimerShowProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TimerShow.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TimerShowProperty =
        DependencyProperty.Register("TimerShow", typeof(string), typeof(SimulatorWindow), new PropertyMetadata("00:00:00"));



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



    public string Begin
    {
        get { return (string)GetValue(BeginProperty); }
        set { SetValue(BeginProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Begin.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BeginProperty =
        DependencyProperty.Register("Begin", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(DateTime.MinValue.ToString()));

    public string Finish
    {
        get { return (string)GetValue(FinishProperty); }
        set { SetValue(FinishProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Begin.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty FinishProperty =
        DependencyProperty.Register("Finish", typeof(string), typeof(SimulatorWindow), new PropertyMetadata(DateTime.MinValue.ToString()));

    bool finished = false;
    bool toProgress = true;
    DateTime begin = DateTime.MinValue;
    DateTime finish = DateTime.MinValue;
    double percent = 0;
    private BackgroundWorker bgWorker;
    Stopwatch stopWatch = new Stopwatch();

    /// <summary>
    /// The program starts the simulation
    /// by calling the RunWorkerAsync method of the BackgroundWorker
    /// and starts a Stopwatch. 
    /// ProgressChanged is the main progress
    /// </summary>
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

    /// <summary>
    /// The BgWorker_DoWork event handler registered
    /// to the DoWork event of the BackgroundWorker register to the "OrderProcessing" and 
    /// "SimulationComplete" events of the MySimulator class, 
    /// starts the simulation by calling the "activate()" method of the MySimulator class,
    /// and enters a loop that sleeps for 1 second and reports progress 3 every iteration.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        MySimulator.RegisterToUpdateProgress(Updated);
        MySimulator.RegisterToSimulationComplete(complete);
        MySimulator.activate();
        while (!bgWorker.CancellationPending)
        {
            Thread.Sleep(1000);
            bgWorker.ReportProgress(0);
        }
    }

    /// <summary>
    /// The BgWorker_ProgressChanged event handler registered to the ProgressChanged event of the
    /// BackgroundWorker updates the form elements with the status of the simulation.
    /// It checks the ProgressPercentage passed from the DoWork method, if it's 0, it updates the timer textblock,
    /// otherwise, if it's greater than or equal to 100000, 
    /// it updates the form elements with the order details passed as the UserState parameter
    /// and sets the completion flag to false. 
    /// Otherwise, it sets the completion flag to true and if the flag finished is true,
    /// unregister from the "OrderProcessing" and "SimulationComplete" events of the MySimulator class 
    /// and stops the stopwatch,
    /// cancels the background worker and sets the cancelation flag to false.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {

        if (e.ProgressPercentage == 0)
        {
            if (toProgress)
            {
                TimeSpan diff = finish - begin;
                int dif = diff.Seconds + 1;
                ForProgress = (int)Math.Round((double)100 / dif + percent) < 100 ? (int)Math.Round((double)100 / dif + percent) : 100;
                percent = ForProgress < 100 ? percent+(double)100 / dif : 100;
                Percent = $"{ForProgress}%";
            }
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            TimerShow = timerText;
            toProgress = true;
        }
        else if (e.ProgressPercentage >= 100000)
        {
            if (Completed)
                toProgress = false;
            Completed = false;
            ForProgress = 0;
            Percent = "0%";
            percent = 0;
            ArrayList arrayList = (ArrayList)e.UserState!;
            OrderID = e.ProgressPercentage;
            Corrent = (BO.StatusOfOrder?)arrayList[1];
            Begin = ((DateTime)arrayList[2]!).ToString();
            Next = (BO.StatusOfOrder?)arrayList[3];
            Finish = ((DateTime)arrayList[4]!).ToString();
        }
        else
        {
            Completed = true;
            if (finished)
            {
                bgWorker.CancelAsync();
            }
        }
    }

    /// <summary>
    /// The BgWorker_RunWorkerCompleted event handler registered to the RunWorkerCompleted event
    /// of the BackgroundWorker closes the form.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        if (!bgWorker.CancellationPending)
        {
            MySimulator.UnregisterFromSimulationComplete(complete);
            MySimulator.UnregisterFromUpdateProgress(Updated);
            stopWatch.Stop();
            this.Close();
        }
    }

    /// <summary>
    ///  The btnStopSimulation_Click event handler,
    /// which is triggered when the user clicks the Stop Simulation button, stops the simulation 
    /// by calling the StopSimulation method of the MySimulator class, if the completion flag is true, 
    /// it unregisters from the "OrderProcessing" and "SimulationComplete" events of the MySimulator class,
    /// stops the stopwatch, cancels the background worker and sets the cancelation flag to false.
    /// Otherwise, it sets the finished flag to true and shows a message box 
    /// informing the user that the window will close when the current order is updated.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnStopSimulation_Click(object sender, EventArgs e)
    {
        MySimulator.StopSimulation();
        if (Completed)
        {
            bgWorker.CancelAsync();
        }
        else
        {
            finished = true;
            MessageBox.Show("The window will close wen the corrent order will be updated");
        }

    }
    private void Updated(int ID, BO.StatusOfOrder? correntStatus, DateTime start, BO.StatusOfOrder? nextStatus, DateTime end)
    {
        begin = start;
        finish = end;
        ArrayList arrayList = new ArrayList();
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

    private void Window_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            this.DragMove();
        }
    }
}

