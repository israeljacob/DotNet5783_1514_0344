using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IOrder
    {
        public BO.Order OrderBYID(int ID);
        public IEnumerable<BO.OrderForList> GetListOfOrders();

        public BO.StatusOfOrder statusOfOrder(DO.Order order);

        public int amoutOfItems(DO.Order order);

        public IEnumerable<BO.OrderItem> orderItems(int ID);

        public BO.Order UpdateShipDate(int ID);

        public BO.Order UpdateDeliveryDate(int ID);

    }
}
