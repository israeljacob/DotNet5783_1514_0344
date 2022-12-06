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
    public IEnumerable<BO.OrderForList?> GetListOfOrders(Func<BO.Order?,bool>? func =null)
    {
        if(func==null)
            return from order in dalList.Order.GetAll()
                   select new BO.OrderForList
                   {
                       UniqID = order?.UniqID ?? throw new BO.MissingDataException("Order ID"),
                       CustomerName = order?.CustomerName,
                       StatusOfOrder = statusOfOrder(order),
                       AmountOfItems = amoutOfItems(order),
                       TotalPrice = totalPrice(order)
                   };
        else
            return from order in dalList.Order.GetAll(/*func*/)
                   select new BO.OrderForList
                   {
                       UniqID = order?.UniqID ?? throw new BO.MissingDataException("Order ID"),
                       CustomerName = order?.CustomerName,
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
            throw new BO.InCorrectDetailsException("Order ID", ID);
        try
        {
            DO.Order order = dalList.Order.Get(ID);
            return new BO.Order
            {
                UniqID = order.UniqID,
                CustomerName = order.CustomerName,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryrDate = order.DeliveryrDate,
                orderItems = (List<BO.OrderItem>)orderItems(order.UniqID)!,
                StatusOfOrder = statusOfOrder(order),
                TotalPrice = totalPrice(order)
            };
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.IdNotExistException(ex);

        }
    }

    public BO.Order OrderBYID(Func<BO.Product,bool> func)
    {
        try
        {
            DO.Order order = dalList.Order.Get(/*func*/1);
            return new BO.Order
            {
                UniqID = order.UniqID,
                CustomerName = order.CustomerName,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryrDate = order.DeliveryrDate,
                orderItems = (List<BO.OrderItem>)orderItems(order.UniqID)!,
                StatusOfOrder = statusOfOrder(order),
                TotalPrice = totalPrice(order)
            };
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.IdNotExistException(ex);

        }
    }

    /// <summary>
    /// Update ship date by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    public BO.Order UpdateShipDate(int ID)
    {
        DO.Order order = new DO.Order();
        try { order = dalList.Order.Get(ID); } //get the ID
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.IdNotExistException(ex);
        }
            if (order.ShipDate != null)
                throw new BO.DatesException("Ship date"); 
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
        try { order = dalList.Order.Get(ID); }//get the order by id
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.IdNotExistException(ex);
        }
        if (order.DeliveryrDate != null)
           throw new BO.DatesException("Delivery date");
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
        catch { throw new BO.InCorrectDetailsException("Order ID", ID); }
        List<Tuple<DateTime?, string?>?> tracking = new List<Tuple<DateTime?, string?>?>();
        if (track.OrderDate != null)
            tracking.Add(Tuple.Create(track.OrderDate, "The order has been created")!);
        if (track.ShipDate != null)
            tracking.Add(Tuple.Create(track.ShipDate, "The order has been sent")!);
        if (track.DeliveryrDate != null)
            tracking.Add(Tuple.Create(track.DeliveryrDate, "The order has been delivered")!);
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
                DOorderItem = dalList.OrderItem.Get(orderItem.UniqID);
                order = dalList.Order.Get(DOorderItem.OrderID);
            }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.IdNotExistException(ex);
        }
        if(order.ShipDate != null) { throw new BO.DatesException("Ship date"); }
        DOorderItem.Amount=orderItem.Amount;
        dalList.OrderItem.Update(DOorderItem);
        return new BO.OrderItem
        {
            UniqID = orderItem.UniqID,
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

        try
        {
            DO.OrderItem orderItem = dalList.OrderItem.Get(ID);
            return new BO.OrderItem
            {
                UniqID = orderItem.UniqID,
                ProductID = orderItem.ProductID,
                ProductName = dalList.Product.Get(orderItem.ProductID).Name,
                Price = orderItem.Price,
                Amount = orderItem.Amount,
                TotalPrice = orderItem.Price * orderItem.Amount
            };
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.IdNotExistException(ex);
        }
    }
    /// <summary>
    /// return the status Of Order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.StatusOfOrder? statusOfOrder(DO.Order? order)
    {
        BO.StatusOfOrder? statusOfOrder = BO.StatusOfOrder.Orderred;
        if (order?.DeliveryrDate != null)
            statusOfOrder = BO.StatusOfOrder.Delivered;
        else if (order?.ShipDate != null)
            statusOfOrder = BO.StatusOfOrder.Sent;
        return statusOfOrder;
    }

    /// <summary>
    /// Amount of items
    /// </summary>
    /// <param name="order"></param>
    /// <returns>The amount of items</returns>
    private int amoutOfItems(DO.Order? order)
    {
        int amountOfItems = 0;
        Func<DO.OrderItem? , bool> func = orderItem => orderItem?.OrderID == order?.UniqID;
        foreach (DO.OrderItem? orderItem in dalList.OrderItem.GetAll(func))
            amountOfItems++;
        return amountOfItems;
    }

    /// <summary>
    /// The total price of order
    /// </summary>
    /// <param name="order"></param>
    /// <returns>The total price</returns>
    private double totalPrice(DO.Order? order)
    {
        double totalPrice = 0;
        Func<DO.OrderItem?, bool> func = orderItem => orderItem?.OrderID == order?.UniqID;
        foreach (DO.OrderItem? orderItem in dalList.OrderItem.GetAll(func))
            totalPrice += orderItem?.Price * orderItem?.Amount ?? throw new BO.MissingDataException("Order item price");
        return totalPrice;
    }
    /// <summary>
    /// return orderitems by id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    private IEnumerable<BO.OrderItem?>? orderItems(int ID)
    {
        Func<DO.OrderItem?, bool> func = orderItem => orderItem?.OrderID ==ID;
            return from orderItem in dalList.OrderItem.GetAll(func)
               select new BO.OrderItem
               {
                   UniqID = orderItem?.UniqID ?? throw new BO.MissingDataException("Order ID"),
                   ProductID = orderItem?.ProductID ?? throw new BO.MissingDataException("Product ID"),
                   ProductName = dalList.Product.Get(orderItem?.ProductID ?? throw new BO.MissingDataException("Product name")).Name,
                   Price = orderItem?.Price ?? throw new BO.MissingDataException("Order item price"),
                   Amount = orderItem?.Amount ?? throw new BO.MissingDataException("Order item amount"),
                   TotalPrice = orderItem?.Price * orderItem?.Amount ?? throw new BO.MissingDataException("Order item total price")
               };
    }
}
