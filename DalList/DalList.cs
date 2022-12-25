using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed public class DalList : IDal //
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
