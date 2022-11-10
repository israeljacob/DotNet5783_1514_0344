 using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Dal;

public class DalOrderItem
{



    /// <summary>
    /// Addes a new order item.
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns>The ID of the new order item</returns>
    /// <exception cref="Exception"></exception>
    public int Add(OrderItem newOrderItem)
    {
        int i = 0,j=0;
        while (DataSource.orders[i].UniqID >= 1000000 && newOrderItem.OrderID != DataSource.orders[i].UniqID)
        {
            i++;
        }
        while (DataSource.products[j].UniqID >= 2000000 && newOrderItem.ProductID != DataSource.products[j].UniqID)
        {
            j++;
        }
        // Find the first empty place in array and add to there the new order item.
        if (DataSource.orders[i].UniqID == newOrderItem.OrderID && newOrderItem.ProductID == DataSource.orderItems[i].UniqID)
        {
            int locationInArray = DataSource.AvailableOrderItem;
            DataSource.orderItems[locationInArray] = newOrderItem;
            return newOrderItem.UniqID;
        }
        // If the order or the product toesn't exist.
        else
            throw new Exception("ID dos not exsist");
        
    }

    /// <summary>
    /// Get an order item by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested order item.</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Read(int ID)
    {
        int j = searchOrderItem(ID);
        //if the order item doesn't exists throw an exeption.
        if (j == -1)
            throw new Exception("ID dos not exsist");
        // if the order item was found
        OrderItem newOrderItem = DataSource.orderItems[j];
        return newOrderItem;
    }

    /// <summary>
    /// Gets all the order items that connected to a specific order.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>array that refers to all the relevant order items</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] ReadByOrder(int orderID)
    {
        OrderItem[] ordersItem;


        int j = 0;
        //if there is no order items that connected to this order.
        for (int i = 0; i < DataSource.orderItems.Length; i++)
        {
            if (DataSource.orderItems[i].OrderID == orderID)
                j++;
        }
        if(j==0)
            throw new Exception("There are no any orderItems in that order");

        ordersItem = new OrderItem[j];
        j = 0;
        // coppy the order items to a new aaray
        for (int i = 0; i < DataSource.orderItems.Length; i++)
        {
            if (DataSource.orderItems[i].OrderID == orderID)
            {
                ordersItem[j] = DataSource.orderItems[i];
                j++;
            }
        }
        return ordersItem;
    }

    /// <summary>
    /// Gets all the order items of a specific product.
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns>array that refers to all the relevant order items</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] ReadByProduct(int productID)
    {
        OrderItem[] ordersItem;
        int j = 0;
        //if there is no order items of this product.
        for (int i = 0; i < DataSource.orderItems.Length; i++)
        {
            if (DataSource.orderItems[i].ProductID == productID)
                j++;
        }
        if (j == 0)
            throw new Exception("There are no any such orderItems that have been orderred");

        ordersItem = new OrderItem[j];
        j = 0;
        // coppy the order items to a new aaray
        for (int i = 0; i < DataSource.orderItems.Length; i++)
        {
            if (DataSource.orderItems[i].ProductID == productID)
            {
                ordersItem[j] = DataSource.orderItems[i];
                j++;
            }
        }
        return ordersItem;
    }

    ///  /// <summary>
    /// Gets all the order items.
    /// </summary>
    /// <returns>An array that refers to all the order items.</returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] ReadAll()
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
        else throw new Exception("There are no any orderItems in the system");
        return ordersItem;
    }


    /// <summary>
    /// Updates details of a spesific order item.
    /// </summary>
    /// <param name="updatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem updatedOrderItem)
    {
        // check if the order item exsists. if yes, update the details.
        int j = searchOrderItem(updatedOrderItem.UniqID);
        //if the order item doesn't exists throw an exeption.
        if (j == -1)
            throw new Exception("ID dos not exsist");
        // if the order item was found, update it.
        DataSource.orderItems[j].OrderID = updatedOrderItem.OrderID;
        DataSource.orderItems[j].ProductID = updatedOrderItem.ProductID;
        DataSource.orderItems[j].Price = updatedOrderItem.Price;
        DataSource.orderItems[j].Amount = updatedOrderItem.Amount;
    }

    /// <summary>
    /// Delete an order item by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        // check if the order item exsists. if yes, delete the order item.
        int j = searchOrderItem(ID);
        //if the order item doesn't exists throw an exeption.
        if (j == -1)
            throw new Exception("ID dos not exsist");
        // if the order item was found, delete it.
        // if it's the last order item in array.
        if (j == 199 || DataSource.orderItems[j + 1].UniqID == 0)
        {
            DataSource.orderItems[j].UniqID = 0;
            DataSource.orderItems[j].OrderID = 0;
            DataSource.orderItems[j].ProductID = 0;
            return;
        }
        for (int i = j; DataSource.orderItems[i].UniqID >= 1000000 && i <= j; i++)
        {
            DataSource.orderItems[j] = DataSource.orderItems[j + 1];
        }
    }

    /// <summary>
    /// Auxiliary function to search an order in array by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The location i array</returns>
    private int searchOrderItem(int ID)
    {

        int i = 0, j = -1;
        // check where is the order item in the array.
        while (DataSource.orders[i].UniqID >= 2000000)
        {
            // If the location was founded, save it in j;
            if (ID != DataSource.orders[i].UniqID)
            {
                j = i;
                break;
            }
            i++;
        }
        return j;
    }
}


    


