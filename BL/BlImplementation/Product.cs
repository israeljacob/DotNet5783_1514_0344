using BLApi;
using BO;
using Dal;
using DalApi;
using DO;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    /// <summary>
    /// Takes care on all of wh
    /// </summary>
    internal class Product : BLApi.IProduct
    {
        /// <summary>
        /// Handles everything related a product
        /// </summary>
        IDal dalList = DalList.Instance;

        /// <summary>
        /// Returns all the products.
        /// </summary>
        /// <returns>An IEnumerable of all the products.</returns>
        /// <exception cref="MissingAttributeException"></exception>
        public IEnumerable<BO.ProductForList> GetListOfProducts()
        {

            return from product in dalList.Product.GetAll()
                   select new BO.ProductForList
                   {
                       UniqID = product.UniqID,
                       Name = product.Name,
                       Price = product.Price,
                       Category = (BO.Category)product.Category
                   };


        }

        /// <summary>
        /// Display more features if the mameger asking product by Id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="Exception"></exception>
        public BO.Product ProductItemForManager(int ID)
        {
            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.
            if (ID <= 0)
                throw new BO.InCorrectIntException("Product ID", ID);
            DO.Product product = new DO.Product();
            try
            {
                product = dalList.Product.Get(ID);
            }
            catch (DO.IdNotExistException)
            {
                throw new BO.IdNotExistException("Product", ID);
            }
            ///return new entitie product 
            return new BO.Product
            {
                UniqID = product.UniqID,
                Name = product.Name ?? throw new Exception(),
                Price = product.Price,
                Category = (BO.Category)product.Category,
                InStock = product.InStock
            };
            
        }

        /// <summary>
        /// Display less features if the customer asking product by Id
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BO.ProductItem ProductItemForCostemor(int ID, BO.Cart cart)
        {
            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.

            if (ID <= 0)
                throw new BO.InCorrectIntException("Product ID", ID);
            DO.Product product = new DO.Product();
            try
            {
                product = dalList.Product.Get(ID);
            }
            catch (DO.IdNotExistException)
            {
                throw new BO.IdNotExistException("Product ID", ID);

            }

            bool inStock = true;
            if (product.InStock == 0)
                inStock = false;
            int amount = 0;
            if (cart.orderItems?.FirstOrDefault(X => X.ProductID == ID) != null)
                amount = cart.orderItems.First(X => X.ProductID == ID).Amount;
            return new BO.ProductItem
            {
                UniqID = product.UniqID,
                Name = product.Name ?? throw new Exception(),
                Price = product.Price,
                Category = (BO.Category)product.Category,
                InStock = inStock,
                Amount = amount
            };
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        /// <param name="inStock"></param>
        /// <exception cref="AggregateException"></exception>
        public void AddProduct(int ID, string name, double price, BO.Category category, int inStock)
        {
            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.
            Product product = new Product();

            if (ID <= 0)
                throw new BO.InCorrectIntException("Product ID", ID);
            if (name == null)
                throw new BO.InCorrectStringException("Product Name", name);
            if (price <= 0)
                throw new BO.InCorrectDoubleException("Product Price", price);
            if (inStock < 0)
                throw new BO.InCorrectIntException("Product Instock", inStock);
            try
            {
                int returnedID = dalList.Product.Add(new DO.Product
                {
                    UniqID = ID,
                    Name = name,
                    Price = price,
                    Category = (DO.Category)category,
                    InStock = inStock
                });
            }
            catch (DO.ExistException)
            {
                throw new BO.IdAlreadyExistException("Product", ID);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="AggregateException"></exception>
        public void DeleteProduct(int ID)
        {
            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.

            if (dalList.OrderItem.GetByProduct(ID) != null)
                throw new BO.ItemExistsInOrderException("Product");
            try
            {
                dalList.Product.Delete(ID);
            }
            catch (DO.IdNotExistException)
            {
                throw new BO.IdNotExistException("Product", ID);
            }
        }
        /// <summary>
        /// Uptate Product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="AggregateException"></exception>
        public void UpdateProduct(BO.Product product)
        {
            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.


            if (product.UniqID <= 0)
                throw new BO.InCorrectIntException("Product ID", product.UniqID);
            else if (product.Name == null)
                throw new BO.InCorrectStringException("Product Name", product.Name);
            else if (product.Price <= 0)
                throw new BO.InCorrectDoubleException("Product Price", product.Price);
            else if (product.InStock < 0)
                throw new BO.InCorrectIntException("Product Instock", product.InStock);
            try
            {
                dalList.Product.Update(new DO.Product
                {
                    UniqID = product.UniqID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (DO.Category)product.Category,
                    InStock = product.InStock
                });
            }
            catch (DO.IdNotExistException)
            {
                throw new BO.IdNotExistException("Product", product.UniqID);
            }

        }
    }
}