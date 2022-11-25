using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace BlImplementation;

internal class Order
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
    IEnumerable<BO.OrderForList> GetListOfOrders()
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

    BO.Order OrderBYID(int ID)
    {
        if (ID <= 0)
            throw new BO.InCorrectDetailsException();
        DO.Order order = new DO.Order();/// צריך לבדוק איך להגדיר
        try
        {
            order = dalList.Order.Get(ID);
        }
        catch (Exception e)
        {
            throw new BO.InCorrectDetailsException();
        }
        return new BO.Order
        {
            UniqID = order.UniqID,
            CustomerName = order.CustomerName,
            CustomerAdress = order.CustomerAdress,
            CustomerEmail = order.CustomerEmail,
            OrderDate = order.OrderDate,
            ShipDate = order.ShipDate,
            DeliveryrDate = order.DeliveryrDate,
            orderItems = orderItems(order.UniqID),
            StatusOfOrder = statusOfOrder(order),
            TotalPrice = totalPrice(order)
        };
    }



    BO.StatusOfOrder statusOfOrder(DO.Order order)
    {
        BO.StatusOfOrder statusOfOrder = BO.StatusOfOrder.Orderred;
        if (order.DeliveryrDate > DateTime.MinValue)
            statusOfOrder = BO.StatusOfOrder.Delivered;
        else if (order.ShipDate > DateTime.MinValue)
            statusOfOrder = BO.StatusOfOrder.Sent;
        return statusOfOrder;
    }

    int amoutOfItems(DO.Order order)
    {
        int amountOfItems = 0;
        foreach (DO.OrderItem orderItem in dalList.OrderItem.GetByOrder(order.UniqID))
            amountOfItems++;
        return amountOfItems;
    }

    double totalPrice(DO.Order order)
    {
        double totalPrice = 0;
        foreach (DO.OrderItem orderItem in dalList.OrderItem.GetByOrder(order.UniqID))
            totalPrice += orderItem.Price * orderItem.Amount;
        return totalPrice;
    }

    IEnumerable<BO.OrderItem> orderItems(int ID)
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
