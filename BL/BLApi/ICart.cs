using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface ICart
    {
        public BO.Cart AddToCart(BO.Cart cart, int ID);

        public BO.Cart UpdateCart(BO.Cart cart, int ID, int amount);

        public void ExecuteOrder(BO.Cart cart);
    }
}
