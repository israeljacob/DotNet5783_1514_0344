using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem GetByFunc(Func<DO.OrderItem?, bool> func)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem GetByID(int ID)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem ID)
    {
        throw new NotImplementedException();
    }
}
