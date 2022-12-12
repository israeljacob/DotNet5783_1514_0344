using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;

namespace BlImplementation
{
    /// <summary>
    /// Ibl entites
    /// </summary>
        sealed internal class Bl : IBL
        {
            public BLApi.IProduct Product => new Product();
            public BLApi.IOrder Order => new Order();
            public BLApi.ICart Cart => new Cart();
        }
}
