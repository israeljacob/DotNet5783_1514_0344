using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// A product for a list.
    /// </summary>
    public class ProductForList
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
        public Category? Category { get; set; }
        /// Converts the object to a printable form.
        /// </summary>
        /// <returns> the printable form. </returns>
        public override string ToString() => $@"
        UniqID: {UniqID}
        {Name}
        category - {Category}
        Price: {Price}

";
    }
}
