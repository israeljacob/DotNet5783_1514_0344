using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace BlImplementation;
/// <summary>
/// Cart implementation
/// </summary>
internal class Cart : ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    #region Add or update cart
    /// <summary>
    /// Add or update the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productItem"></param>
    /// <returns>The Updated cart</returns>
    public BO.Cart AddOrUpdateCart(BO.Cart cart, BO.ProductItem productItem)
    {
        foreach(BO.OrderItem? orderItem in cart.OrderItems!)
            if(orderItem?.ProductID== productItem.UniqID)
            {
                return UpdateCart(cart, productItem.UniqID, productItem.Amount);
            }
        cart = AddToCart(cart, productItem.UniqID);
        return UpdateCart(cart, productItem.UniqID, productItem.Amount);
    }

    #endregion

    #region Add to cart
    /// <summary>
    /// Add to cart by Id and the corrent cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns>The Updated cart</returns>
    /// <exception cref="AggregateException"></exception>
    public BO.Cart AddToCart(BO.Cart cart, int productID)
    {
        IEnumerable<BO.OrderItem?> v = cart.OrderItems!;
        
        DO.Product? product = new DO.Product();//connect between the product to id
            try
            {
            product = dal?.Product.GetByID(productID);
                if (product?.InStock == 0)
                    throw new BO.InCorrectDetailsException("Cart ID", productID);
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.CatchetDOException(ex);
            }
            catch (BO.InCorrectDetailsException ex) { throw ex; }
        try { product = dal!.Product.GetByID(productID); }
        catch (DO.DoesNotExistException ex)
        {
            throw new BO.CatchetDOException(ex);
        };
        cart.OrderItems!.Add(new BO.OrderItem //if there is product in the stock add it to order item
        {
            ProductID = product?.UniqID?? throw new Exception(),
            ProductName = product?.Name,
            Price = product?.Price ?? throw new Exception(),
            Amount = 1,
            TotalPrice = product?.Price ?? throw new Exception()
        });
        return cart;
    }
    #endregion

    #region Update cart
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
        BO.OrderItem? orderItem = cart.OrderItems?.FirstOrDefault(x => x?.ProductID == ID); //find the orderItem by id
        if (orderItem == null)
            throw new BO.InCorrectDetailsException("Order Item", ID);
        int dif = amount - orderItem.Amount; //update the amount
        if (dif == 0)
            return cart;
        if (amount == 0)
        {
            cart.TotalPrice -= orderItem.TotalPrice; //update the total price
            cart.OrderItems?.Remove(orderItem);
        }
        else
        {
            orderItem.Amount += dif; //if amount > 1, add the dit
            orderItem.TotalPrice += dif * orderItem.Price;
            cart.TotalPrice += dif * orderItem.Price;
        }
        return cart;
    }
    #endregion

    #region Execute order
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
        foreach (BO.OrderItem? orderItem in cart.OrderItems!)
        {
            try
            {
                dal?.Product.GetByID(orderItem!.ProductID);  //find the product
                if (dal?.Product.GetByID(orderItem!.ProductID).InStock < orderItem!.Amount) //if the order amount is large then in the product stock
                    throw new BO.missingItemsException( orderItem.ProductID , orderItem.Amount);
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.CatchetDOException(ex);
            } 
            catch(BO.InCorrectDetailsException ex) { throw ex; }
            if (orderItem.Amount <= 0)
                throw new BO.InCorrectDetailsException("Product Amount", orderItem.Amount);
        }
        BO.Order order = new BO.Order //build BO entitie
        {
            UniqID = 0,
            CustomerName = cart.CustomerName,
            CustomerAdress = cart.CustomerAdress,
            CustomerEmail = cart.CustomerEmail,
            orderItems = cart.OrderItems,
            TotalPrice = cart.TotalPrice,
            StatusOfOrder = BO.StatusOfOrder.Orderred,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue,
            DeliveryrDate = DateTime.MinValue,
        };
        order.UniqID = dal!.Order.Add(new DO.Order //add it to dal
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
            try
            {
                DO.Product product = dal!.Product.GetByID(orderItem1!.ProductID); //add to Bo
                product.InStock -= orderItem1.Amount;
                dal!.Product.Update(product);
            }
            catch(DO.DoesNotExistException ex) { throw new BO.CatchetDOException(ex); }
           

            dal!.OrderItem.Add(new DO.OrderItem
            {
                UniqID = 0,
                ProductID = orderItem1.ProductID,
                OrderID = order.UniqID,
                Amount = orderItem1.Amount,
                Price = orderItem1.Price,
            });
        }
    }
    #endregion
}