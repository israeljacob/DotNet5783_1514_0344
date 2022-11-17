using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{/// <summary>
 /// An item that was orderred.
 /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// The ID of the item.
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// The ID of the order that this item was orderred by..
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// The product name.
        /// </summary>
        public string ?ProductName { get; set; }
        /// <summary>
        /// The actual price of the product in the current order.
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// The quantity of items of this product in the order.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Converts the object to a printable form.
        /// </summary>
        /// <returns> the printable form. </returns>
        public override string ToString() => $@" 
        Product ID: {ProductID}
        Order ID: {OrderID} 
        Product Name: {ProductName}
        Amount: {Amount} 
        Price: {Price} 

        ";
    }
}
