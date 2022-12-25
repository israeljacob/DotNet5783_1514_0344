using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi;
#region ICart
/// <summary>
/// Interface of cart implementation 
/// </summary>
public interface ICart
{
    //Add to cart
    public BO.Cart AddToCart(BO.Cart cart, int ID);

    //Update the cart
    public BO.Cart UpdateCart(BO.Cart cart, int ID, int amount);

    //Do the order that in the cart
    public void ExecuteOrder(BO.Cart cart);
}
#endregion