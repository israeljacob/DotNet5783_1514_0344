using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Collections;
using Order = DO.Order;


namespace Dal;
/// <summary>
/// CRUD of order.
/// </summary>
internal class DalOrder : IOrder
{
    DataSource dataSource = DataSource.Instance;
    /// <summary>
    /// Addes a new order.
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns>The ID of the new order.</returns>
    public int Add(Order newOrder)
    {
        newOrder.UniqID = DataSource.Config.OrderID;
        dataSource.orders.Add(newOrder);
        return newOrder.UniqID;
    }
    /// <summary>
    /// Get an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested order.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public Order Get(int ID)
    {
        Order? tempOrder= dataSource.orders.Find(order => order.UniqID == ID);
        //If the order was not found.
        if (tempOrder.Value.UniqID == 0)
         throw new IdNotExistException("Order",ID);
        // If the order was found.
        return (Order)tempOrder;
    }

    /// <summary>
    /// Gets all the orders.
    /// </summary>
    /// <returns>An array that refers to all the order.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public IEnumerable<Order> GetAll()
    {
        
        IEnumerable<Order>? orders = dataSource.orders.Where(order => order.UniqID>0);
        // If there is no orders.
        if (orders==null)
            throw new Empty("There is no Orders at all");
        return orders;
    }

    /// <summary>
    /// updates details of a spesific order.
    /// </summary>
    /// <param name="updatedOrder"></param>
    /// <exception cref="DoesNotExistsException"></exception>
    public void Update(Order updatedOrder)
    {
        // Find the requested order.
        int i = 0;
        foreach (Order order in dataSource.orders)
        {
            // If the order was found.
            if (order.UniqID == updatedOrder.UniqID)
            {
                dataSource.orders[i]=updatedOrder;
                return;
            }
            i++;
        }
        // If the order was not found.
        throw new IdNotExistException("Order", updatedOrder.UniqID);
    }
    /// <summary>
    /// Delete an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="DoesNotExistsException"></exception>
    public void Delete(int ID)
    {
        // Remove the order by ID and if the order does not exists throw an exception.
        if(dataSource.orders.RemoveAll(order=>order.UniqID==ID)==0)
            throw new IdNotExistException("Order",ID);
    }

    public void CompareDates(DateTime d1,DateTime d2)
    {
        if (d1 > d2)
            throw new DatesException("Order:", d1, d2);
    }
}