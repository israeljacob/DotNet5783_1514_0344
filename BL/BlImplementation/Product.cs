using BLApi;
using BO;
using Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Product: BLApi.IProduct
    {
        DalList dalList = new DalList();

        List<BO.ProductForList> GetListOfProducts()
        {
           ProductForList productForLists1 = new ProductForList();
            IEnumerable<DO.Product> products = dalList.Product.GetAll();
            List<ProductForList> productForLists = new List<ProductForList>();
            foreach (DO.Product product in products)
            {
                productForLists1.UniqID=product.UniqID;
                productForLists1.Name=product.Name;
                productForLists1.Price=product.Price;
                productForLists1.Category = (BO.Category)product.Category;
                productForLists.Add(productForLists1);
            }
            return productForLists;
        }
        BO.Product ProductItem(int ID)
        {
            if (ID <= 0)
                throw new UnPositiveIDException();
            DO.Product product= new DO.Product();
            try
            {
                product = dalList.Product.Get(ID);
            }
            catch
            {
                throw new BO.DoesNotExistsException("product ID");
            }
            BO.Product BOproduct = new BO.Product();
            BOproduct.UniqID= product.UniqID;
            BOproduct.Name= product.Name;
            BOproduct.Price= product.Price;
            BOproduct.Category = (BO.Category)product.Category;
            BOproduct.InStock= product.InStock;
            return BOproduct;
        }
    }
}
