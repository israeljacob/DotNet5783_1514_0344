using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.VariantTypes;

namespace BlImplementation;

internal class Order : BLApi.IOrder
{
    /// <summary>
    /// Handles everything related an order.
    /// </summary>
    DalApi.IDal dal = DalApi.Factory.Get()!;

    #region Get List Of orders for simulator
    /// <summary>
    /// Returns all the orders.
    /// </summary>
    /// <returns>An IEnumerable of all the orders.</returns>
    /// <exception cref="MissingAttributeException"></exception>
    public int? ForSimulator()
    {
        DO.Order? order1, order2;
        try
        {
            order1 = (from order in dal.Order.GetAll()
                      where order?.ShipDate == null
                      orderby order?.OrderDate
                      select order).FirstOrDefault();
            order2 = (from order in dal.Order.GetAll()
                      where order?.ShipDate != null && order?.DeliveryrDate == null
                      orderby order?.ShipDate
                      select order).FirstOrDefault();
        }
        catch (DO.EmptyException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
        if (order1 is null && order2 is null)
            return null;
        else if (order1 is not null && order2 is not null)
            return order1?.OrderDate < order2?.ShipDate ? order1?.UniqID : order2?.UniqID;
        else
            return order1 is null ? order2?.UniqID : order1?.UniqID;

    }
    #endregion

    #region Get List Of orders
    /// <summary>
    /// Returns all the orders.
    /// </summary>
    /// <returns>An IEnumerable of all the orders.</returns>
    /// <exception cref="MissingAttributeException"></exception>
    public IEnumerable<BO.OrderForList?> GetListOfOrders(Func<BO.OrderForList?, bool>? func = null)
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
    public BO.Order GetOrderByID(int ID)
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
        if (order.DeliveryrDate < order.ShipDate || order.ShipDate < order.OrderDate)
            throw new BO.DateException();
        if (((order.DeliveryrDate == null || order.ShipDate == null) && order.StatusOfOrder == BO.StatusOfOrder.Delivered) || (order.ShipDate == null && order.StatusOfOrder == BO.StatusOfOrder.Sent)
            || (order.StatusOfOrder == BO.StatusOfOrder.Sent && order.DeliveryrDate != null) || (order.StatusOfOrder == BO.StatusOfOrder.Orderred && (order.ShipDate != null || order.DeliveryrDate != null)))
            throw new BO.DateException();
        DO.Order DOorder;
        try { DOorder = dal.Order.GetByID(order.UniqID); } //get the ID
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
        DOorder.ShipDate = order.ShipDate;
        DOorder.DeliveryrDate = order.DeliveryrDate;
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
    public BO.Order UpdateOrderItemAmount(BO.Order BOorder, BO.OrderItem orderItem)
    {
        BO.OrderItem orderOrderItem = BOorder.orderItems?.FirstOrDefault(item => item?.UniqID == orderItem.UniqID)!;
        int difference = orderItem.Amount - orderOrderItem.Amount;
        DO.Product product = new DO.Product();
        DO.OrderItem DOorderItem = new DO.OrderItem();
        DO.Order DOorder = new DO.Order();
        try
        {
            product = dal.Product.GetByID(orderItem.ProductID);
            DOorderItem = dal.OrderItem.GetByID(orderItem.UniqID);
            DOorder = dal.Order.GetByID(DOorderItem.OrderID);
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
        if (product.InStock < difference) throw new BO.missingItemsException(product.UniqID, difference);
        if (DOorder.ShipDate != null) { throw new BO.DatesException("Ship date"); }
        if (orderItem.Amount == 0)
        {
            try
            {
                dal.OrderItem.Delete(DOorderItem.UniqID);
            }
            catch (Exception ex) { throw new BO.CatchetDOException(ex); }
            BOorder.orderItems?.ToList().RemoveAll(item => item?.UniqID == orderItem.UniqID);
            return BOorder;
        }
        DOorderItem.Amount = orderItem.Amount;
        try
        {
            product.InStock -= difference;
            dal.Product.Update(product);
            dal.OrderItem.Update(DOorderItem);
        }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        }
        foreach (BO.OrderItem? orderItem1 in BOorder.orderItems!)
            if (orderItem1?.UniqID == orderItem.UniqID)
                orderItem1.Amount = orderItem.Amount;
        return BOorder;
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
        catch (DO.EmptyException ex) { throw new BO.CatchetDOException(ex); }
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
