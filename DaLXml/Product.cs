using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;

internal class Product : IProduct
{
    public int Add(DO.Product entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int ID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public DO.Product GetByFunc(Func<DO.Product?, bool> func)
    {
        throw new NotImplementedException();
    }

    public DO.Product GetByID(int ID)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product ID)
    {
        throw new NotImplementedException();
    }
}
