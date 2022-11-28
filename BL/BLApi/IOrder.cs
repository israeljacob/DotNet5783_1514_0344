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


        public BO.Order UpdateShipDate(int ID);

        public BO.Order UpdateDeliveryDate(int ID);

        public BO.OrderItem UpdateOrderItemAmount(BO.OrderItem orderItem);

        public BO.OrderTracking OrderTrack(int ID);
        public BO.OrderItem GetOrderItem(int ID);

    }
}
