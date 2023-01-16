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

    public void Delete(int ID)
    {
        XElement orderItemsRootElem = XMLTools.LoadListFromXMLElement(s_orderItems);

        (orderItemsRootElem.Elements()
            .FirstOrDefault(ord => (int?)ord.Element("UniqID") == ID) ?? throw new DO.DoesNotExistException("Order item", ID))
            .Remove();

        XMLTools.SaveListToXMLElement(orderItemsRootElem, s_orderItems);
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
    {
        if (func is null)
            return XMLTools.LoadListFromXMLElement(s_orderItems).Elements().Select(o => getOrderItem(o));
        return XMLTools.LoadListFromXMLElement(s_orderItems).Elements().Select(o => getOrderItem(o)).Where(func);
    }

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

    public void Update(DO.OrderItem orderItem)
    {
        Delete(orderItem.UniqID);
        Add(orderItem);
    }
}
