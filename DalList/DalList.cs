using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// we use the lazy to initial only one and protact it even if happan to be Multi-tranding
    /// so there is no way to make two instance 
    /// </summary>
    sealed public class DalList : IDal 
    {
        static readonly Lazy<DalList> lazy = new Lazy<DalList>(() => new DalList());
        public static DalList Instance => lazy.Value;

        private DalList() 
        {
            Order = new DalOrder();
            Product= new DalProduct();
            OrderItem= new DalOrderItem();
        }
        public IOrder Order { get; }
        public IProduct Product { get; }
        public IOrderItem OrderItem { get; }
    }
}
