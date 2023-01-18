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


    const string s_orders = @"Orders";
    const string s_data = @"data-config";

    /// <summary>
    /// loads a list of orders from the XML file, checks if the order already exists in the list, 
    /// and if not, it adds the new order to the list and saves the list to the XML file.
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    /// <exception cref="DO.IdAlreadyExistException"></exception>
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
    /// <summary>
    /// This method takes an ID as a parameter, loads a list of orders from the XML file, 
    /// removes all the orders with the specified ID from the list,
    /// and then saves the updated list to the XML file.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public void Delete(int ID)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        if (listOrders.RemoveAll(o => o?.UniqID == ID) == 0)
            throw new DO.DoesNotExistException("Order",ID); 
        XMLTools.SaveListToXMLSerializer(listOrders, s_orders);
    }
    /// <summary>
    /// This method loads a list of orders from the XML file, 
    /// applies a filtering function (if provided) to the list and returns the filtered list sorted by the unique ID.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
    {
        var listOrders = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders)!;
        return func == null ? listOrders.OrderBy(ord => ((DO.Order)ord!).UniqID)
                              : listOrders.Where(func).OrderBy(ord => ((DO.Order)ord!).UniqID);
    }
    /// <summary>
    /// loads a list of orders from the XML file, applies a filtering function to the list and returns the first order 
    /// that matches the function or throws an exception if no matching order is found.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public DO.Order GetByFunc(Func<DO.Order?, bool> func)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(func)
            ?? throw new DO.DoesNotExistException("Order");
    }
    /// <summary>
    ///  loads a list of orders from the XML file, 
    ///  and returns the order with the specified ID or throws an exception if no matching order is found.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public DO.Order GetByID(int ID)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders).FirstOrDefault(o => o?.UniqID == ID)
            ?? throw new DO.DoesNotExistException("Order",ID);

    }
    /// <summary>
    /// takes an Order object as a parameter, deletes the current order with the same ID,
    /// loads a list of orders from the XML file, checks if the order already exists in the list, and if not, 
    /// it adds the updated order to the list and saves the list to the XML file.
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="DO.DoesNotExistException"></exception>
    /// <exception cref="DO.IdAlreadyExistException"></exception>
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
