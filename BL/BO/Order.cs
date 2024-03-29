﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        /// <summary>
        /// order ID.
        /// </summary>
        public int UniqID { get; set; }
        /// <summary>
        /// The name of the costomer.
        /// </summary>
        public string? CustomerName { get; set; }
        /// <summary>
        /// The email address of the customer.
        /// </summary>
        public string? CustomerEmail { get; set; }
        /// <summary>
        /// The address of the customer.
        /// </summary>
        public string? CustomerAdress { get; set; }
        /// <summary>
        /// The status of the order.
        /// </summary>
        public StatusOfOrder? StatusOfOrder { get; set; }
        /// <summary>
        /// All the items of this list.
        /// </summary>
        public IEnumerable<BO.OrderItem?>? orderItems { get; set; }
        /// <summary>
        /// The date (and time) that the order has been accepted.
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// The date (and time) that the order has been sent.
        /// </summary>
        public DateTime? ShipDate { get; set; }
        /// <summary>
        /// The date (and time) that the order has been Deliverred.
        /// </summary>
        public DateTime? DeliveryrDate { get; set; }
        /// <summary>
        /// The total price of the order.
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// Converts the object to a printable form.
        /// </summary>
        /// <returns> the printable form. </returns>
        public override string ToString() => $@" 
UniqID: {UniqID}
Name: {CustomerName}, 
Customer Email=  {CustomerEmail}, 
Customer Adress: {CustomerAdress}
Status Of Order: {StatusOfOrder}
Order Items: {orderItems}
Order Date: {OrderDate}  
Ship Date: {ShipDate} 
Deliveryr Date: {DeliveryrDate}
Total Price: {TotalPrice}
";
    }
}
