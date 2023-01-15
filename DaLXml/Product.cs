using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_products = "Products"; //Linq to XML

   
    public int Add(DO.Product product)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (listProducts.FirstOrDefault(ord => ord?.UniqID == product.UniqID) != null)
            throw new DO.IdAlreadyExistException("Product", product.UniqID);
        listProducts.Add(product);
        XMLTools.SaveListToXMLSerializer(listProducts, s_products);
        return product.UniqID;
    }

    public void Delete(int ID)
    {
        var listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);

        if (listProducts.RemoveAll(p => p?.UniqID == ID) == 0)
            throw new DO.DoesNotExistException("Product", ID);
        XMLTools.SaveListToXMLSerializer(listProducts, s_products);
    }
   
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
    {
        var listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products)!;
        return func == null ? listProducts.OrderBy(ord => ((DO.Product)ord!).UniqID)
                              : listProducts.Where(func).OrderBy(ord => ((DO.Product)ord!).UniqID);
    }
       

    public DO.Product GetByFunc(Func<DO.Product?, bool> func)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products).FirstOrDefault(func)
            ?? throw new DO.DoesNotExistException("Product");
    }

    public DO.Product GetByID(int ID)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products).FirstOrDefault(p => p?.UniqID == ID)
            ?? throw new DO.DoesNotExistException("Product", ID);
    }

    public void Update(DO.Product product)
    {
        try
        {
            Delete(product.UniqID);
            Add(product);
        }
        catch
        {
            throw new DO.DoesNotExistException("Product", product.UniqID);
        }
    }
}
