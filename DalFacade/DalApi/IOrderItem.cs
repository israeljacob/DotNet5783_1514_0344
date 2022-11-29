﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem:ICrud<OrderItem>
    {
        public IEnumerable<OrderItem?> GetByOrder(int ID,Func<OrderItem?,bool> func = null);
        public IEnumerable<OrderItem?> GetByProduct(int ID, Func<OrderItem?, bool> func = null);
    }
}
