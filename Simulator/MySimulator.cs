using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

/// <summary>
/// MySimulator is used to control the execution of the simulation
/// </summary>
public static class MySimulator
{
    /// <summary>
    /// These events can be subscribed to by external code to receive notifications
    /// when the simulation is complete or when the order processing is updated.
    /// </summary>
    private static volatile bool active = false;
    static readonly BLApi.IBL bl = BLApi.Factory.Get();
    public delegate void SimulationCompleteEventHandler();
    public static event SimulationCompleteEventHandler? SimulationComplete;
    public delegate void OrderProcessingEventHandler(int ID, BO.StatusOfOrder? CurrentStatus, DateTime start, BO.StatusOfOrder? nextStatus, DateTime end);
    public static event OrderProcessingEventHandler? OrderProcessing;


    /// <summary>
    ///  starts a new thread, The thread repeatedly calls the "bl.Order.ForSimulator()" method to get an order ID,
    /// and if an order ID is returned, it proceeds to simulate the processing of the order by
    /// randomly generating a time duration, updating the order's status, 
    /// and invoking the "OrderProcessing" event to report the progress of the simulation.
    /// If the order has already been shipped, it updates the status to "Delivered", 
    /// otherwise it updates the status to "Sent". After updating the order, 
    /// it invokes the "SimulationComplete" event to notify that the simulation is complete. 
    /// The thread sleeps for 1 second before starting the next iteration.
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static void activate()
    {
        active = true;
        new Thread(() =>
        {
            while (active)
            {
                int? orderID = bl.Order.ForSimulator();
                if (orderID is not null)
                {
                    BO.Order order;
                    try
                    {
                        order = bl.Order.GetOrderByID((int)orderID);
                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                    bool shipped = false;
                    if (order.ShipDate != null)
                        shipped = true;
                    Random random = new Random();
                    int time = random.Next(3, 11);
                    DateTime finishTask = DateTime.Now + new TimeSpan(time* 10000000);
                    //report
                    OrderProcessing?.Invoke(
                        order.UniqID,
                        order.StatusOfOrder,
                        DateTime.Now,
                        order.StatusOfOrder == BO.StatusOfOrder.Orderred ? BO.StatusOfOrder.Sent : BO.StatusOfOrder.Delivered,
                        finishTask);
                    Thread.Sleep(time * 1000);
                    if (shipped)
                    {
                        order.StatusOfOrder = BO.StatusOfOrder.Delivered;
                        order.DeliveryrDate = DateTime.Now;
                    }
                    else
                    {
                        order.StatusOfOrder = BO.StatusOfOrder.Sent;
                        order.ShipDate = DateTime.Now;
                    }
                    try
                    {
                        bl.Order.UpdateOrder(order);
                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                    //report finished
                    SimulationComplete?.Invoke();
                }
                Thread.Sleep(1000);
            }
        }).Start();
    }

    /// <summary>
    /// "StopSimulation" sets the "active" variable to false, which stops the simulation. 
    /// </summary>
    public static void StopSimulation()
    {
        active = false;
    }
    /// <summary>
    ///  allows external code to subscribe 
    /// and unsubscribe to the "SimulationComplete" and "OrderProcessing" events.
    /// </summary>
    /// <param name="handler"></param>
    public static void RegisterToSimulationComplete(SimulationCompleteEventHandler handler)
    {
        SimulationComplete += handler;
    }

    /// <summary>
    /// allows external code to subscribe 
    /// and unsubscribe to the "SimulationComplete" and "OrderProcessing" events.
    /// </summary>
    /// <param name="handler"></param>
    public static void UnregisterFromSimulationComplete(SimulationCompleteEventHandler handler)
    {
        SimulationComplete -= handler;
    }
    /// <summary>
    /// allows external code to subscribe 
    /// and unsubscribe to the "SimulationComplete" and "OrderProcessing" events.
    /// </summary>
    /// <param name="handler"></param>
    public static void RegisterToUpdateProgress(OrderProcessingEventHandler handler)
    {
        OrderProcessing += handler;
    }

    /// <summary>
    /// allows external code to subscribe 
    /// and unsubscribe to the "SimulationComplete" and "OrderProcessing" events.
    /// </summary>
    /// <param name="handler"></param>
    public static void UnregisterFromUpdateProgress(OrderProcessingEventHandler handler)
    {
        OrderProcessing -= handler;
    }
}