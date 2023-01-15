using DalApi;

namespace Dal;

sealed public class DalXml : IDal
{
    /// <summary>
    /// we use the lazy to initial only one and protact it even if happan to be Multi-tranding
    /// so there is no way to make two instance 
    /// </summary>
    static readonly Lazy<DalXml> lazy = new Lazy<DalXml>(() => new DalXml());
    public static DalXml Instance => lazy.Value;

    private DalXml()
    {
        Order = new Order();
        Product = new Product();
        OrderItem = new OrderItem();
    }


    public IOrder Order { get; }
    public IProduct Product { get; }
    public IOrderItem OrderItem { get; }

   
}