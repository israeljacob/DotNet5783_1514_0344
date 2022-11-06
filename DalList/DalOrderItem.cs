 using DO;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Dal;

public class DalOrderItem
{




    public int Add(OrderItem newOrderItem)
    {

        int locationInArray = DataSource.AvailableOrderItem;
        DataSource.orderItems[locationInArray] = newOrderItem;
        DataSource.AvailableOrderItem++;
        return newOrderItem.UniqID;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    public OrderItem Read(int ID)
    {
        int i = 0;
        while (DataSource.orderItems[i].UniqID >= 3000000 && ID != DataSource.orderItems[i].UniqID && DataSource.orderItems[i].UniqID != 0)
        {
            i++;
        }
        if (DataSource.orderItems[i].UniqID == ID)
        {
            OrderItem neworderitem = DataSource.orderItems[i];
            return neworderitem;
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
        OrderItem[] ordersItem1;
        int i = -1;

        while (DataSource.orderItems[i + 1].UniqID >= 3000000 && DataSource.orderItems[i + 1].UniqID != 0)
        {
            i++;
        }
        if (i >= 0)
        {
            ordersItem1 = new OrderItem[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if (DataSource.orderItems[j].UniqID != 0)
                    ordersItem1[j] = DataSource.orderItems[j];
            }
        }
        else throw new Exception("There are no any products in the system");
        return ordersItem1;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="updatedOrder"></param>
    /// <exception cref="Exception"></exception>

    public void Update(OrderItem updatedOrderItem)
    {
        int i = 0;
        while (DataSource.orderItems[i].UniqID >= 3000000 && updatedOrderItem.UniqID != DataSource.orderItems[i].UniqID)
        {
            i++;
        }
        if (DataSource.orderItems[i].UniqID == updatedOrderItem.UniqID)
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

    public void Delete(int ID)
    {
        int i = -1, j;
        while (DataSource.orderItems[i + 1].UniqID >= 3000000 && ID != DataSource.orderItems[i + 1].UniqID)
        {
            i++;
        }
        if (DataSource.orderItems[i + 1].UniqID == ID && i < DataSource.AvailableOrderItem - 1)
        {
            for (j = i + 1; DataSource.orderItems[j].UniqID >= 3000000 && j < DataSource.orderItems.Length; j++)
            {
                DataSource.orderItems[j] = DataSource.orderItems[j + 1];
            }
            DataSource.AvailableOrderItem--;
        }
        else
            throw new Exception("ID dos not exsist");
    }
}


    


