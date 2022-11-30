using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrder:ICrud<Order>
    {
        /// <summary>
        /// to comper two dates
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        public void CompareDates(DateTime d1, DateTime d2);
    }
}
