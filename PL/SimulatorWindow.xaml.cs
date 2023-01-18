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
    public event PropertyChangedEventHandler? PropertyChanged ;
    private BackgroundWorker bgWorker;
    private bool cancelation = true;
    private bool isBackgroundWorker = true;
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
        MySimulator.RegisterToUpdateProgress(Simulator_SimulationOrderUpdated);
        MySimulator.activate();
        while (isBackgroundWorker)
        {
            Thread.Sleep(1000);
            bgWorker.ReportProgress(1);
        }
    }

    private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        int ID = e.ProgressPercentage;
        lblCurrentOrder.Content = ID;
        string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
    }

    private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        MessageBox.Show("Simulation Completed");
        this.Close();
    }

    private void btnStopSimulation_Click(object sender, EventArgs e)
    {
        if (isBackgroundWorker)
        {
            stopWatch.Stop();
            isBackgroundWorker = false;
        }
        cancelation = false;
        this.Close();
    }

    private void Simulator_SimulationCompleted(object sender, EventArgs e)
    {
        bgWorker.CancelAsync();
    }

    private void Simulator_SimulationStopped(object sender, EventArgs e)
    {
        bgWorker.CancelAsync();
    }
    private void Simulator_SimulationOrderUpdated(int ID, BO.StatusOfOrder? correntStatus, DateTime now, BO.StatusOfOrder? nextStatus, DateTime finish)
    {
        bgWorker.ReportProgress(ID);
    }
    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = cancelation;
        if(cancelation)
            MessageBox.Show("You musn't close the window in this way");
    }
}

