using DO;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Dal;

public class DalProduct
{
    public Product product=new();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public int Add(Product newProduct)
    {
        int i = 0;
        Product product = DataSource.products[i];
        while (product.UniqID >= 2000000 && newProduct.Name!= DataSource.products[i].Name)
        {
            i++;
            product = DataSource.products[i];
        }
        if (DataSource.products[i].Name == newProduct.Name)
            throw new Exception("Product allready exists!");
        int locationInArray = DataSource.AvailableProduct;
        newProduct.UniqID = DataSource.Config.ProductID;
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
        while (DataSource.products[i].UniqID >=2000000 && ID != DataSource.products[i].UniqID)
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

        while (DataSource.products[i + 1].UniqID >= 2000000 )
        {
            i++;
        }
        if (i >= 0)
        {
            products = new Product[i + 1];
            for (int j = 0; j <= i; j++)
            {
                if(DataSource.products[j].UniqID != 0)
                 products[j] = DataSource.products[j];
            }
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
        while (DataSource.products[i].UniqID >=2000000 && updatedProduct.UniqID != DataSource.products[i].UniqID)
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
        while (DataSource.products[i + 1].UniqID >=2000000 && ID != DataSource.products[i + 1].UniqID)
        {
            i++;
        }
        if (DataSource.products[i+1].UniqID == ID)
        {
            for (j = i+1; DataSource.products[j].UniqID >= 2000000 && j<DataSource.products.Length; j++)
            {
                DataSource.products[j] = DataSource.products[j + 1];
            }
        }
        else
            throw new Exception("ID dos not exsist");
    }
}
