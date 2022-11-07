 using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Dal;

public class DalOrderItem
{




    public int Add(OrderItem newOrderItem)
    {
        int i = 0,j=0;
        while (DataSource.orders[i].UniqID >= 1000000 && newOrderItem.OrderID != DataSource.orders[i].UniqID)
        {
            i++;
        }
        while (DataSource.products[j].UniqID >= 2000000 && newOrderItem.ProductID != DataSource.products[i].UniqID)
        {
            j++;
        }
        if (DataSource.orders[i].UniqID == newOrderItem.OrderID && newOrderItem.ProductID == DataSource.products[i].UniqID)
        {
            int locationInArray = DataSource.AvailableOrderItem;
            DataSource.orderItems[locationInArray] = newOrderItem;
            DataSource.AvailableOrderItem++;
            return newOrderItem.UniqID;
        }
        else
            throw new Exception("ID dos not exsist");
        
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
        while (DataSource.orderItems[i].UniqID >= 3000000 && ID != DataSource.orderItems[i].UniqID)
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
    public OrderItem[] ReadByOrder(int orderID)
    {
        OrderItem[] ordersItem;
        int j = 0;

        for (int i = 0; i < DataSource.orderItems.Length; i++)
        {
            if (DataSource.orderItems[i].OrderID == orderID)
                j++;
        }
        if(j==0)
            throw new Exception("There are no any products in that order");

        ordersItem = new OrderItem[j];
        j = 0;
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
    public OrderItem[] ReadByProduct(int productID)
    {
        OrderItem[] ordersItem;
        int j = 0;

        for (int i = 0; i < DataSource.orderItems.Length; i++)
        {
            if (DataSource.orderItems[i].ProductID == productID)
                j++;
        }
        if (j == 0)
            throw new Exception("There are no any such products that have been orderred");

        ordersItem = new OrderItem[j];
        j = 0;
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
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem[] ReadAll()
    {
        OrderItem[] ordersItem;
        int i = -1;

        while (DataSource.orderItems[i + 1].UniqID >= 3000000)
        {
            i++;
        }
        if (i >= 0)
        {
            ordersItem = new OrderItem[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if (DataSource.orderItems[j].UniqID != 0)
                    ordersItem[j] = DataSource.orderItems[j];
            }
        }
        else throw new Exception("There are no any products in the system");
        return ordersItem;
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
        if (DataSource.orderItems[i + 1].UniqID == ID)
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


    


