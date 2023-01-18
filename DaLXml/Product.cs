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
using System.Runtime.CompilerServices;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_products = "Products"; //Linq to XML

    /// <summary>
    ///  adds it to a list of products loaded from an XML file using the XMLTools.LoadListFromXMLSerializer method. 
    /// If a product with the same ID already exists in the list, it throws an IdAlreadyExistException. 
    /// The updated list of products is then saved to the XML file using the XMLTools.SaveListToXMLSerializer method. 
    /// The method returns the unique ID of the added product.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="DO.IdAlreadyExistException"></exception>
    public int Add(DO.Product product)
    {
        List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (listProducts.FirstOrDefault(ord => ord?.UniqID == product.UniqID) != null)
            throw new DO.IdAlreadyExistException("Product", product.UniqID);
        listProducts.Add(product);
        XMLTools.SaveListToXMLSerializer(listProducts, s_products);
        return product.UniqID;
    }
    /// <summary>
    /// takes an integer ID as an argument and removes the product with that ID from the list of products,
    /// loaded from the XML file using the XMLTools.LoadListFromXMLSerializer method. If no product with the specified ID exists in the list it throws a DoesNotExistException.
    /// The updated list of products is then saved to the XML file using the XMLTools.SaveListToXMLSerializer method.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public void Delete(int ID)
    {
        var listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);

        if (listProducts.RemoveAll(p => p?.UniqID == ID) == 0)
            throw new DO.DoesNotExistException("Product", ID);
        XMLTools.SaveListToXMLSerializer(listProducts, s_products);
    }
    /// <summary>
    /// returns an enumerable list of products, loaded from the XML file using the XMLTools.LoadListFromXMLSerializer method. 
    /// If the optional argument is provided, 
    /// it filters the list of products based on the provided function. 
    /// The returned list is ordered by the unique ID of the products.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
    {
        var listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products)!;
        return func == null ? listProducts.OrderBy(ord => ((DO.Product)ord!).UniqID)
                              : listProducts.Where(func).OrderBy(ord => ((DO.Product)ord!).UniqID);
    }

    /// <summary>
    /// returns the first product that matches the provided function.
    /// If no matching product is found, it throws a DoesNotExistException.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public DO.Product GetByFunc(Func<DO.Product?, bool> func)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products).FirstOrDefault(func)
            ?? throw new DO.DoesNotExistException("Product");
    }
    /// <summary>
    /// takes an integer ID as an argument and returns the product with that ID.
    /// If no product with the specified ID exists, it throws a DoesNotExistException.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="DO.DoesNotExistException"></exception>
    public DO.Product GetByID(int ID)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products).FirstOrDefault(p => p?.UniqID == ID)
            ?? throw new DO.DoesNotExistException("Product", ID);
    }
    /// <summary>
    /// updates the product with the same unique ID in the list of products.
    /// If the product does not exist in the list, it throws a DoesNotExistException. The method first deletes the product with the same ID, 
    /// then adds the updated product to the list and saves the updated list to the XML file using the XMLTools.SaveListToXMLSerializer method.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="DO.DoesNotExistException"></exception>
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
