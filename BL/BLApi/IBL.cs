using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IBL
    {
        public ICart cart { get; }
        public IProduct product { get; }
        public IOrder order { get; }
    }
}
