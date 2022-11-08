using DO;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;

namespace Dal;
/// <summary>
/// CRUD of product.
/// </summary>
public class DalProduct
{
    /// <summary>
    /// Addes a new product.
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns>The ID of the new product.</returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product newProduct)
    {
        int i = 0;
        // Check if the ID exsist allready.
        Product product = DataSource.products[i];
        while (product.UniqID >= 2000000 && newProduct.Name!= DataSource.products[i].Name)
        {
            i++;
            product = DataSource.products[i];
        }
        // if the product exists throw an exeption.
        if (DataSource.products[i].Name == newProduct.Name)
            throw new Exception("Product allready exists!");

        // find the first empty place in array and add to there the new product.
        int locationInArray = DataSource.AvailableProduct;
        newProduct.UniqID = DataSource.Config.ProductID;
        DataSource.products[locationInArray] = newProduct;
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
        int j = searchProduct(ID);
        //if the product doesn't exists throw an exeption.
        if (j==-1)
            throw new Exception("ID dos not exsist");
        // if the product was found
        Product newProduct = DataSource.products[j];
        return newProduct;
    }
    /// <summary>
    /// Gets all the product
    /// </summary>
    /// <returns>array that refers to all the product.</returns>
    /// <exception cref="Exception"></exception>
    public Product[] ReadAll()
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
        else throw new Exception("There are no any products in the system");
        return products;
    }
    /// <summary>
    /// updates details of a spesific product.
    /// </summary>
    /// <param name="updatedProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product updatedProduct)
    {
        // check if the product exsists. if yes, update the details.
        int j = searchProduct(updatedProduct.UniqID);
        //if the product doesn't exists throw an exeption.
        if (j==-1)
            throw new Exception("ID dos not exsist");
        // if the product was found, update it.
        DataSource.products[j].Name = updatedProduct.Name;
        DataSource.products[j].Price = updatedProduct.Price;
        DataSource.products[j].Category = updatedProduct.Category;
        DataSource.products[j].InStock = updatedProduct.InStock;

    }
    /// <summary>
    /// Delete a product by ID.
    /// </summary>
    /// <param name="ID"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int ID)
    {
        // check if the product exsists. if yes, delete the product.
        int j = searchProduct(ID);
        //if the product doesn't exists throw an exeption.
        if (j == -1)
            throw new Exception("ID dos not exsist");
        // if the product was found, delete it.
        // if it's the last order in array.
        if (j == 49 || DataSource.products[j + 1].UniqID == 0)
        {
            DataSource.products[j].UniqID = 0;
            return;
        }
        for (int i = j; DataSource.products[i].UniqID >= 1000000 && i <= j; i++)
        {
            DataSource.products[j] = DataSource.products[j + 1];
        }
    }
    /// <summary>
    /// Auxiliary function to search a product in array by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns>The location i array</returns>
    private int searchProduct(int ID)
    {

        int i = 0,j=-1;
        // check where is the product in the array.
        while (DataSource.products[i].UniqID >= 2000000)
        {
            // If the location was founded, save it in j;
            if(ID != DataSource.products[i].UniqID)
            {
                j = i;
                break;
            }
            i++;
        }
        return j;
    }
}
