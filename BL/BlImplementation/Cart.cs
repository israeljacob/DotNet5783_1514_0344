using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using BLApi;
using Dal;

namespace BlImplementation
{
    internal class Cart : ICart
    {
        IDal dalList = DalList.Instance;
        /// <summary>
        /// Add to cart by Id and the corrent cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        public BO.Cart AddToCart(BO.Cart cart, int productID)
        {

            

            foreach (var item in cart.orderItems)
            {
                ///id found
                if (item.ProductID == productID)
                {
                    try
                    {
                        ///get the product by id
                        dalList.Product.Get(productID);

                    }
                    catch (DO.IdNotExistException)
                    {

                        throw new BO.IdNotExistException("Cart ID", productID);
                    }
                    if(dalList.Product.Get(productID).InStock == 0)///לא מבין למה צריך
                        throw new BO.InCorrectIntException("Cart ID", productID);
                    ///add one to amount and update the price
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;
                    return cart;
                }
            }

            ///connect between the product to id
            DO.Product product = new DO.Product();
            try { product = dalList.Product.Get(productID); }
            catch (DO.IdNotExistException)
            {
                throw new BO.IdNotExistException("Cart", productID);
                
            };
            if (product.InStock == 0)
                throw new BO.InCorrectIntException("Cart InStock", product.InStock);
            ///if there is product in the stock add it to order item
            cart.orderItems.Add(new BO.OrderItem
            {
                
                ProductID = product.UniqID,
                ProductName = product.Name,
                Price = product.Price,
                Amount = 1,
                TotalPrice = product.Price
            });
            return cart;
        }

        /// <summary>
        /// update the cart
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="ID"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        public BO.Cart UpdateCart(BO.Cart cart, int ID, int amount)
        {


            ///find the orderItem by id
            BO.OrderItem? orderItem = cart.orderItems?.FirstOrDefault(x => x.ProductID == ID);
            if (orderItem == null)
                throw new BO.InCorrectIntException("OrderItem", ID);
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
            return cart;
        }
        /// <summary>
        /// GET the cart and creat new order
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="AggregateException"></exception>
        public void ExecuteOrder(BO.Cart cart)
        {

            if (cart.CustomerName == null)
                throw new BO.InCorrectStringException("Customer Name in Cart", "null");
            if (cart.CustomerAdress == null)
                throw new BO.InCorrectStringException("Customer Adress in Cart", "null");
            if (!(cart.CustomerEmail.Contains("@") && cart.CustomerEmail.Contains(".")))
                throw new BO.InCorrectStringException("Customer Email in Cart", "null");
            if (cart.TotalPrice <= 0)
                throw new BO.InCorrectDoubleException("Total Price in Cart", cart.TotalPrice);

            
            foreach (BO.OrderItem orderItem in cart.orderItems)
            {
                ///find the product
                try { dalList.Product.Get(orderItem.ProductID); }
                catch (DO.IdNotExistException)
                {
                    throw new BO.IdNotExistException("Product", orderItem.ProductID);
                } 
                ///if the order amount is large then in the product stock
                if (dalList.Product.Get(orderItem.ProductID).InStock < orderItem.Amount)
                    throw new BO.InCorrectIntException($"Product InStock is less then {orderItem.Amount}",orderItem.ProductID );

                if (orderItem.Amount <= 0)
                    throw new BO.InCorrectIntException("Product Amount", orderItem.Amount);
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
                StatusOfOrder = BO.StatusOfOrder.Orderred,
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

        }
    }
}