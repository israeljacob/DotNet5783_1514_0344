using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BLApi;
using Dal;
using BO;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        IDal dalList = DalList.Instance;
        public BO.Cart AddToCart(BO.Cart cart, int ID)
        {

            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.
            var exceptions = new List<Exception>();

            foreach (var item in cart.orderItems)
            {
                if (item.ProductID == ID)
                {
                    try
                    {
                        dalList.Product.Get(ID);

                    }
                    catch (DO.IdNotExist)
                    {
                        
                        exceptions.Add(new BO.IdNotExistException("Cart ID", ID));
                    }
                    if(dalList.Product.Get(ID).InStock == 0)///לא מבין למה צריך
                        exceptions.Add(new BO.InCorrectIntException("Cart ID", ID));

                    item.Amount++;
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;
                    return cart;
                }
            }
            DO.Product product = new DO.Product();// לתקן
            try { product = dalList.Product.Get(ID); }
            catch (DO.IdNotExist)
            {
                exceptions.Add(new BO.IdNotExistException("Cart", ID));
                
            };
            if (product.InStock == 0)
                exceptions.Add(new BO.InCorrectIntException("Cart Instock", product.InStock));

            cart.orderItems.Add(new BO.OrderItem
            {
                ProductID = product.UniqID,
                ProductName = product.Name,
                Price = product.Price,
                Amount = 1,
                TotalPrice = product.Price
            });
            if (exceptions.Count != 0)
                throw new AggregateException(exceptions);
            return cart;
        }
        public BO.Cart UpdateCart(BO.Cart cart, int ID, int amount)
        {

            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.
            var exceptions = new List<Exception>();


            BO.OrderItem? orderItem = cart.orderItems?.FirstOrDefault(x => x.ProductID == ID);
            if (orderItem == null)
                exceptions.Add(new BO.InCorrectIntException("OrderItem is null", 0));

            int dif = amount - orderItem.Amount;
            if (dif == 0)
                return cart;
            if (amount == 0)
            {
                cart.TotalPrice -= orderItem.TotalPrice;
                cart.orderItems?.Remove(orderItem);
            }
            else
            {
                orderItem.Amount += dif;
                orderItem.TotalPrice += dif * orderItem.Price;
                cart.TotalPrice += dif * orderItem.Price;
            }
            if (exceptions.Count != 0)
                throw new AggregateException(exceptions);
            return cart;
        }
        public void ExecuteOrder(BO.Cart cart)
        {

            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.
            var exceptions = new List<Exception>();

            if (cart.CustomerName == null)
                exceptions.Add(new BO.InCorrectStringException("Customer Name in Cart", "null"));
            if (cart.CustomerAdress == null)
                exceptions.Add(new BO.InCorrectStringException("Customer Adress in Cart", "null"));
            if (!(cart.CustomerEmail.Contains("@") && cart.CustomerEmail.Contains(".")))
                exceptions.Add(new BO.InCorrectStringException("Customer Email in Cart", "null"));
            if (cart.TotalPrice <= 0)
                exceptions.Add(new BO.InCorrectDoubleException("Total Price in Cart", cart.TotalPrice));
            foreach (BO.OrderItem orderItem in cart.orderItems)
            {
                try { dalList.Product.Get(orderItem.ProductID); }
                catch (DO.IdNotExist)
                {
                    exceptions.Add(new BO.IdNotExistException("Product", orderItem.ProductID));
                } 
                
                if (dalList.Product.Get(orderItem.ProductID).InStock < orderItem.Amount)
                    exceptions.Add(new BO.InCorrectIntException("Product InStock is less then", orderItem.Amount));

                if (orderItem.Amount <= 0)
                    exceptions.Add(new BO.InCorrectIntException("Product Amount", orderItem.Amount));
            }
            BO.Order order = new BO.Order
            {
                UniqID = 0,
                CustomerName = cart.CustomerName,
                CustomerAdress = cart.CustomerAdress,
                CustomerEmail = cart.CustomerEmail,
                orderItems = cart.orderItems,
                TotalPrice = cart.TotalPrice,
                StatusOfOrder = StatusOfOrder.Orderred,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.MinValue,
                DeliveryrDate = DateTime.MinValue,
            };
            order.UniqID = dalList.Order.Add(new DO.Order
            {
                UniqID = 0,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryrDate = order.DeliveryrDate,
            });
            foreach (BO.OrderItem orderItem1 in order.orderItems)
            {

                DO.Product product = dalList.Product.Get(orderItem1.ProductID);
                product.InStock -= orderItem1.Amount;
                dalList.Product.Update(product);

                dalList.OrderItem.Add(new DO.OrderItem
                {
                    UniqID = 0,
                    ProductID = orderItem1.ProductID,
                    OrderID = order.UniqID,
                    Amount = orderItem1.Amount,
                    Price = orderItem1.Price,
                });

            }

            if (exceptions.Count != 0)
                throw new AggregateException(exceptions);
        }
    }
}