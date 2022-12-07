using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IProduct
    {
        // Get a list of products
        public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<BO.ProductForList?,bool>? func =null);
        // Get a product for managger
        public BO.Product ProductItemForManagger(int ID);
        // Get a product for costemor
        public BO.ProductItem ProductItemForCostemor(int ID, BO.Cart cart);
        // Add a product
        public void AddProduct(int ID, string name, double price, BO.Category category, int inStock);
        // Delete a product
        public void DeleteProduct(int ID);
        // Update a product
        public void UpdateProduct(BO.Product product);

    }
}
