using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Order = DO.Order;

namespace Dal;

public class DalOrder
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns></returns>
    public int Add(Order newOrder)
    {
        newOrder.UniqID = DataSource.Config.OrderID;
        int locationInArray = DataSource.Config.OrderID;
        DataSource.orders[locationInArray] =newOrder;
        return newOrder.UniqID;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order Read(int ID)
    {
        int i = 0;
        while (DataSource.orders[i].UniqID>1000000 && ID!= DataSource.orders[i].UniqID)
        {
            i++;
        }
        if (DataSource.orders[i].UniqID == ID)
        {
            Order newOrder = DataSource.orders[i];
            return newOrder;
        }
        else
            throw new Exception("ID dos not exsist");
    }

    public Order[] ReadAll()
    {
        Order[] orders;
        int i = -1;

        while (DataSource.orders[i+1].UniqID > 1000000 )
        {
            i++;
        }
        if (i >= 0)
        {
            orders = new Order[i + 1];
            for (int j = 0; j < i; j++)
                orders[j] = DataSource.orders[j];
        }
        else throw new Exception("There are no any orders in the system");
        return orders;
    }
    public void Update(Order updatedOrder)
    {
        int i = 0;
        while (DataSource.orders[i].UniqID > 1000000 && updatedOrder.UniqID != DataSource.orders[i].UniqID)
        {
            i++;
        }
        if (DataSource.orders[i].UniqID == updatedOrder.UniqID)
        {
             DataSource.orders[i].CustomerName= updatedOrder.CustomerName;
            DataSource.orders[i].CustomerEmail = updatedOrder.CustomerEmail;
            DataSource.orders[i].CustomerAdress = updatedOrder.CustomerAdress;
            DataSource.orders[i].OrderDate = updatedOrder.OrderDate;
            DataSource.orders[i].ShipDate = updatedOrder.ShipDate;
            DataSource.orders[i].DeliveryrDate = updatedOrder.DeliveryrDate;
        }
        else
            throw new Exception("ID dos not exsist");
    }
    public Order Delete(int ID)
    {
        return new Order();
    }
}
