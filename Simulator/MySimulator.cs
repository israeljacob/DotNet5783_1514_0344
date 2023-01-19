using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;

public static class MySimulator
{
    private static volatile bool active = false;
    static readonly BLApi.IBL bl = BLApi.Factory.Get();
    public delegate void SimulationCompleteEventHandler();
    public static event SimulationCompleteEventHandler? SimulationComplete;
    public delegate void OrderProcessingEventHandler(int ID, BO.StatusOfOrder? CurrentStatus, DateTime start, BO.StatusOfOrder? nextStatus, DateTime end);
    public static event OrderProcessingEventHandler? OrderProcessing;

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

    public static void StopSimulation()
    {
        active = false;
    }
    public static void RegisterToSimulationComplete(SimulationCompleteEventHandler handler)
    {
        SimulationComplete += handler;
    }

    public static void UnregisterFromSimulationComplete(SimulationCompleteEventHandler handler)
    {
        SimulationComplete -= handler;
    }
    public static void RegisterToUpdateProgress(OrderProcessingEventHandler handler)
    {
        OrderProcessing += handler;
    }

    public static void UnregisterFromUpdateProgress(OrderProcessingEventHandler handler)
    {
        OrderProcessing -= handler;
    }


}