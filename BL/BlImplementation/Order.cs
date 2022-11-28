using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DocumentFormat.OpenXml.Drawing;

namespace BlImplementation;

internal class Order : BLApi.IOrder
{
    /// <summary>
    /// Handles everything related an order.
    /// </summary>
    IDal dalList = DalList.Instance;

    /// <summary>
    /// Returns all the orders.
    /// </summary>
    /// <returns>An IEnumerable of all the orders.</returns>
    /// <exception cref="MissingAttributeException"></exception>
    public IEnumerable<BO.OrderForList> GetListOfOrders()
    {
        return from order in dalList.Order.GetAll()
               select new BO.OrderForList
               {
                   UniqID = order.UniqID,
                   CustomerName = order.CustomerName,
                   StatusOfOrder = statusOfOrder(order),
                   AmountOfItems = amoutOfItems(order),
                   TotalPrice = totalPrice(order)
               };
    }


    /// <summary>
    /// Get order by Id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    public BO.Order OrderBYID(int ID)
    {
        ///All the exception that comes from DO we catch it, then insert the appropriate exception to list,
        ///in the end if the list is not empty throw AggregateException: kind of build in function
        ///that hold and represents one or more errors.
        var exceptions = new List<Exception>();


        if (ID <= 0)
            exceptions.Add(new BO.InCorrectIntException("Order ID", ID));
        DO.Order order = new DO.Order();
        try
        {
            order = dalList.Order.Get(ID);
        }
        catch (DO.IdNotExistException)
        {
            exceptions.Add(new BO.IdNotExistException("Order", ID));

        }
        if (exceptions.Count != 0)
            throw new AggregateException(exceptions);
        ///return BO entitie
        return new BO.Order
        {
            UniqID = order.UniqID,
            CustomerName = order.CustomerName,
            CustomerAdress = order.CustomerAdress,
            CustomerEmail = order.CustomerEmail,
            OrderDate = order.OrderDate,
            ShipDate = order.ShipDate,
            DeliveryrDate = order.DeliveryrDate,
            orderItems = (List<BO.OrderItem>)orderItems(order.UniqID),
            StatusOfOrder = statusOfOrder(order),
            TotalPrice = totalPrice(order)
        };



    }


