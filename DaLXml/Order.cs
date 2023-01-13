﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

internal class Order : IOrder
{
    const string s_orders = @"orders";
    public int Add(DO.Order order)
    {
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (listOrders.FirstOrDefault(ord => ord?.UniqID == order.UniqID) != null)
            throw new DO.IdAlreadyExistException("Order", order.UniqID);
        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
        throw new NotImplementedException();
    }

    public void Delete(int ID)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.RemoveAll(p => p?.UniqID == ID) == 0)
            throw new DO.DoesNotExistException("Order",ID); 
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return func == null ? listOrders.OrderBy(ord => ((DO.Order)ord!).UniqID)
                              : listOrders.Where(func).OrderBy(ord => ((DO.Order)ord!).UniqID);
    }

    public DO.Order GetByFunc(Func<DO.Order?, bool> func)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(func)
            ?? throw new DO.DoesNotExistException("Order");
    }

    public DO.Order GetByID(int ID)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(p => p?.UniqID == ID)
            ?? throw new DO.DoesNotExistException("Order",ID);

    }

    public void Update(DO.Order order)
    {
        try
        {
            Delete(order.UniqID);
            Add(order);
        }
        catch 
        {
            throw new DO.DoesNotExistException("Order", order.UniqID);
        }
    }
}
