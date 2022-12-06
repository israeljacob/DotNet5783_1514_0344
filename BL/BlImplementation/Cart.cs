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
            foreach (BO.OrderItem? item in cart.orderItems!)
            {
                if (item?.ProductID == productID)//id found
                {
                    try
                    {
                        if (dalList.Product.Get(productID).InStock == 0)
                            throw new BO.InCorrectDetailsException("Cart ID", productID);
                    }
                    catch (DO.DoesNotExistException ex)
                    {
                        throw new BO.IdNotExistException(ex);
                    }
                    catch (BO.InCorrectDetailsException ex) { throw ex; }
                    
                    
                    item.Amount++;//add one to amount and update the price
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;
                    return cart;
                }
            }

            
            DO.Product product = new DO.Product();//connect between the product to id
            try { product = dalList.Product.Get(productID); }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.IdNotExistException(ex);
                
            };
            if (product.InStock == 0)
                throw new BO.InCorrectDetailsException("Cart InStock", product.InStock);
            cart.orderItems.Add(new BO.OrderItem //if there is product in the stock add it to order item
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
            BO.OrderItem? orderItem = cart.orderItems?.FirstOrDefault(x => x?.ProductID == ID); //find the orderItem by id
            if (orderItem == null)
                throw new BO.InCorrectDetailsException("Order Item", ID);
            int dif = amount - orderItem.Amount; //update the amount
            if (dif == 0)
                return cart;
            if (amount == 0)
            {
                cart.TotalPrice -= orderItem.TotalPrice; //update the total price
                cart.orderItems?.Remove(orderItem);
            }
            else
            {
                orderItem.Amount += dif; //if amount > 1, add the dit
                orderItem.TotalPrice += dif * orderItem.Price;
                cart.TotalPrice += dif * orderItem.Price;
            }
            return cart;
        }
        /// <summary>
        /// Get the cart and creat new order
        /// </summary>
        /// <param name="cart"></param>
        /// <exception cref="AggregateException"></exception>
        public void ExecuteOrder(BO.Cart cart)
        {

            if (cart.CustomerName == null)
                throw new BO.MissingDataException("Customer Name");
            if (cart.CustomerAdress == null)
                throw new BO.MissingDataException("Customer Adress");
            if (!(cart.CustomerEmail!.Contains("@") && cart.CustomerEmail.Contains(".")))
                throw new BO.InCorrectDetailsException("Customer Email", cart.CustomerEmail);
            if (cart.TotalPrice <= 0)
                throw new BO.InCorrectDetailsException("Total Price in Cart", (int)cart.TotalPrice);

            
            foreach (BO.OrderItem? orderItem in cart.orderItems!)
            {
                try { dalList.Product.Get(orderItem!.ProductID); } //find the product
                catch (DO.DoesNotExistException ex)
                {
                    throw new BO.IdNotExistException(ex);
                } 
                if (dalList.Product.Get(orderItem.ProductID).InStock < orderItem.Amount) //if the order amount is large then in the product stock
                    throw new BO.InCorrectDetailsException($"Product In Stock {orderItem.ProductID} is less then",orderItem.ProductID );

                if (orderItem.Amount <= 0)
                    throw new BO.InCorrectDetailsException("Product Amount", orderItem.Amount);
            }
            BO.Order order = new BO.Order //build BO entitie
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
            order.UniqID = dalList.Order.Add(new DO.Order //add it to dal
            {
                UniqID = 0,
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                OrderDate = order.OrderDate,
                ShipDate = order.ShipDate,
                DeliveryrDate = order.DeliveryrDate,
            });
            foreach (BO.OrderItem? orderItem1 in order.orderItems)
            {
                DO.Product product = dalList.Product.Get(orderItem1!.ProductID); //add to Bo
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