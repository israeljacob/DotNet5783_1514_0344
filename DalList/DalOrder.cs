using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Collections;



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
    public int Add(DO.Order newOrder)
    {
        newOrder.UniqID = DataSource.Config.OrderID;
        dataSource.orders.Add(newOrder);
        return newOrder.UniqID;
    }
    /// <summary>
    /// Gets an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested order.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public DO.Order Get(int ID)
    {
        return dataSource.orders?.Find(order => order?.UniqID == ID)
           ?? throw new DoesNotExistException("order", ID);
    }
    /// <summary>
    ///  Gets an order by a boolyan deligate.
    /// </summary>
    /// <param name="func"></param>
    /// <returns>The requested order.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public DO.Order Get(Func<DO.Order?, bool> func)
    {
        return dataSource.orders?.First(func) 
            ?? throw new DoesNotExistException("order"); 
    }

    /// <summary>
    /// Gets all the orders.
    /// </summary>
    /// <returns>An array that refers to all the order.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func)
    {
        // If there is no orders.
        if (dataSource.orders.Count == 0)
            throw new EmptyException("orders");
        if (func == null)
            return from order in dataSource.orders
                   where order != null
                   select order; 
        else
            return from order in dataSource.orders
                   where func(order)
                   select order;
    }

    /// <summary>
    /// updates details of a spesific order.
    /// </summary>
    /// <param name="updatedOrder"></param>
    /// <exception cref="DoesNotExistsException"></exception>
    public void Update(DO.Order updatedOrder)
    {
        int i = 0;
        foreach (DO.Order? order in dataSource.orders)// Find the requested order.
        {
            if (order?.UniqID == updatedOrder.UniqID)// If the order was found.
            {
                dataSource.orders[i]=updatedOrder;
                return;
            }
            i++;
        }
        throw new DoesNotExistException("order", updatedOrder.UniqID);// If the order was not found.
    }
    /// <summary>
    /// Delete an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="DoesNotExistsException"></exception>
    public void Delete(int ID)
    {
        if(dataSource.orders.RemoveAll(order=>order?.UniqID ==ID)==0)// Remove the order by ID and if the order does not exists throw an exception.
            throw new DoesNotExistException("Order",ID);
    }
}