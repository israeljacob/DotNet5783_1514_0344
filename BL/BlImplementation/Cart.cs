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


    #region Add to cart
    /// <summary>
    /// Add to cart by Id and the corrent cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productID"></param>
    /// <returns>The Updated cart</returns>
    /// <exception cref="AggregateException"></exception>
    public BO.Cart AddToCart(BO.Cart cart, int productID, int amount = 1)
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
        cart.OrderItems!.Add(new BO.OrderItem //if there is product in the stock add it to order item
        {
            ProductID = product?.UniqID ?? throw new BO.MissingDataException("Product ID"),
            ProductName = product?.Name,
            Price = product?.Price ?? throw new BO.MissingDataException("Product price"),
            Amount = amount,
            TotalPrice = product?.Price * amount ?? throw new BO.MissingDataException("Product price")
        });
        cart.TotalPrice += product?.Price * amount ?? throw new BO.MissingDataException("Product price");
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
    public int ExecuteOrder(BO.Cart cart)
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
                DO.Product? product = dal?.Product.GetByID(orderItem!.ProductID);  //find the product
                if (product?.InStock < orderItem!.Amount) //if the order amount is large then in the product stock
                    throw new BO.missingItemsException(orderItem.ProductID, orderItem.Amount);
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.CatchetDOException(ex);
            }
            catch (BO.InCorrectDetailsException ex) { throw ex; }
            if (orderItem.Amount <= 0)
                throw new BO.InCorrectDetailsException("Product Amount", orderItem.Amount);
        }
        int id = dal!.Order.Add(new DO.Order //add it to dal
        {
            UniqID = 0,
            CustomerAdress = cart.CustomerAdress,
            CustomerEmail = cart.CustomerEmail,
            CustomerName = cart.CustomerName,
            OrderDate = DateTime.Now,
            ShipDate = null,
            DeliveryrDate = null,
        });
        foreach (BO.OrderItem? orderItem1 in cart.OrderItems)
        {
            try
            {
                DO.Product product = dal!.Product.GetByID(orderItem1!.ProductID); //add to Bo
                product.InStock -= orderItem1.Amount;
                dal!.Product.Update(product);
                dal!.OrderItem.Add(new DO.OrderItem
                {
                    UniqID = 0,
                    ProductID = orderItem1.ProductID,
                    OrderID = id,
                    Amount = orderItem1.Amount,
                    Price = orderItem1.Price,
                });
            }
            catch (DO.DoesNotExistException ex) { throw new BO.CatchetDOException(ex); }
        }
        return id;
    }
    #endregion
}