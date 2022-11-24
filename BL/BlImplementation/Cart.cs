using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal;
using BO;

namespace BlImplementation
{
    internal class Cart : BLApi.ICart
    {
        IDal dalList = DalList.Instance;
        BO.Cart AddToCart(BO.Cart cart, int ID)
        {
            foreach (var item in cart.orderItems)
            {
                if (item.ProductID == ID)
                {
                    if (dalList.Product.Get(ID).InStock == 0)
                        throw new Exception();////////// לתקן
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;
                    return cart;
                }
            }
            DO.Product product = new DO.Product();// לתקן
            try { product = dalList.Product.Get(ID); }
            catch (Exception e) { throw new Exception(e.Message); };//// לתקן 
            if (product.InStock == 0)
                throw new Exception();//לתקן
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
        BO.Cart UpdateCart(BO.Cart cart, int ID, int amount)
        {
            BO.OrderItem? orderItem = cart.orderItems?.FirstOrDefault(x => x.ProductID == ID);
            if (orderItem == null)
                throw new Exception();  // לתקןןןן
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
            return cart;
        }
        void ExecuteOrder(BO.Cart cart)
        {
            if (cart.CustomerName == null)
                throw new BO.MissingAttributeException("Costemor name is null");
            if (cart.CustomerAdress == null)
                throw new BO.MissingAttributeException("Costemor adress is null");
            if (!(cart.CustomerEmail.Contains("@") && cart.CustomerEmail.Contains(".")))
                throw new BO.MissingAttributeException("Costemor mail is in correct");
            if (cart.TotalPrice <= 0)
                throw new MissingAttributeException("The total price is not positive");
            foreach (BO.OrderItem orderItem in cart.orderItems)
            {
                try { dalList.Product.Get(orderItem.ProductID); }
                catch (Exception e) { throw new Exception(e.Message); } /// לתקןןן
                if (dalList.Product.Get(orderItem.ProductID).InStock < orderItem.Amount)
                    throw new BO.DoesNotExistsException("One or more of the items");
                if (orderItem.Amount <= 0)
                    throw new BO.InCorrectDetailsException();
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
        }
    }
}