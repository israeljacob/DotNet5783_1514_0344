//using BLApi;
//using DocumentFormat.OpenXml.EMMA;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Simulator;

//public sealed class MySimulator: INotifyPropertyChanged
//{
//    public event PropertyChangedEventHandler? PropertyChanged;
//    static readonly IBL bl = Factory.Get();

//    MySimulator() { }
//    public static MySimulator Instance { get; } = new MySimulator();


//    Action<MySimulator> update;
//    private bool active = false;
//    public void UnActivate()
//    {
//        active= false;
//    }
//    public void Activate()
//    {
//        active = true;
//        new Thread(()=>
//        while (active)
//        {
//            int? orderID = bl.Order.ForSimulator();

//        }
//    });
    
//    BO.Order? order = bl.Order.ForSimulator();
//    public BO.Order Order
//    {
//        get { return order!; }
//        set
//        {
//            order = value;
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Order)));
//        }
//    }





//    ////private static properties
//    //private static Thread? simulationThread;
//    //private static volatile bool stopSimulation;

//    ////public static events and delegates
//    //public delegate void SimulationEventHandler(object sender, EventArgs e);
//    //public static event SimulationEventHandler? SimulationCompleted;
//    //public static event SimulationEventHandler? Simulationupdated;
//    //public static event SimulationEventHandler? SimulationStopped;
//    //public static event SimulationEventHandler? SimulationOrderUpdated;

//    ////////public static methods
//    //////public static void RunSimulation()
//    //////{
//    //////    isRunning = true;
//    //////    stopSimulation = false;

//    //////    //perform simulation
//    //////    while (isRunning)
//    //////    {
//    //////        //check if simulation should stop
//    //////        if (stopSimulation)
//    //////        {
//    //////            isRunning = false;
//    //////            OnSimulationComplete?.Invoke();
//    //////            break;
//    //////        }
//    //////        //update simulation
//    //////        OnSimulationUpdate?.Invoke();
//    //////        //additional event
//    //////        OnAdditionalEvent?.Invoke();
//    //////    }
//    //////}

//    //public static void StopSimulation()
//    //{
//    //    stopSimulation = true;
//    //    simulationThread?.Join();
//    //}

//    //public static void RegisterForCompleteEvent(SimulationEventHandler handler)
//    //{
//    //    SimulationCompleted += handler;
//    //}

//    //public static void UnregisterForCompleteEvent(SimulationEventHandler handler)
//    //{
//    //    SimulationCompleted -= handler;
//    //}

//    //public static void RegisterForUpdateEvent(SimulationEventHandler handler)
//    //{
//    //    Simulationupdated += handler;
//    //}

//    //public static void UnregisterForUpdateEvent(SimulationEventHandler handler)
//    //{
//    //    Simulationupdated -= handler;
//    //}

//    //public static void RegisterForAdditionalEvent(SimulationEventHandler handler)
//    //{
//    //    SimulationOrderUpdated += handler;
//    //}

//    //public static void UnregisterForAdditionalEvent(SimulationEventHandler handler)
//    //{
//    //    SimulationOrderUpdated -= handler;
//    //}

//    //public static void RegisterForStopEvent(SimulationEventHandler handler)
//    //{
//    //    SimulationStopped += handler;
//    //}

//    //public static void UnregisterForStopEvent(SimulationEventHandler handler)
//    //{
//    //    SimulationStopped -= handler;
//    //}
//    //public static void RunSimulation()
//    //{
//    //    stopSimulation = false;

//    //    simulationThread = new Thread(Simulate);
//    //    simulationThread.Start();
//    //}
//    //public static void Simulate()
//    //{
//    //    BLApi.IBL bl = BLApi.Factory.Get();
//    //    List<BO.Order> orders = (from BO.OrderForList order in bl.Order.GetListOfOrders().ToList()!
//    //                             select bl.Order.GetOrderByID(order.UniqID)).ToList();


//    //    stopSimulation = false;
//    //    while (true)
//    //    {
//    //        if (stopSimulation)
//    //        {
//    //            break;
//    //        }
//    //        //Get the next order to process based on priority
//    //        var nextOrder = orders.Where(o => o.StatusOfOrder == BO.StatusOfOrder.Orderred)
//    //                              .OrderBy(o => o.OrderDate)
//    //                              .FirstOrDefault();
//    //        if (nextOrder == null)
//    //        {
//    //            nextOrder = orders.Where(o => o.StatusOfOrder == BO.StatusOfOrder.Sent)
//    //                              .OrderBy(o => o.ShipDate)
//    //                              .FirstOrDefault();
//    //        }

//    //        if (nextOrder == null)
//    //        {
//    //            //There are no more orders to process
//    //            break;
//    //        }

//    //        //Update the order status
//    //        if (nextOrder.StatusOfOrder == BO.StatusOfOrder.Orderred)
//    //        {
//    //            nextOrder.StatusOfOrder = BO.StatusOfOrder.Sent;
//    //            nextOrder.ShipDate = DateTime.Now;
//    //            Console.WriteLine("Order " + nextOrder.UniqID + " has been sent");
//    //        }
//    //        else if (nextOrder.StatusOfOrder == BO.StatusOfOrder.Sent)
//    //        {
//    //            nextOrder.StatusOfOrder = BO.StatusOfOrder.Delivered;
//    //            nextOrder.DeliveryrDate = DateTime.Now;
//    //            Console.WriteLine("Order " + nextOrder.UniqID + " has been delivered");
//    //        }

//    //        //Wait for a certain amount of time before processing the next order
//    //        Random random= new Random();
//    //        System.Threading.Thread.Sleep(random.Next(3000,10000));
//    //    }
//    //}
//}


