using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;

namespace BlImplementation
{
        sealed public class Bl : IBL
        {
              ///declare the 3 entities and build them

            public BLApi.IProduct Product => new Product();
            public BLApi.IOrder Order => new Order();
            public BLApi.ICart Cart => new Cart();
        }
}
