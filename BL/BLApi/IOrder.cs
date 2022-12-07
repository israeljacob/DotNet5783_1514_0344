using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IOrder
    {
        //get order by ID
        public BO.Order OrderBYID(int ID);
        //get list of order
        public IEnumerable<BO.OrderForList?> GetListOfOrders(Func<BO.OrderForList?,bool>? func =null);
        //Update Ship Date
        public BO.Order UpdateShipDate(int ID);
        //Update Delivery Date
        public BO.Order UpdateDeliveryDate(int ID);
        //Update OrderItem Amount
        public BO.OrderItem UpdateOrderItemAmount(BO.OrderItem orderItem);
        //get Order Tracking by ID
        public BO.OrderTracking OrderTrack(int ID);
        //get orderitem by id
        public BO.OrderItem GetOrderItem(int ID);

    }
}
