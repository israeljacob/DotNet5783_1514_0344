using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;

namespace BlImplementation
{
    internal class Order
    {
        IDal dalList = new DalList();
        List<BO.OrderForList> ordersForList()
        {
            List<BO.OrderForList> orderForLists;
            //try
            //{ foreach (DO.Order order in dalList.Order.GetAll())
            //    {
              
            //     }
            //}
            return new List<BO.OrderForList>(); 
        }
    }
}
