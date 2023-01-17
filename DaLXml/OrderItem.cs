using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

using DAL;
using DalApi;
using DO;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{
   // DataSourcexml dataSourcexml = DataSourcexml.Instance;
    const string s_orderItems = "OrderItems"; //Linq to XML
    /// <summary>
    /// assigns a unique ID to it using the GetOrderItemId property from the SerialNumbers class,
    /// loads a list of order items from the XML file, 
    /// checks if the order item already exists in the list, and if not,
    /// it adds the new order item to the list and saves the list to the XML file.
    /// </summary>
    /// <param name="ord"></param>
    /// <returns></returns>
    static DO.OrderItem? getOrderItem(XElement ord)
    {
        if (ord.ToIntNullable("UniqID") is null)
            return null;
        return new DO.OrderItem
        {
            UniqID = (int)ord.Element("UniqID")!,
            ProductID = (int)ord.Element("ProductID")!,
            OrderID = (int)ord.Element("OrderID")!,
            Price = (int)ord.Element("Price")!,
            Amount = (int)ord.Element("Amount")!,
        };
    }

    /// <summary>
    /// his function takes an OrderItem object and converts it to a list of XElements
    /// <param name="orderItem"></param>
    /// <returns></returns>
    static IEnumerable<XElement> createOrderItemElement(DO.OrderItem orderItem)
    {
        yield return new XElement("UniqID", orderItem.UniqID);
        if (orderItem.ProductID != 0)
            yield return new XElement("ProductID", orderItem.ProductID);
        if (orderItem.OrderID != 0)
            yield return new XElement("OrderID", orderItem.OrderID);
        if (orderItem.Price != 0)
            yield return new XElement("Price", orderItem.Price);
        if (orderItem.Amount != 0)
            yield return new XElement("Amount", orderItem.Amount);

    }
    /// <summary>
    /// It first generates a unique ID for the OrderItem using the SerialNumbers class  
    /// then it checks if the ID already exists in the system. 
    /// If the ID already exists, it throws an exception.
    /// Otherwise, it adds the OrderItem to the XML file using the XMLTools class.
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="DO.IdAlreadyExistException"></exception>
    public int Add(DO.OrderItem orderItem)
    {
        orderItem.UniqID = SerialNumbers.GetOrderItemId;
        XElement orderItemsRootElem = XMLTools.LoadListFromXMLElement(s_orderItems);
        if (XMLTools.LoadListFromXMLElement(s_orderItems)?.Elements()
            .FirstOrDefault(ord => ord.ToIntNullable("UniqID") == orderItem.UniqID) is not null)
            throw new DO.IdAlreadyExistException("Order item", orderItem.UniqID);
        orderItemsRootElem.Add(new XElement("OrderItem", createOrderItemElement(orderItem)));
        XMLTools.SaveListToXMLElement(orderItemsRootElem, s_orderItems);
        return orderItem.UniqID;
    }

    /// <summary>
    /// loads a list of order items from the XML file, removes the order item with the specified ID from the list, 
    /// and then saves the updated list to the XML file.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public void Delete(int ID)
    {
        XElement orderItemsRootElem = XMLTools.LoadListFromXMLElement(s_orderItems);

        (orderItemsRootElem.Elements()
            .FirstOrDefault(ord => (int?)ord.Element("UniqID") == ID) ?? throw new DO.DoesNotExistException("Order item", ID))
            .Remove();

        XMLTools.SaveListToXMLElement(orderItemsRootElem, s_orderItems);
    }
    /// <summary>
    /// This method loads a list of order items from the XML file, 
    /// applies a filtering function (if provided) to the list and returns the filtered list.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
    {
        if (func is null)
            return XMLTools.LoadListFromXMLElement(s_orderItems).Elements().Select(o => getOrderItem(o));
        return XMLTools.LoadListFromXMLElement(s_orderItems).Elements().Select(o => getOrderItem(o)).Where(func);
    }
    /// <summary>
    ///  applies a filtering function to the list and returns 
    ///  the first order item that matches the function or throws an exception if no matching order item is found.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public DO.OrderItem GetByFunc(Func<DO.OrderItem?, bool> func)
    {
        IEnumerable<DO.OrderItem?> result = XMLTools.LoadListFromXMLElement(s_orderItems)
            .Elements().Select(o => getOrderItem(o)).Where(func);
        return result.FirstOrDefault() ?? throw new DO.DoesNotExistException("Order item");
    }

    public DO.OrderItem GetByID(int ID)
    {
        return (DO.OrderItem)getOrderItem(XMLTools.LoadListFromXMLElement(s_orderItems)?.Elements()
        .FirstOrDefault(ord => ord.ToIntNullable("UniqID") == ID)
        ?? throw new DO.DoesNotExistException("Order item", ID))!;
    }
    /// <summary>
    /// This method takes an OrderItem object as a parameter, deletes the current
    /// </summary>
    /// <param name="orderItem"></param>
    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.UniqID);
        Add(orderItem);
    }
}
