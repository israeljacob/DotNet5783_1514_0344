﻿using DO;
using Order = DO.Order;

namespace Dal;

public class DalOrder
{
    
    /// <summary>
    /// Add new order to array
    /// </summary>
    /// <param name="newOrder"></param>
    /// <returns></returns>
    public int Add(Order newOrder)
    {

        int locationInArray = DataSource.AvailableOrder;
        DataSource.orders[locationInArray] = newOrder;
        DataSource.AvailableOrder++;
        return newOrder.UniqID;
    }
    /// <summary>
    /// by given id we can find the order, and then return the order
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
  
    public Order Read(int ID)
    {
        int i = 0;
        while (DataSource.orders[i].UniqID >= 1000000 && ID != DataSource.orders[i].UniqID && DataSource.orders[i].UniqID != 0)
        {
            i++;
        }
        if (DataSource.orders[i].UniqID == ID)
        {
            Order neworder = DataSource.orders[i];
            return neworder;
        }
        else
            throw new Exception("ID dos not exsist");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order[] ReadAll()
    {
        Order[] orders;
        int i = -1;

        while (DataSource.orders[i + 1].UniqID >= 1000000 && DataSource.orders[i + 1].UniqID != 0)
        {
            i++;
        }
        if (i >= 0)
        {
            orders = new Order[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if (DataSource.orders[j].UniqID != 0)
                    orders[j] = DataSource.orders[j];
            }
        }
        else throw new Exception("There are no any products in the system");
        return orders;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="updatedOrder"></param>
    /// <exception cref="Exception"></exception>
  
    public void Update(Order updatedOrder)
    {
        int i = 0;
        while (DataSource.orders[i].UniqID >= 1000000 && updatedOrder.UniqID != DataSource.orders[i].UniqID)
        {
            i++;
        }
        if (DataSource.orders[i].UniqID == updatedOrder.UniqID)
        {
            DataSource.orders[i].CustomerName = updatedOrder.CustomerName;
            DataSource.orders[i].CustomerEmail = updatedOrder.CustomerEmail;
            DataSource.orders[i].CustomerAdress = updatedOrder.CustomerAdress;
            DataSource.orders[i].OrderDate = updatedOrder.OrderDate;
            DataSource.orders[i].ShipDate = updatedOrder.ShipDate;
            DataSource.orders[i].DeliveryrDate = updatedOrder.DeliveryrDate;

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
        while (DataSource.orders[i + 1].UniqID >= 1000000 && ID != DataSource.orders[i + 1].UniqID)
        {
            i++;
        }
        if (DataSource.orders[i + 1].UniqID == ID && i < DataSource.AvailableOrder - 1)
        {
            for (j = i + 1; DataSource.orders[j].UniqID >= 1000000 && j < DataSource.orders.Length; j++)
            {
                DataSource.orders[j] = DataSource.orders[j + 1];
            }
            DataSource.AvailableOrder--;
        }
        else
            throw new Exception("ID dos not exsist");
    }
}
