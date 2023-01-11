using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi;
#region IOrder
/// <summary>
/// Interface of order implementation 
/// </summary>
public interface IOrder
{
    //get order by ID
    public BO.Order OrderByID(int ID);

    //get list of order
    public IEnumerable<BO.OrderForList?> GetListOfOrders(Func<BO.OrderForList?,bool>? func =null);

    //Update Order
    public void UpdateOrder(BO.Order order);

    //Update OrderItem Amount
    public BO.Order UpdateOrderItemAmount(BO.Order order, BO.OrderItem orderItem);

    //get Order Tracking by ID
    public BO.OrderTracking OrderTrack(int ID);

    //get orderitem by id
    public BO.OrderItem GetOrderItem(int ID);

}
#endregion