using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Dal;

internal class DalOrderItem:IOrderItem
{



    /// <summary>
    /// Addes a new order item.
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns>The ID of the new order item</returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem newOrderItem)
    {
        DataSource.orderItems.Add(newOrderItem);
        return newOrderItem.UniqID;
    }

    /// <summary>
    /// Get an order item by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested order item.</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Read(int ID)
    {
        int i = 0;
        foreach (OrderItem orderItem in DataSource.orderItems)
        {
            if (orderItem.UniqID == ID)
                break;
            i++;
        }
        if (i == DataSource.orderItems.Count)
            throw new DoesNotExistsException("Order item ID");
        // if the order item was found
        OrderItem newOrder = DataSource.orderItems[i];
        return newOrder;
    }

    /// <summary>
    /// Gets all the order items that connected to a specific order.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>array that refers to all the relevant order items</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem> ReadByOrder(int orderID)
    {
       IEnumerable<OrderItem> orderItems = DataSource.orderItems.Where(x=>x.OrderID==orderID);
        if (!orderItems.Any())
            throw new DoesNotExistsException("Any of those order items");
        return orderItems;
    }

    /// <summary>
    /// Gets all the order items of a specific product.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>array that refers to all the relevant order items</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem> ReadByProduct(int productID)
    {
        IEnumerable<OrderItem> orderItems = DataSource.orderItems.Where(x => x.ProductID == productID);
        if (!orderItems.Any())
            throw new DoesNotExistsException("Any of those order items");
        return orderItems;
    }

    ///  /// <summary>
    /// Gets all the order items.
    /// </summary>
    /// <returns>An array that refers to all the order items.</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<OrderItem> ReadAll()
    {
        OrderItem[] ordersItem;
        int i = -1;
        // Checks where is the last order item.
        while (DataSource.orderItems[i + 1].UniqID >= 3000000)
        {
            i++;
        }
        if (i >= 0)
        {
            // Create a new array and copy all order items to the new array.
            ordersItem = new OrderItem[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if (DataSource.orderItems[j].UniqID != 0)
                    ordersItem[j] = DataSource.orderItems[j];
            }
        }
        //if there is no any order items throw an exeption.
        else throw new Exception("Any order items");
        return ordersItem;
    }


    /// <summary>
    /// Updates details of a spesific order item.
    /// </summary>
    /// <param name="updatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem updatedOrderItem)
    {
        int i = 0;
        foreach (OrderItem orderItem in DataSource.orderItems)
        {
            if (orderItem.UniqID == updatedOrderItem.UniqID)
            {
                DataSource.orderItems[i] = updatedOrderItem;
                break;
            }
            i++;
        }
        if (i == DataSource.orderItems.Count)
            throw new DoesNotExistsException("Order item ID");
    }

    /// <summary>
    /// Delete an order item by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        bool flag = false;
        foreach (OrderItem orderItem in DataSource.orderItems)
        {
            if (orderItem.UniqID == ID)
            {
                DataSource.orderItems.Remove(orderItem);
                flag = true;
                break;
            }

        }
        if (!flag)
            throw new DoesNotExistsException("Order item ID");
    }
}


    


