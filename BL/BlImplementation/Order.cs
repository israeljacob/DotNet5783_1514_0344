using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Drawing;

namespace BlImplementation;

internal class Order : BLApi.IOrder
{
    /// <summary>
    /// Handles everything related an order.
    /// </summary>
    DalApi.IDal dal = DalApi.Factory.Get()!;
    #region Get List Of orders
    /// <summary>
    /// Returns all the orders.
    /// </summary>
    /// <returns>An IEnumerable of all the orders.</returns>
    /// <exception cref="MissingAttributeException"></exception>
    public IEnumerable<BO.OrderForList?> GetListOfOrders(Func<BO.OrderForList?,bool>? func =null)
    {
        try
        {
            if (func == null)
                return from order in dal.Order.GetAll()
                       select new BO.OrderForList
                       {
                           UniqID = order?.UniqID ?? throw new BO.MissingDataException("Order ID"),
                           CustomerName = order?.CustomerName,
                           StatusOfOrder = statusOfOrder(order),
                           AmountOfItems = amoutOfItems(order),
                           TotalPrice = totalPrice(order)
                       };
            else
                return from order in GetListOfOrders()
                       where func(order)
                       orderby order.UniqID
                       select order;
        }
        catch (DO.EmptyException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
    }
    #endregion

    #region Get order by ID
    /// <summary>
    /// Get order by Id
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="AggregateException"></exception>
    public BO.Order OrderByID(int ID)
    {
        if (ID <= 0)
            throw new BO.InCorrectDetailsException("Order ID", ID);
        try
        {
            DO.Order order = dal.Order.GetByID(ID);
            return new BO.Order
            {
                UniqID = order.UniqID,
                CustomerName = order.CustomerName,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryrDate = order.DeliveryrDate,
                orderItems = (from orderItem in dal.OrderItem.GetAll(item => ((DO.OrderItem)item!).OrderID == order.UniqID)
                             let item = (DO.OrderItem)orderItem
                             let product = dal.Product.GetByID(item.ProductID)
                             select new BO.OrderItem()
                             {
                                 UniqID = item.UniqID,
                                 ProductID = item.ProductID,
                                 ProductName = product.Name,
                                 Amount = item.Amount,
                                 Price = item.Price,
                                 TotalPrice = item.Amount * item.Price,
                             }).ToList(),
                StatusOfOrder = statusOfOrder(order),
                TotalPrice = totalPrice(order)
            };
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);

        }
    }
    #endregion

    #region Update total price
    public void UpdateOrder(BO.Order order)
    {
        if (order.DeliveryrDate < order.ShipDate || order.ShipDate<order.OrderDate)
            throw new BO.DateException();
        if((order.DeliveryrDate==null && order.StatusOfOrder == BO.StatusOfOrder.Delivered)|| (order.ShipDate == null && order.StatusOfOrder == BO.StatusOfOrder.Sent) 
            ||(order.StatusOfOrder ==BO.StatusOfOrder.Sent && order.DeliveryrDate!=null)||(order.StatusOfOrder == BO.StatusOfOrder.Orderred && order.ShipDate != null) )
            throw new BO.DateException();
        DO.Order DOorder;
        try {  DOorder = dal.Order.GetByID(order.UniqID); } //get the ID
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
        DOorder.ShipDate = order.ShipDate;
        DOorder.DeliveryrDate= order.DeliveryrDate;
        dal.Order.Update(DOorder);
    }
    #endregion

    

    #region Order track
    /// <summary>
    ///  Track the order status
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="BO.InCorrectIntException"></exception>
    public BO.OrderTracking OrderTrack(int ID)
    {
        DO.Order track = new DO.Order();
        try { track = dal.Order.GetByID(ID); }
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
    #endregion

    #region Update order item amount
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
                DOorderItem = dal.OrderItem.GetByID(orderItem.UniqID);
                order = dal.Order.GetByID(DOorderItem.OrderID);
            }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
        if(order.ShipDate != null) { throw new BO.DatesException("Ship date"); }
        DOorderItem.Amount=orderItem.Amount;
        try
        {
            dal.OrderItem.Update(DOorderItem);
            return new BO.OrderItem
            {
                UniqID = orderItem.UniqID,
                ProductID = orderItem.ProductID,
                ProductName = dal.Product.GetByID(DOorderItem.ProductID).Name,
                Price = orderItem.Price,
                Amount = orderItem.Amount,
                TotalPrice = orderItem.TotalPrice
            };
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
    }
    #endregion

    #region Get order item
    /// <summary>
    /// Get OrderItem by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public BO.OrderItem GetOrderItem(int ID)
    {

        try
        {
            DO.OrderItem orderItem = dal.OrderItem.GetByID(ID);
            return new BO.OrderItem
            {
                UniqID = orderItem.UniqID,
                ProductID = orderItem.ProductID,
                ProductName = dal.Product.GetByID(orderItem.ProductID).Name,
                Price = orderItem.Price,
                Amount = orderItem.Amount,
                TotalPrice = orderItem.Price * orderItem.Amount
            };
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
    }
    #endregion

    #region status of order
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
    #endregion

    #region amout of items
    /// <summary>
    /// Amount of items
    /// </summary>
    /// <param name="order"></param>
    /// <returns>The amount of items</returns>
    private int amoutOfItems(DO.Order? order)
    {
        try
        {
            return dal.OrderItem.GetAll(orderItem => orderItem?.OrderID == order?.UniqID).Count();
        }
        catch(DO.EmptyException ex) { throw new BO.CatchetDOException(ex); }
    }
    #endregion

    #region total price
    /// <summary>
    /// The total price of order
    /// </summary>
    /// <param name="order"></param>
    /// <returns>The total price</returns>
    private double totalPrice(DO.Order? order)
    {
        double totalPrice = 0;
        try
        {
            var v = dal.OrderItem.GetAll().Where(orderItem => orderItem?.OrderID == order?.UniqID);
            foreach (DO.OrderItem? orderItem in v)
                totalPrice += orderItem?.Price * orderItem?.Amount ?? throw new BO.MissingDataException("Order item price");
        }
        catch (DO.EmptyException ex) { throw new BO.CatchetDOException(ex); }
        return totalPrice;

    }
    #endregion
}
