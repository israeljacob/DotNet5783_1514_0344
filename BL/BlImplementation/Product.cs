using BLApi;
using Dal;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    internal class Product: BLApi.IProduct
    {
        IDal dalList = new DalList();

        List<BO.ProductForList> GetListOfProducts()
        {
           BO.ProductForList productForLists1 = new BO.ProductForList();
            IEnumerable<DO.Product> products = dalList.Product.GetAll();
            List<BO.ProductForList> productForLists = new List<BO.ProductForList>();
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
        BO.Product ProductItemForManager(int ID)
        {
            if (ID <= 0)
                throw new BO.InCorrectDetails();
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
        BO.ProductItem ProductItemForCostemor(int ID, Cart cart)
        {
            if (ID <= 0)
                throw new BO.InCorrectDetails();
            DO.Product product = new DO.Product();
            try
            {
                product = dalList.Product.Get(ID);
            }
            catch
            {
                throw new BO.DoesNotExistsException("product ID");
            }
            BO.ProductItem BOproductItem = new BO.ProductItem();
            BOproductItem.UniqID = product.UniqID;
            BOproductItem.Name = product.Name;
            BOproductItem.Price = product.Price;
            BOproductItem.Category = (BO.Category)product.Category;
            if (product.InStock == 0)
                BOproductItem.InStock = false;
            else
                BOproductItem.InStock= true;
           // BOproductItem.Amount=cart.orderItems.Find(ProductItem => ProductItem.ProductID== ID).Amount;
            return BOproductItem;
        }
        void AddProduct(int ID, string name, double price, BO.Category category, int inStock)
        {
            if(ID<= 0 || name==null || price<=0) throw new BO.InCorrectDetails();
            DO.Product product = new DO.Product() { UniqID= ID,Name=name,Price=price,Category = (DO.Category)category, InStock=inStock };
            try { dalList.Product.Add(product); }
            catch { throw new BO.ExistException("Product ID"); }
        }

    }
}
