using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalList : IDal //
    {
        static readonly Lazy<DalList> lazy = new Lazy<DalList>(() => new DalList());
        public static DalList Instance => lazy.Value;

        private DalList() { }
        public IOrder Order => new DalOrder();
        public IProduct Product => new DalProduct();
        public IOrderItem OrderItem => new DalOrderItem();
    }
}
