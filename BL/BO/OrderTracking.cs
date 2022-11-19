using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    /// An order for trak.
    /// </summary>
    public class OrderTracking
    {
        /// <summary>
        /// order ID.
        /// </summary>
        public int UniqID { get; set; }
        /// <summary>
        /// The status of the order.
        /// </summary>
        public StatusOfOrder StatusOfOrder { get; set; }
        /// <summary>
        /// A list of tuples that contains the date of the update and a description of the proress.
        /// </summary>
        public List<Tuple<DateTime,string>>? ProgressOfOrder { get; set; }
        public override string ToString() => $@" 
        UniqID: {UniqID}
        Status Of Order: {StatusOfOrder}
        Progress Of Order: {ProgressOfOrder}

";
    }
}
