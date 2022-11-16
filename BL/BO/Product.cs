using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        /// <summary>
        ///Product ID.
        /// </summary>
        public int UniqID { get; set; }
        /// <summary>
        /// The product name.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The price of the product.
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// To wich category the product belongs.
        /// </summary>
        public Category Category { get; set; }
        /// <summary>
        /// How much from this product is available.
        /// </summary>
        public int InStock { get; set; }
        /// <summary>
        /// Converts the object to a printable form.
        /// </summary>
        /// <returns> the printable form. </returns>
        public override string ToString() => $@"
UniqID: {UniqID}
{Name}
category - {Category}
Price: {Price}
Amount in stock: {InStock} 

";
    
    }
}
