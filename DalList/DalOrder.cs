using DalApi;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Collections;
using Order = DO.Order;

namespace Dal;
/// <summary>
/// CRUD of order.
/// </summary>
internal class DalOrder : IOrder
{

    /// <summary>
    /// Addes a new order.
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns>The ID of the new order.</returns>
    public int Add(Order newOrder)
    {
        newOrder.UniqID = DataSource.Config.OrderID;
        DataSource.orders.Add(newOrder);
        return newOrder.UniqID;
    }
    /// <summary>
    /// Get an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested order.</returns>
    /// <exception cref="Exception"></exception>
    public Order Read(int ID)
    {
        int i = 0;
        foreach (Order order in DataSource.orders)
        {
            if(order.UniqID == ID)
                break;
            i++;
        }
       if(i==DataSource.orders.Count)
         throw new DoesNotExistsException("Order ID");
        // if the order was found
        Order newOrder = DataSource.orders[i];
        return newOrder;
    }

    /// <summary>
    /// Gets all the orders.
    /// </summary>
    /// <returns>An array that refers to all the order.</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<Order> ReadAll()
    {
        IEnumerable<Order> orders = DataSource.orders;
        if(!orders.Any())
            throw new DoesNotExistsException("Any orders");
        return orders;
    }
    /// <summary>
    /// updates details of a spesific order.
    /// </summary>
    /// <param name="updatedOrder"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order updatedOrder)
    {
        int i = 0;
        foreach (Order order in DataSource.orders)
        {
            if (order.UniqID == updatedOrder.UniqID)
            {
                DataSource.orders[i]=updatedOrder;
                break;
            }
            i++;
        }
        if (i == DataSource.orders.Count)
            throw new DoesNotExistsException("Order ID");
    }
    /// <summary>
    /// Delete an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        bool flag = false;
        foreach (Order order in DataSource.orders)
        {
            if (order.UniqID == ID)
            {
                DataSource.orders.Remove(order);
                flag = true;
                break;
            }
           
        }
        if (!flag)
            throw new DoesNotExistsException("Order ID");

    }

}