using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;

namespace BlImplementation;
#region BL
/// <summary>
/// Ibl entites
/// </summary>
sealed internal class Bl : IBL
{
    public Bl()
    {
        Product = new Product();
        Order = new Order();
        Cart = new Cart();
    }
    public IProduct Product { get; }
    public IOrder Order { get; }
    public  ICart Cart {get; }
}
#endregion