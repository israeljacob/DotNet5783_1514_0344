using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A shopping cart.
    /// </summary>
    public class Cart
    {
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
        /// All the items of this list.
        /// </summary>
        public List<OrderItem>? orderItems { get; set; }
        /// <summary>
        /// The total price of the order.
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// Converts the object to a printable form.
        /// </summary>
        /// <returns> the printable form. </returns>
        public override string ToString() => $@" 
        Name: {CustomerName}, 
        Customer Email=  {CustomerEmail}, 
        Customer Adress: {CustomerAdress} 
        Order Items: {orderItems}
        Total Price: {TotalPrice}
";
    }
}
