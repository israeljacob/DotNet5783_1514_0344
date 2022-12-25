using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi;
#region IBL
/// <summary>
/// Interface for the implementation of BL
/// </summary>
public interface IBL
{
     //declare the 3 entities

    public ICart Cart { get; }
    public IProduct Product { get; }
    public IOrder Order { get; }
}
#endregion