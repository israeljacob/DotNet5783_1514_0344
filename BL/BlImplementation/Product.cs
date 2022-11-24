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
        /// Returns all the priducts.
        /// </summary>
        /// <returns>An IEnumerable of all the products.</returns>
        /// <exception cref="MissingAttributeException"></exception>
        IEnumerable<BO.ProductForList> GetListOfProducts()
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
        BO.Product ProductItemForManager(int ID)
        {
            if (ID <= 0)
                throw new BO.InCorrectDetailsException();
            DO.Product product = new DO.Product();/// צריך לבדוק איך להגדיר
            try
            {
                product = dalList.Product.Get(ID);
            }
            catch (Exception e)
            {
                throw new BO.InCorrectDetailsException();
            }
            return new BO.Product
            {
                UniqID = product.UniqID,
                Name = product.Name ?? throw new Exception(),
                Price = product.Price,
                Category = (BO.Category)product.Category,
                InStock = product.InStock
            };
        }
        BO.ProductItem ProductItemForCostemor(int ID, BO.Cart cart)
        {
            if (ID <= 0)
                throw new BO.InCorrectDetailsException();
            DO.Product product = new DO.Product();
            try
            {
                product = dalList.Product.Get(ID);
            }
            catch (Exception e)
            {
                throw new BO.InCorrectDetailsException();
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
        void AddProduct(int ID, string name, double price, BO.Category category, int inStock)
        {
            if (ID <= 0)
                throw new DetailsAreInCorrct("ID");
            if (name == null)
                throw new DetailsAreInCorrct("Name");
            if (price <= 0)
                throw new DetailsAreInCorrct("Price");
            if (inStock < 0)
                throw new DetailsAreInCorrct("InStock");
            try
            {
                dalList.Product.Add(new DO.Product
                {
                    UniqID = ID,
                    Name = name,
                    Price = price,
                    Category = (DO.Category)category,
                    InStock = inStock
                });
            }
            catch (DalApi.DoesNotExistsException e)
            {
                throw new BO.ExistException("Product");
            }
        }
        public void DeleteProduct(int ID)
        {
            if (dalList.OrderItem.GetByProduct(ID) != null)
                throw new ItemExistsInOrder("Product");
            try
            {
                dalList.Product.Delete(ID);
            }
            catch (DalApi.DoesNotExistsException e)
            {
                throw new BO.DoesNotExistsException(e.Message);
            }
        }
        public void UpdateProduct(BO.Product product)
        {
            if (product.UniqID <= 0)
                throw new DetailsAreInCorrct("ID");
            else if (product.Name == null)
                throw new DetailsAreInCorrct("Name");
            else if (product.Price <= 0)
                throw new DetailsAreInCorrct("Price");
            else if (product.InStock < 0)
                throw new DetailsAreInCorrct("InStock");
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
            catch (DalApi.DoesNotExistsException e)
            {
                throw new BO.ExistException(e.Message);
            }
        }
    }
}