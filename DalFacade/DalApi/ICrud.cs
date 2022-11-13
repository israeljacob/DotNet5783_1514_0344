using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>:IEnumerable<T>
    {
        public int Add(T entity);
        public void Delete(int ID);
        public void Update(int ID);
        public T Read(int ID);
        public T[] ReadAll(); 
    }
}
