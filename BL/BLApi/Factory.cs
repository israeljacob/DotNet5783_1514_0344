using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{

    public static class Factory
    {
        static IBL bl = new Bl();
        public static IBL Get
        {
            get {return bl; }
        }
    }
}
