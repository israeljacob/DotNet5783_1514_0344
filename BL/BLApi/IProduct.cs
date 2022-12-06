using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IProduct
    {
        public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<BO.Product?,bool>? func =null);
        public BO.Product ProductItemForManagger(int ID);
        public BO.Product ProductItemForManagger(Func<BO.Product?, bool> func);

        public BO.ProductItem ProductItemForCostemor(int ID, BO.Cart cart);
        public BO.ProductItem ProductItemForCostemor(Func<BO.Product?, bool> func, BO.Cart cart);

        public void AddProduct(int ID, string name, double price, BO.Category category, int inStock);

        public void DeleteProduct(int ID);

        public void UpdateProduct(BO.Product product);

    }
}
