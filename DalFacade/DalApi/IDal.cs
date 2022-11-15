using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public class IDal
    {
        Order Order { get; }
        Product Product { get; }
        OrderItem OrderItem { get; }
    }
}
