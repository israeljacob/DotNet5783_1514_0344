using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Linq;

namespace Dal;

internal class DalOrderItem:IOrderItem
{
    
    DataSource dataSource = DataSource.Instance;
    /// <summary>
    /// Addes a new order item.
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns>The ID of the new order item</returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.UniqID = DataSource.Config.OrderItemID;
        dataSource.orderItems.Add(newOrderItem);
        return newOrderItem.UniqID;
    }

    /// <summary>
    /// Get an order item by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested order item.</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Get(int ID)
    {
        return dataSource.orderItems?.Find(orderItem => orderItem?.UniqID == ID)
          ?? throw new DoesNotExistException("order item", ID);
    }

    /// <summary>
    /// Gets an order item by a boolyan deligate.
    /// </summary>
    /// <param name="func"></param>
    /// <returns>The requested order item.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public OrderItem Get(Func<OrderItem?, bool> func)
    {
         return dataSource.orderItems.First(func) ?? throw new DO.DoesNotExistException("Order item"); 
    }

    ///  /// <summary>
    /// Gets all the order items.
    /// </summary>
    /// <returns>An array that refers to all the order items.</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func = null)
    {
        // If there is no orders.
        if (dataSource.orderItems.Count == 0)
            throw new EmptyException("order item");
        if (func == null)
            return from orderItem in dataSource.orderItems
                   where orderItem != null 
                   select orderItem;
        else
            return from orderItem in dataSource.orderItems
                   where func(orderItem)
                   select orderItem;
    }


    /// <summary>
    /// Updates details of a spesific order item.
    /// </summary>
    /// <param name="updatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem updatedOrderItem)
    {
        int i = 0;
        foreach (OrderItem? orderItem in dataSource.orderItems)// Find the requested order item.
        {
            if (orderItem?.UniqID == updatedOrderItem.UniqID)// If there is such order items.
            {
                dataSource.orderItems[i] = updatedOrderItem;
                return;
            }
            i++;
        }
        throw new DoesNotExistException("Order item", updatedOrderItem.UniqID);// If there is no such order items.
    }

    /// <summary>
    /// Delete an order item by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        if (dataSource.orderItems.RemoveAll(orderItem => orderItem?.UniqID == ID) == 0)// Remove the order item by ID and if the order item does not exists throw an exception.
            throw new DoesNotExistException("Order item",ID);
    }

   
}


    


