using Dal;
using DalApi;

namespace BlImplementation
{
    /// <summary>
    /// Takes care on all of wh
    /// </summary>
    internal class Product : BLApi.IProduct
    {
        /// <summary>
        /// Handles everything related a product
        /// </summary>
        IDal dalList = DalList.Instance;

        /// <summary>
        /// Returns all the products.
        /// </summary>
        /// <returns>An IEnumerable of all the products.</returns>
        /// <exception cref="MissingAttributeException"></exception>
        public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<BO.Product?, bool>? func = null)
        {
            try
            {
                if (func == null)
                    return from product in dalList.Product.GetAll()
                           select new BO.ProductForList
                           {
                               UniqID = product?.UniqID ?? throw new BO.MissingDataException("Product ID"),
                               Name = product?.Name,
                               Price = product?.Price ?? throw new BO.MissingDataException("Product price"),
                               Category = (BO.Category)product?.Category!
                           };
                else
                    return from product in dalList.Product.GetAll(/*func*/)
                           select new BO.ProductForList
                           {
                               UniqID = product?.UniqID ?? throw new BO.MissingDataException("Product ID"),
                               Name = product?.Name,
                               Price = product?.Price ?? throw new BO.MissingDataException("Product price"),
                               Category = (BO.Category)product?.Category!
                           };
            }
            catch (DO.EmptyException ex){ throw new BO.EmptyException(ex);  }
        }

        /// <summary>
        /// Display more features if the mameger asking product by Id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="Exception"></exception>
        public BO.Product ProductItemForManagger(int ID)
        {
            if (ID <= 0)
                throw new BO.InCorrectDetailsException("Product ID", ID);
            try
            {
                DO.Product product = dalList.Product.Get(ID);
                return new BO.Product //return new entitie product 
                {
                    UniqID = product.UniqID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (BO.Category)product.Category!,
                    InStock = product.InStock
                };
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.IdNotExistException(ex);
            }
        }

        /// <summary>
        /// Display more features if the mameger asking product by Id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="Exception"></exception>
        public BO.Product ProductItemForManagger(Func<BO.Product?, bool> func)
        {
            try
            {
                DO.Product product = dalList.Product.Get(1/*func*/);
                return new BO.Product //return new entitie product 
                {
                    UniqID = product.UniqID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (BO.Category)product.Category!,
                    InStock = product.InStock
                };
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.IdNotExistException(ex);
            }
        }

        /// <summary>
        /// Display less features if the customer asking product by Id
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BO.ProductItem ProductItemForCostemor(int ID, BO.Cart cart)
        {
            if (ID <= 0)
                throw new BO.InCorrectDetailsException("Product ID", ID);
            try
            {
                DO.Product product = dalList.Product.Get(ID);
                return new BO.ProductItem
                {
                    UniqID = product.UniqID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (BO.Category)product.Category!,
                    InStock = true ? product.InStock !=0 : false
                };
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.IdNotExistException(ex);

            }
        }

        /// <summary>
        /// Display less features if the customer asking product by Id
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public BO.ProductItem ProductItemForCostemor(Func<BO.Product?, bool> func, BO.Cart cart)
        {
            try
            {
                DO.Product product = dalList.Product.Get(1/*func*/);
                return new BO.ProductItem
                {
                    UniqID = product.UniqID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (BO.Category)product.Category!,
                    InStock = true ? product.InStock != 0 : false
                };
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.IdNotExistException(ex);

            }
        }

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="category"></param>
        /// <param name="inStock"></param>
        /// <exception cref="AggregateException"></exception>
        public void AddProduct(int ID, string name, double price, BO.Category category, int inStock)
        {
            Product product = new Product();

            if (ID <= 0)
                throw new BO.InCorrectDetailsException("product ID", ID);
            if (name == null)
                throw new BO.MissingDataException("product name");
            if (price <= 0)
                throw new BO.InCorrectDetailsException("product price", price);
            if (inStock < 0)
                throw new BO.InCorrectDetailsException("product in stock", inStock);
            try
            {
                int returnedID = dalList.Product.Add(new DO.Product
                {
                    UniqID = ID,
                    Name = name,
                    Price = price,
                    Category = (DO.Category)category,
                    InStock = inStock
                });
            }
            catch (DO.IdAlreadyExistException ex)
            {
                throw new BO.IdAlreadyExistException(ex);
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="AggregateException"></exception>
        public void DeleteProduct(int ID)
        {
            Func<DO.OrderItem?, bool> func = orderItem => orderItem?.ProductID == ID;
            if (dalList.OrderItem.GetAll(func) != null)
                throw new BO.ItemExistsInOrderException("Product");
            try
            {
                dalList.Product.Delete(ID);
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.IdNotExistException(ex);
            }
        }
        /// <summary>
        /// Uptate Product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="AggregateException"></exception>
        public void UpdateProduct(BO.Product product)
        {


            if (product.UniqID <= 0)
                throw new BO.InCorrectDetailsException("Product ID", product.UniqID);
            if (product.Name == null)
                throw new BO.MissingDataException("Product name");
            if (product.Price <= 0)
                throw new BO.InCorrectDetailsException("Product price",product.Price);
            if (product.Category == null)
                throw new BO.MissingDataException("Product category");
            if (product.InStock < 0)
                throw new BO.InCorrectDetailsException("Product in stock", product.InStock);
            try
            {
                dalList.Product.Update(new DO.Product
                {
                    UniqID = product.UniqID,
                    Name = product.Name,
                    Price = product.Price,
                    Category = (DO.Category)product.Category,
                    InStock = product.InStock
                });
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.IdNotExistException(ex);
            }

        }

       
    }
}