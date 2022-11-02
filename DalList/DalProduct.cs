﻿using DO;

namespace Dal;

public class DalProduct
{
    public int Add(Product newProduct)
    {
        int locationInArray = DataSource.Config.ProductID;
        DataSource.products[locationInArray] = newProduct;
        return newProduct.UniqID;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product Read(int ID)
    {
        int i = 0;
        while (DataSource.products[i].UniqID > 1000000 && ID != DataSource.products[i].UniqID)
        {
            i++;
        }
        if (DataSource.products[i].UniqID == ID)
        {
            Product newProduct = DataSource.products[i];
            return newProduct;
        }
        else
            throw new Exception("ID dos not exsist");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product[] ReadAll()
    {
        Product[] products;
        int i = -1;

        while (DataSource.products[i + 1].UniqID > 1000000)
        {
            i++;
        }
        if (i >= 0)
        {
            products = new Product[i + 1];
            for (int j = 0; j < i; j++)
                products[j] = DataSource.products[j];
        }
        else throw new Exception("There are no any products in the system");
        return products;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="updated Product"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product updatedProduct)
    {
        int i = 0;
        while (DataSource.products[i].UniqID > 1000000 && updatedProduct.UniqID != DataSource.products[i].UniqID)
        {
            i++;
        }
        if (DataSource.products[i].UniqID == updatedProduct.UniqID)
        {
            DataSource.products[i].Name = updatedProduct.Name;
            DataSource.products[i].Price = updatedProduct.Price;
            DataSource.products[i].Category = updatedProduct.Category;
            DataSource.products[i].InStock = updatedProduct.InStock;
            
        }
        else
            throw new Exception("ID dos not exsist");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public void Delete(int ID)
    {
        int i = -1, j;
        while (DataSource.products[i + 1].UniqID > 1000000 && ID != DataSource.products[i + 1].UniqID)
        {
            i++;
        }
        if (DataSource.products[i].UniqID == ID)
        {
            for (j = i; DataSource.products[j].UniqID > 1000000; j++)
            {
                DataSource.products[j] = DataSource.products[j + 1];
            }
            DataSource.products[j].UniqID = 0;
            DataSource.products[i].Name = "";
            DataSource.products[i].Price = 0;
            DataSource.products[i].Category = null;
            DataSource.products[i].InStock = null;
        }
        else
            throw new Exception("ID dos not exsist");
    }
}