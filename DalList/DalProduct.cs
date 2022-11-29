using DalApi;
using DO;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections;

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
    public int Add(Product newProduct)
    {
        // If there is such a product throw an exception.
        if(dataSource.products.Exists(product => product?.UniqID == newProduct.UniqID))
            throw new IdAlreadyExistException("Product",newProduct.UniqID);
        // Add the new product.
        dataSource.products.Add(newProduct);
        return newProduct.UniqID;
    }
    
    /// <summary>
    /// Get a product by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested product.</returns>
    /// <exception cref="Exception"></exception>
    public Product Get(int ID)
    {
        Product? tempProduct = dataSource.products.Find(product => product?.UniqID == ID);
        // If the product was not found
        if (tempProduct.Value.Name == null)
            throw new IdNotExistException("Product",ID);
        // If the product was found
        return (Product)tempProduct;
    }
    /// <summary>
    /// Gets all the product
    /// </summary>
    /// <returns>array that refers to all the product.</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<Product?>? GetAll( Func<Product?, bool>? func = null)
    {
        // If there is no orders.
        if (dataSource.products.Count == 0)
            throw new Empty("There is no products at all");
        List<Product?> result = new List<Product?>();
        foreach (var product in dataSource.products)
        {
            if (product == null) continue;
            result.Add(product);
        }

        return from product in result
               select product;
    }
    /// <summary>
    /// updates details of a spesific product.
    /// </summary>
    /// <param name="updatedProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product updatedProduct)
    {
        // Find the requested product.
        int i = 0;
        foreach (Product pro in dataSource.products)
        {
            // If the product was found
            if (updatedProduct.UniqID == pro.UniqID)
            {
                dataSource.products[i] = updatedProduct;
                return;
            }
            i++;
        }
        // If the product was not found
        throw new IdNotExistException("Product", updatedProduct.UniqID);
    }
    /// <summary>
    /// Delete a product by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        // Remove the product by ID and if the product does not exists throw an exception.
        if (dataSource.products.RemoveAll(product => product?.UniqID == ID) == 0)
            throw new IdNotExistException("Product",ID);
    }
}
