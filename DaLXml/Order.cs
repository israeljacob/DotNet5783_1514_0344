using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

using DAL;
using DalApi;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Runtime.CompilerServices;

internal class Order : IOrder
{
    DataSourcexml dataSource = DataSourcexml.Instance;


    const string s_orders = @"Orders";

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Order order)
    {
        order.UniqID = DataConfig.GetOrderId;
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (listOrders.FirstOrDefault(ord => ord?.UniqID == order.UniqID) != null)
            throw new DO.IdAlreadyExistException("Order", order.UniqID);
        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
        return order.UniqID;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int ID)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.RemoveAll(o => o?.UniqID == ID) == 0)
            throw new DO.DoesNotExistException("Order",ID); 
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return func == null ? listOrders.OrderBy(ord => ((DO.Order)ord!).UniqID)
                              : listOrders.Where(func).OrderBy(ord => ((DO.Order)ord!).UniqID);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order GetByFunc(Func<DO.Order?, bool> func)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(func)
            ?? throw new DO.DoesNotExistException("Order");
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order GetByID(int ID)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(o => o?.UniqID == ID)
            ?? throw new DO.DoesNotExistException("Order",ID);

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order order)
    {
        try
        {
            Delete(order.UniqID);
        }
        catch 
        {
            throw new DO.DoesNotExistException("Order", order.UniqID);
        }
        List<DO.Order?> listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (listOrders.FirstOrDefault(ord => ord?.UniqID == order.UniqID) != null)
            throw new DO.IdAlreadyExistException("Order", order.UniqID);
        listOrders.Add(order);
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }
}
