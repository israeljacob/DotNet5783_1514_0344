
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
        DalApi.IDal dal = DalApi.Factory.Get()!;

        #region Get list of products
        /// <summary>
        /// Returns all the products.
        /// </summary>
        /// <returns>An IEnumerable of all the products.</returns>
        /// <exception cref="MissingAttributeException"></exception>
        public IEnumerable<BO.ProductForList?> GetListOfProducts(Func<BO.ProductForList?, bool>? func = null)
        {
            try
            {
                if (func == null)
                    return from product in dal.Product.GetAll()
                           select new BO.ProductForList
                           {
                               UniqID = product?.UniqID ?? throw new BO.MissingDataException("Product ID"),
                               Name = product?.Name,
                               Price = product?.Price ?? throw new BO.MissingDataException("Product price"),
                               Category = (BO.Category)product?.Category!
                           };
                else
                    return from product in GetListOfProducts()
                           where func(product)
                           orderby product.UniqID
                           select product;
            }
            catch (DO.EmptyException ex){ throw new BO.CatchetDOException(ex);  }
        }
        #endregion

        #region Product item for managger
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
                DO.Product product = dal.Product.GetByID(ID);
                BO.Product returnedProduct = (BO.Product)product.CopyPropertiesToNew(typeof(BO.Product));
                return returnedProduct; //return new entitie product 
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.CatchetDOException(ex);
            }
        }
        #endregion

        #region Product item for costemor
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
                DO.Product product = dal.Product.GetByID(ID);
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
                throw new BO.CatchetDOException(ex);

            }
        }
        #endregion

        #region Add product
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
            try
            {
                int returnedID = dal.Product.Add(new DO.Product
                {
                    UniqID = (ID >=0)? ID : throw new BO.InCorrectDetailsException("product ID", ID),
                    Name = name?? throw new BO.MissingDataException("product name"),
                    Price = (price >= 0) ? price : throw new BO.InCorrectDetailsException("product price", price),
                    Category = (DO.Category)category,
                    InStock = (inStock > 0)? inStock : throw new BO.InCorrectDetailsException("product in stock", inStock)
                });
            }
            catch (DO.IdAlreadyExistException ex)
            {
                throw new BO.CatchetDOException(ex);
            }
        }
        #endregion

        #region Delete product
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="ID"></param>
        /// <exception cref="AggregateException"></exception>
        public void DeleteProduct(int ID)
        {
            Func<DO.OrderItem?, bool> func = orderItem => orderItem?.ProductID == ID;
            try
            {
            if (dal.OrderItem.GetAll(func) != null)
                throw new BO.ItemExistsInOrderException("Product");
            }
            catch (DO.EmptyException ex) { throw new BO.CatchetDOException(ex); }
            try
            {
                dal.Product.Delete(ID);
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.CatchetDOException(ex);
            }
        }
        #endregion

        #region Update product
        /// <summary>
        /// Uptate Product
        /// </summary>
        /// <param name="product"></param>
        /// <exception cref="AggregateException"></exception>
        public void UpdateProduct(BO.Product product)
        {
            try
            {
                dal.Product.Update(new DO.Product
                {
                    UniqID = (product.UniqID >= 0)? product.UniqID : throw new BO.InCorrectDetailsException("Product ID", product.UniqID),
                    Name = product.Name ?? throw new BO.MissingDataException("Product name"),
                    Price = (product.Price >0)? product.Price : throw new BO.InCorrectDetailsException("Product price", product.Price),
                    Category = product.Category != null? (DO.Category)product.Category : throw new BO.MissingDataException("Product category"),
                    InStock = (product.InStock >= 0)? product.InStock : throw new BO.InCorrectDetailsException("Product in stock", product.InStock)
            });
            }
            catch (DO.DoesNotExistException ex)
            {
                throw new BO.CatchetDOException(ex);
            }
        }
        #endregion
    }
}