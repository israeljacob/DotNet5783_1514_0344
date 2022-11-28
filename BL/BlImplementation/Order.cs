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
        if (ID <= 0)
            throw new BO.InCorrectIntException("Order ID", ID);
        DO.Order order = new DO.Order();
        try
        {
            order = dalList.Order.Get(ID);
        }
        catch (DO.IdNotExistException)
        {
            throw new BO.IdNotExistException("Order", ID);

        }
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


    /// <summary>
    /// pdate ship date by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
     public BO.Order UpdateShipDate(int ID)
    {
        DO.Order order = new DO.Order();
        ///get the ID
        try { order = dalList.Order.Get(ID); }
        catch(DO.IdNotExistException)
        {
            throw new BO.IdNotExistException("Order ID", ID);
        }
        try {
            if (order.ShipDate > DateTime.MinValue)
                throw new DO.DatesException("Order:", order.ShipDate, DateTime.MinValue); ///Not sure if this is the correct way



        }
        catch(DO.DatesException)
        {
            throw new BO.DatesException("Order:", order.ShipDate, DateTime.MinValue);
        }

        ///update the ship date
        order.ShipDate= DateTime.Now;
         dalList.Order.Update(order);
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
    /// <summary>
    /// Update Delivery Date by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    public BO.Order UpdateDeliveryDate(int ID)
    {

        DO.Order order = new DO.Order();
        ///get the order by id
        try { order = dalList.Order.Get(ID); }
        catch(DO.IdNotExistException)
        {
            throw new BO.IdNotExistException("Order", ID);
        }
        try {
               if (order.DeliveryrDate > DateTime.MinValue)
                throw new DO.DatesException("Order:", order.ShipDate, DateTime.MinValue); ///Not sure if this is the correct way

        }
        catch (DO.DatesException)
        {
            throw new BO.DatesException("Order:", order.ShipDate, DateTime.MinValue);

        }
        order.DeliveryrDate = DateTime.Now;
        dalList.Order.Update(order);

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
    /// <summary>
    ///  Track the order status
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="BO.InCorrectIntException"></exception>
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

    /// <summary>
    ///  Update OrderItem Amount 
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="BO.InCorrectDetailsException"></exception>
    public BO.OrderItem UpdateOrderItemAmount(BO.OrderItem orderItem)
    {

        DO.OrderItem DOorderItem = new DO.OrderItem();
        DO.Order  order = new DO.Order();
        try { 
                DOorderItem = dalList.OrderItem.Get(orderItem.OrderItemID);
            order = dalList.Order.Get(DOorderItem.OrderID);
            }
        catch (DO.IdNotExistException)
        {
            throw new BO.IdNotExistException("Order item", orderItem.OrderItemID);///הבז
        }
        if(order.ShipDate>DateTime.MinValue) { throw new BO.InCorrectDetailsException("O", orderItem.OrderItemID); }////לוודא
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
    /// <summary>
    /// Get OrderItem by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public BO.OrderItem GetOrderItem(int ID)
    {

        DO.OrderItem orderItem = new DO.OrderItem();
        try {orderItem=  dalList.OrderItem.Get(ID); }
        catch (DO.IdNotExistException)
        {
            throw new BO.IdNotExistException("Order", ID);

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
    /// <summary>
    /// return the status Of Order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.StatusOfOrder statusOfOrder(DO.Order order)
    {
        BO.StatusOfOrder statusOfOrder = BO.StatusOfOrder.Orderred;
        if (order.DeliveryrDate > DateTime.MinValue)
            statusOfOrder = BO.StatusOfOrder.Delivered;
        else if (order.ShipDate > DateTime.MinValue)
            statusOfOrder = BO.StatusOfOrder.Sent;
        return statusOfOrder;
    }

    /// <summary>
    /// Amount of items
    /// </summary>
    /// <param name="order"></param>
    /// <returns>The amount of items</returns>
    private int amoutOfItems(DO.Order order)
    {
        int amountOfItems = 0;
        foreach (DO.OrderItem orderItem in dalList.OrderItem.GetByOrder(order.UniqID))
            amountOfItems++;
        return amountOfItems;
    }

    /// <summary>
    /// The total price of order
    /// </summary>
    /// <param name="order"></param>
    /// <returns>The total price</returns>
    private double totalPrice(DO.Order order)
    {
        double totalPrice = 0;
        foreach (DO.OrderItem orderItem in dalList.OrderItem.GetByOrder(order.UniqID))
            totalPrice += orderItem.Price * orderItem.Amount;
        return totalPrice;
    }
    /// <summary>
    /// return orderitems by id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
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
