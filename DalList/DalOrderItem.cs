 using DO;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Dal;

public class DalOrderItem
{
    public int Add(OrderItem newOrderItem)
    {
        int locationInArray = DataSource.Config.OrderItemID;
        DataSource.orderItems[locationInArray] = newOrderItem;
        return newOrderItem.UniqID;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem Read(int uniqID)
    {
        int i = 0;
        while (DataSource.orderItems[i].UniqID >= 3000000 && uniqID != DataSource.orderItems[i].UniqID)
        {
            i++;
        }
        if (DataSource.orderItems[i].UniqID == uniqID )
        {
            OrderItem newOrderItem = DataSource.orderItems[i];
            return newOrderItem;
        }
        else
            throw new Exception("ID dos not exsist");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] ReadAll()
    {
        OrderItem[] orderItems;
        int i = -1;

        while (DataSource.orderItems[i + 1].UniqID >= 3000000)
        {
            i++;
        }
        if (i >= 0)
        {
            orderItems = new OrderItem[i + 1];
            for (int j = 0; j < i; j++)
                orderItems[j] = DataSource.orderItems[j];
        }
        else throw new Exception("There are no any orderItems in the system");
        return orderItems;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="updated orderItems"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem updatedOrderItem)
    {
        int i = 0;
        while (DataSource.orderItems[i].UniqID >= 3000000 & updatedOrderItem.UniqID != DataSource.orderItems[i].UniqID)
        {
            i++;
        }
        if (DataSource.orderItems[i].UniqID == updatedOrderItem.UniqID )
        {
            DataSource.orderItems[i].OrderID = updatedOrderItem.OrderID;
            DataSource.orderItems[i].ProductID = updatedOrderItem.ProductID;
            DataSource.orderItems[i].Price = updatedOrderItem.Price;
            DataSource.orderItems[i].Amount = updatedOrderItem.Amount;

        }
        else
            throw new Exception("ID dos not exsist");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public void Delete(int uniqID)
    {
        int i = -1, j;
        while (DataSource.orderItems[i].UniqID >= 3000000 && uniqID != DataSource.orderItems[i].UniqID)
        {
            i++;
        }
        if (DataSource.orderItems[i].UniqID == uniqID )
        {
            for (j = i; DataSource.orderItems[j].UniqID >= 3000000; j++)
            {
                DataSource.orderItems[j] = DataSource.orderItems[j + 1];
            }
            DataSource.orderItems[j].UniqID = 0;
            DataSource.orderItems[j].ProductID = 0;
            DataSource.orderItems[j].OrderID = 0;
            DataSource.AvailableOrderItem--;
        }
        else
            throw new Exception("ID dos not exsist");
    }
}


