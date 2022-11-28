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
        ///Add to cart by Id and the corrent cart
        public BO.Cart AddToCart(BO.Cart cart, int ID)
        {

            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.
            var exceptions = new List<Exception>();
            

            foreach (var item in cart.orderItems)
            {
                /// id found
                if (item.ProductID == ID)
                {
                    try
                    {
                        ///get the product by id
                        dalList.Product.Get(ID);

                    }
                    catch (DO.IdNotExistException)
                    {

                        exceptions.Add(new BO.IdNotExistException("Cart ID", ID));
                    }
                    if(dalList.Product.Get(ID).InStock == 0)///לא מבין למה צריך
                        exceptions.Add(new BO.InCorrectIntException("Cart ID", ID));
                    ///add one to amount and update the price
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;
                    return cart;
                }
            }

            ///connect between the product to id
            DO.Product product = new DO.Product();
            try { product = dalList.Product.Get(ID); }
            catch (DO.IdNotExistException)
            {
                exceptions.Add(new BO.IdNotExistException("Cart", ID));
                
            };
            if (product.InStock == 0)
                exceptions.Add(new BO.InCorrectIntException("Cart InStock", product.InStock));
            ///if there is product in the stock add it to order item
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

        ///update the cart 
        public BO.Cart UpdateCart(BO.Cart cart, int ID, int amount)
        {

            ///All the exception that comes from DO we catch it, than insert the appropriate exception to list,
            ///in the end if the list is not empty throw AggregateException: kind of build in function
            ///that hold and represents one or more errors.
            var exceptions = new List<Exception>();

            ///find the orderItem by id
            BO.OrderItem? orderItem = cart.orderItems?.FirstOrDefault(x => x.ProductID == ID);
            if (orderItem == null)
                exceptions.Add(new BO.InCorrectIntException("OrderItem is null", 0));
            ///update the amount
            int dif = amount - orderItem.Amount;
            if (dif == 0)
                return cart;
            if (amount == 0)
            {
                ///update the total price
                cart.TotalPrice -= orderItem.TotalPrice;
                cart.orderItems?.Remove(orderItem);
            }
            else
            {
                ///if amount > 1, add the dit
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
            ///all the possibilities to go something worg 
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
                ///find the product
                try { dalList.Product.Get(orderItem.ProductID); }
                catch (DO.IdNotExistException)
                {
                    exceptions.Add(new BO.IdNotExistException("Product", orderItem.ProductID));
                } 
                ///if the order amount is large then in the product stock
                if (dalList.Product.Get(orderItem.ProductID).InStock < orderItem.Amount)
                    exceptions.Add(new BO.InCorrectIntException("Product InStock is less then", orderItem.Amount));

                if (orderItem.Amount <= 0)
                    exceptions.Add(new BO.InCorrectIntException("Product Amount", orderItem.Amount));
            }
            //build BO entitie
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
            //add it to dal
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
                ///add to Bo
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