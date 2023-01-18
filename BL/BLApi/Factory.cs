using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi;
#region BL Factory
/// <summary>
/// The class that PL calls in order to use the BL layer
/// </summary>
public class Factory
{
    public static IBL Get() => new Bl();
}

#endregion