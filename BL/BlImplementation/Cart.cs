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
    //internal class Cart
    //{
    //    IDal dalList=new DalList();
    //    BO.Cart AddItem(BO.Cart cart, int ID)
    //    {
    //        BO.OrderItem? orderItem = cart.orderItems.Find(X => X.ProductID == ID);
    //        if (orderItem != null)
    //        {
    //            cart.orderItems.Find(X => X.ProductID == ID).Amount++;
    //            cart.TotalPrice += orderItem.Price;
    //            return cart;
    //        }
    //        else
    //        {
    //            try 
    //            {
    //                BO.Product ?product = dalList.Product.Get(ID);
    //            }
    //            catch { }
    //        }
    //    }
    //}
}