    ///Update ship date by ID
     public BO.Order UpdateShipDate(int ID)
    {
        ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
        ///in the end if the list is not empty throw AggregateException: kind of build in function
        ///that hold and represents one or more errors.
        var exceptions = new List<Exception>();

        DO.Order order = new DO.Order();
        ///get the ID
        try { order = dalList.Order.Get(ID); }
        catch(DO.IdNotExistException)
        {
            exceptions.Add(new BO.IdNotExistException("Order ID", ID));
        }
        try {
            if (order.ShipDate > DateTime.MinValue)
                throw new DO.DatesException("Order:", order.ShipDate, DateTime.MinValue); ///Not sure if this is the correct way



        }
        catch(DO.DatesException)
        {
            exceptions.Add(new BO.DatesException("Order:", order.ShipDate, DateTime.MinValue));
        }

        ///update the ship date
        order.ShipDate= DateTime.Now;
         dalList.Order.Update(order);
        if (exceptions.Count != 0)
            throw new AggregateException(exceptions);
        return new BO.Order
        {
            UniqID = order.UniqID,
            CustomerAdress = order.CustomerAdress,
            CustomerEmail = order.CustomerEmail,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            ShipDate = order.ShipDate,
            DeliveryrDate = order.DeliveryrDate,
            orderItems = orderItems(ID),
            StatusOfOrder = BO.StatusOfOrder.Sent,
            TotalPrice = totalPrice(order)
        };
    }
    ///Update Delivery Date by ID
    public BO.Order UpdateDeliveryDate(int ID)
    {

        ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
        ///in the end if the list is not empty throw AggregateException: kind of build in function
        ///that hold and represents one or more errors.
        var exceptions = new List<Exception>();

        DO.Order order = new DO.Order();
        ///get the order by id
        try { order = dalList.Order.Get(ID); }
        catch(DO.IdNotExistException)
        {
            exceptions.Add(new BO.IdNotExistException("Order", ID));
        }
        try {
               if (order.DeliveryrDate > DateTime.MinValue)
                throw new DO.DatesException("Order:", order.ShipDate, DateTime.MinValue); ///Not sure if this is the correct way

        }
        catch (DO.DatesException)
        {
            exceptions.Add(new BO.DatesException("Order:", order.ShipDate, DateTime.MinValue));

        }
        ///Update Delivery
        order.DeliveryrDate = DateTime.Now;
        dalList.Order.Update(order);

        if (exceptions.Count != 0)
            throw new AggregateException(exceptions);
        return new BO.Order
        {
            UniqID = order.UniqID,
            CustomerAdress = order.CustomerAdress,
            CustomerEmail = order.CustomerEmail,
            CustomerName = order.CustomerName,
            OrderDate = order.OrderDate,
            ShipDate = order.ShipDate,
            DeliveryrDate = order.DeliveryrDate,
            orderItems = orderItems(ID),
            StatusOfOrder = BO.StatusOfOrder.Delivered,
            TotalPrice = totalPrice(order)
        };
    }
    ///Track the order status
    public BO.OrderTracking OrderTrack(int ID)
    {
        DO.Order track = new DO.Order();
        try { track = dalList.Order.Get(ID); }
        catch { throw new BO.InCorrectIntException("Order ID", ID); }
        List<Tuple<DateTime, string>> tracking = new List<Tuple<DateTime, string>>();
        if (track.OrderDate > DateTime.MinValue)
            tracking.Add(Tuple.Create(track.OrderDate, "The order has been created"));
        if (track.ShipDate > DateTime.MinValue)
            tracking.Add(Tuple.Create(track.ShipDate, "The order has been sent"));
        if (track.DeliveryrDate > DateTime.MinValue)
            tracking.Add(Tuple.Create(track.DeliveryrDate, "The order has been delivered"));
        return new BO.OrderTracking
        {
            UniqID = track.UniqID,
            StatusOfOrder = statusOfOrder(track),
            ProgressOfOrder = tracking
        };
    }
    ///Update OrderItem Amount 
    public BO.OrderItem UpdateOrderItemAmount(BO.OrderItem orderItem)
    {

        ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
        ///in the end if the list is not empty throw AggregateException: kind of build in function
        ///that hold and represents one or more errors.
        var exceptions = new List<Exception>();
        DO.OrderItem DOorderItem = new DO.OrderItem();
        DO.Order  order = new DO.Order();
        try { 
                DOorderItem = dalList.OrderItem.Get(orderItem.OrderItemID);
            order = dalList.Order.Get(DOorderItem.OrderID);
            }
        catch (DO.IdNotExistException)
        {
            exceptions.Add(new BO.IdNotExistException("Order item", orderItem.OrderItemID));///הבז
        }
        if(order.ShipDate>DateTime.MinValue) { throw new BO.InCorrectDetailsException(); }////לוודא
        return new BO.OrderItem
        {
            OrderItemID = orderItem.OrderItemID,
            ProductID = orderItem.ProductID,
            ProductName = dalList.Product.Get(DOorderItem.ProductID).Name,
            Price = orderItem.Price,
            Amount = orderItem.Amount,
            TotalPrice = orderItem.TotalPrice
        };
    }
    ///Get OrderItem by ID
    public BO.OrderItem GetOrderItem(int ID)
    {
        var exceptions = new List<Exception>();

        DO.OrderItem orderItem = new DO.OrderItem();
        try {orderItem=  dalList.OrderItem.Get(ID); }
        catch (DO.IdNotExistException)
        {
            exceptions.Add(new BO.IdNotExistException("Order", ID));

        }
        return new BO.OrderItem
        {
            OrderItemID = orderItem.UniqID,
            ProductID = orderItem.ProductID,
            ProductName = dalList.Product.Get(orderItem.ProductID).Name,
            Price = orderItem.Price,
            Amount = orderItem.Amount,
            TotalPrice = orderItem.Price * orderItem.Amount
        };
    }
    ///return the status Of Order
    private BO.StatusOfOrder statusOfOrder(DO.Order order)
    {
        BO.StatusOfOrder statusOfOrder = BO.StatusOfOrder.Orderred;
        if (order.DeliveryrDate > DateTime.MinValue)
            statusOfOrder = BO.StatusOfOrder.Delivered;
        else if (order.ShipDate > DateTime.MinValue)
            statusOfOrder = BO.StatusOfOrder.Sent;
        return statusOfOrder;
    }

    private int amoutOfItems(DO.Order order)
    {
        int amountOfItems = 0;
        foreach (DO.OrderItem orderItem in dalList.OrderItem.GetByOrder(order.UniqID))
            amountOfItems++;
        return amountOfItems;
    }

    private double totalPrice(DO.Order order)
    {
        double totalPrice = 0;
        foreach (DO.OrderItem orderItem in dalList.OrderItem.GetByOrder(order.UniqID))
            totalPrice += orderItem.Price * orderItem.Amount;
        return totalPrice;
    }
    ///return orderitems by id
    private IEnumerable<BO.OrderItem> orderItems(int ID)
    {
        return from orderItem in dalList.OrderItem.GetByOrder(ID)
               select new BO.OrderItem
               {
                   OrderItemID = orderItem.UniqID,
                   ProductID = orderItem.ProductID,
                   ProductName = dalList.Product.Get(orderItem.ProductID).Name,
                   Price = orderItem.Price,
                   Amount = orderItem.Amount,
                   TotalPrice = orderItem.Price * orderItem.Amount
               };
    }
    
}
