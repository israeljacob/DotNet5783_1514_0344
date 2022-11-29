using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// An order for a list.
    /// </summary>
    public class OrderForList
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
        /// The status of the order.
        /// </summary>
        public StatusOfOrder? StatusOfOrder { get; set; }
        /// <summary>
        /// The amount of the items in the order.
        /// </summary>
        public int AmountOfItems { get; set; }
        /// <summary>
        /// The total pric of the order.
        /// </summary>
        public double TotalPrice { get; set; }
        // <summary>
        /// Converts the object to a printable form.
        /// </summary>
        /// <returns> the printable form. </returns>
        public override string ToString() => $@" 
        UniqID: {UniqID}
        Name: {CustomerName}, 
        Status Of Order: {StatusOfOrder}
        Amount Of Items: {AmountOfItems}
        Total Price: {TotalPrice}

";
    }
}
