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
    /// <summary>
    /// Addes a new product.
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns>The ID of the new product.</returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product newProduct)
    {
        // If there is such a product throw an exception.
        foreach(Product pro in DataSource.products)
            if(pro.UniqID==newProduct.UniqID)
                throw new ExistException("Product");

        DataSource.products.Add(newProduct);
        return newProduct.UniqID;
    }
    
    /// <summary>
    /// Get a product by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The requested product.</returns>
    /// <exception cref="Exception"></exception>
    public Product Read(int ID)
    {
        int i = 0;
        foreach (Product product in DataSource.products)
        {
            if (product.UniqID == ID)
                break;
            i++;
        }
        if (i == DataSource.products.Count)
            throw new DoesNotExistsException("Product ID");
        // if the order was found
        Product newOrder = DataSource.products[i];
        return newOrder;
    }
    /// <summary>
    /// Gets all the product
    /// </summary>
    /// <returns>array that refers to all the product.</returns>
    /// <exception cref="Exception"></exception>
    public IEnumerable<Product> ReadAll()
    {

        Product[] products;
        int i = -1;
        // Checks where is the last product.
        while (DataSource.products[i + 1].UniqID >= 2000000 )
        {
            i++;
        }
        if (i >= 0)
        {
            // create a new array and copy all products to the new array.
            products = new Product[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if(DataSource.products[j].UniqID != 0)
                 products[j] = DataSource.products[j];
            }
        }
        //if there is no any products throw an exeption.
        else throw new DoesNotExistsException("Any product ID");
        return products;
    }
    /// <summary>
    /// updates details of a spesific product.
    /// </summary>
    /// <param name="updatedProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product updatedProduct)
    {
        int i = 0;
        foreach (Product pro in DataSource.products)
        {
            if (updatedProduct.UniqID == pro.UniqID)
            {
                DataSource.products[i] = updatedProduct;
                break;
            }
            i++;
        }
        if (i == DataSource.products.Count)
            throw new DoesNotExistsException("Product ID");
    }
    /// <summary>
    /// Delete a product by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        bool flag = false;
        foreach (Product product in DataSource.products)
        {
            if (product.UniqID == ID)
            {
                DataSource.products.Remove(product);
                flag = true;
                break;
            }

        }
        if (!flag)
            throw new DoesNotExistsException("Product ID");

    }
}
