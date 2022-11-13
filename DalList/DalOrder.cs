using DalApi;
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
    internal int Add(Order newOrder)
    {
        // Find the first empty place in list and add to there the new order.
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
        int j = searchOrder(ID);
        //if the order doesn't exists throw an exeption.
        if (j == -1)
            throw new Exception("ID dos not exsist");
        // if the order was found
        Order newOrder = DataSource.orders[j];
        return newOrder;
    }

    /// <summary>
    /// Gets all the orders.
    /// </summary>
    /// <returns>An array that refers to all the order.</returns>
    /// <exception cref="Exception"></exception>
    public Order[] ReadAll()
    {
        Order[] orders;
        int i = -1;
        // Checks where is the last orders.
        while (DataSource.orders[i + 1].UniqID >= 1000000)
        {
            i++;
        }
        if (i >= 0)
        {
            // create a new array and copy all orders to the new array.
            orders = new Order[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if (DataSource.orders[j].UniqID != 0)
                    orders[j] = DataSource.orders[j];
            }
        }
        //if there is no any orders throw an exeption.
        else
            throw new Exception("There are no any orders in the system");
        return orders;
    }
    /// <summary>
    /// updates details of a spesific order.
    /// </summary>
    /// <param name="updatedOrder"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order updatedOrder)
    {
        // check if the order exsists. if yes, update the details.
        int j = searchOrder(updatedOrder.UniqID);
        //if the order doesn't exists throw an exeption.
        if (j == -1)
            throw new Exception("ID dos not exsist");
        // if the order was found, update it.
        DataSource.orders[j].CustomerName = updatedOrder.CustomerName;
        DataSource.orders[j].CustomerEmail = updatedOrder.CustomerEmail;
        DataSource.orders[j].CustomerAdress = updatedOrder.CustomerAdress;
        DataSource.orders[j].OrderDate = updatedOrder.OrderDate;
        DataSource.orders[j].ShipDate = updatedOrder.ShipDate;
        DataSource.orders[j].DeliveryrDate = updatedOrder.DeliveryrDate;
    }
    /// <summary>
    /// Delete an order by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        // check if the order exsists. if yes, delete the order.
        int j = searchOrder(ID);
        //if the order doesn't exists throw an exeption.
        if (j == -1)
            throw new Exception("ID dos not exsist");
        // if the order was found, delete it.
        // if it's the last order in array.
        if (j == 99 || DataSource.orders[j + 1].UniqID == 0)
        {
            DataSource.orders[j].UniqID = 0;
            return;
        }
        for (int i = j; DataSource.orders[i].UniqID >= 1000000 && i <= j; i++)
        {
            DataSource.orders[j] = DataSource.orders[j + 1];
        }
    }

    /// <summary>
    /// Auxiliary function to search an order in array by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The location i array</returns>
    private int searchOrder(int ID)
    {

        int i = 0, j = -1;
        // check where is the order in the array.
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