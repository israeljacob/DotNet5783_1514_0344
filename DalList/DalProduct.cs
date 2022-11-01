using DO;

namespace Dal;

public class DalProduct
{
    public int Add(Product newProduct)
    {
        Product product = newProduct;
        int locationInArray = DataSource.Config.ProductID;
        DataSource.orders[locationInArray] = newProduct;
        product.UniqID = DataSource.Config.OrderID;

        return product.UniqID;
    }
}
