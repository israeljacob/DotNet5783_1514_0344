using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<DO.Product?,bool>? func =null);
        public BO.Product ProductItemForManager(int ID);

        public BO.ProductItem ProductItemForCostemor(int ID, BO.Cart cart);

        public void AddProduct(int ID, string name, double price, BO.Category category, int inStock);

        public void DeleteProduct(int ID);

        public void UpdateProduct(BO.Product product);

    }
}
