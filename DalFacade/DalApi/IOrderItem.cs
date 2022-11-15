using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem:ICrud<OrderItem>
    {
        public IEnumerable<OrderItem> ReadByOrder(int ID);
        public IEnumerable<OrderItem> ReadByProduct(int ID);
    }
}
