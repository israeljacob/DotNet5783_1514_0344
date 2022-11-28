using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;

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
        OrderItem? tempOrderItem = dataSource.orderItems.Find(orderItem => orderItem.UniqID == ID);
        // If the order item was not found
        if (tempOrderItem == null)
            throw new IdNotExistException("Order item",ID);
        // If the order item was found
        return (OrderItem)tempOrderItem;
    }

    /// <summary>
    /// Gets all the order items that connected to a specific order.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>array that refers to all the relevant order items</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem> GetByOrder(int orderID)
    {
       IEnumerable<OrderItem>? orderItems = dataSource.orderItems.Where(x=>x.OrderID==orderID);
        // If there is no such order items.

        if (orderItems==null)
            throw new IdNotExistException("Order in order items", orderID);
        return orderItems;
    }

    /// <summary>
    /// Gets all the order items of a specific product.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>array that refers to all the relevant order items</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem> GetByProduct(int productID)
    {
        IEnumerable<OrderItem>? orderItems = dataSource.orderItems.Where(x => x.ProductID == productID);
        // If there is no such order items.
        if (orderItems == null)
            throw new IdNotExistException("Product in order items", productID);
        return orderItems;
    }

    ///  /// <summary>
    /// Gets all the order items.
    /// </summary>
    /// <returns>An array that refers to all the order items.</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem> GetAll()
    {
        IEnumerable<OrderItem>? orderItems = dataSource.orderItems.Where(x => x.UniqID>0);
        // If there is no such order items.
        if (orderItems == null)
            throw new Empty("order items");
        return orderItems;
    }


    /// <summary>
    /// Updates details of a spesific order item.
    /// </summary>
    /// <param name="updatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem updatedOrderItem)
    {
        // Find the requested order item.
        int i = 0;
        foreach (OrderItem orderItem in dataSource.orderItems)
        {
            // If there is such order items.
            if (orderItem.UniqID == updatedOrderItem.UniqID)
            {
                dataSource.orderItems[i] = updatedOrderItem;
                return;
            }
            i++;
        }
        // If there is no such order items.
        throw new IdNotExistException("Order item", updatedOrderItem.UniqID);
    }

    /// <summary>
    /// Delete an order item by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        // Remove the order item by ID and if the order item does not exists throw an exception.
        if (dataSource.orderItems.RemoveAll(orderItem => orderItem.UniqID == ID) == 0)
            throw new IdNotExistException("Order item",ID);
    }
}


    


