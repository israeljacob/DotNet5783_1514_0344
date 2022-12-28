using DalApi;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections;
using System.Linq;

namespace Dal;
/// <summary>
/// CRUD of product.
/// </summary>
internal class DalProduct:IProduct
{
    DataSource dataSource = DataSource.Instance;
    /// <summary>
    /// Addes a new product.
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns>The ID of the new product.</returns>
    /// <exception cref="ExistException"></exception>
    public int Add(DO.Product newProduct)
    {
        if(dataSource.products.Exists(product => product?.UniqID == newProduct.UniqID))// If there is such a product throw an exception.
            throw new DO.IdAlreadyExistException("Product",newProduct.UniqID);
        dataSource.products.Add(newProduct); // Add the new product.
        return newProduct.UniqID;
    }
    
    /// <summary>
    /// Gets a product by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested product.</returns>
    /// <exception cref="Exception"></exception>
    public DO.Product GetByID(int ID)
    {
        return dataSource.products?.Find(product => product?.UniqID == ID)
            ?? throw new DO.DoesNotExistException("product", ID);
    }
    /// <summary>
    ///  Gets a product by a boolyan deligate.
    /// </summary>
    /// <param name="func"></param>
    /// <returns>The requested product.</returns>
    /// <exception cref="DoesNotExistsException"></exception>
    public DO.Product GetByFunc(Func<DO.Product?, bool> func)
    {
         return dataSource.products?.First(func) ?? throw new DO.DoesNotExistException("product");
    }
    /// <summary>
    /// Gets all the product
    /// </summary>
    /// <returns>array that refers to all the product.</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<DO.Product?> GetAll( Func<DO.Product?, bool>? func = null)
    {
        if (dataSource.products?.Count == 0)// If there is no orders.
            throw new DO.EmptyException("product");
        if(func == null)
            return from product in dataSource.products
                   where product != null
                   select product;
        else
            return from product in dataSource.products
                   where func(product)
                   select product;
    }
    /// <summary>
    /// updates details of a spesific product.
    /// </summary>
    /// <param name="updatedProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(DO.Product updatedProduct)
    {
        int i = 0;
        foreach (DO.Product? pro in dataSource.products) // Find the requested product.
        {
            if (updatedProduct.UniqID == pro?.UniqID)// If the product was found
            {
                dataSource.products[i] = updatedProduct;
                return;
            }
            i++;
        }
        throw new DO.DoesNotExistException("product", updatedProduct.UniqID);// If the product was not found
    }
    /// <summary>
    /// Delete a product by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        if (dataSource.products.RemoveAll(product => product?.UniqID == ID) == 0)// Remove the product by ID and if the product does not exists throw an exception.
            throw new DO.DoesNotExistException("product",ID);
    }

   
}
