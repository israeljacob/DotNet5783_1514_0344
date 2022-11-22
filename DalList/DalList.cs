﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed public class DalList : IDal 
    {
        private static IDal instance = new DalList();
        public static IDal Instance { get { return instance; } }
        private DalList() { }
        public IOrder Order => new DalOrder();
        public IProduct Product => new DalProduct();
        public IOrderItem OrderItem => new DalOrderItem();
    }
}
