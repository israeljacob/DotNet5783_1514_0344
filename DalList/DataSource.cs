using DO;

namespace Dal;

internal static class DataSource
{
    //static int ID = 1000000;
    //static Random r = new Random();
    //static readonly int RandomNumber = r.Next(0, 1000);
    internal static Order[] orders = new Order[100];
    internal static OrderItem[] orderItems = new OrderItem[200];
    internal static Product[] products = new Product[50];
    static  DataSource()
    {
        S_Initialize();
    }
    static void S_Initialize()
    {
        orders[0].UniqID = 0;
    }

    internal static class Config
    {
        
        
        internal static int availableOrder=0;
        internal static int AvailableOrder
        {
            get { return availableOrder++; }

        }
        internal static int availableOrderItem = 0;
        internal static int AvailableOrderItem
        {
            get { return availableOrderItem++; }
        }
        internal static int availableProduct=0;
        internal static int AvailableProduct
        {
            get { return availableProduct++; }
        }
        internal static int orderID = 1000000;
        
        internal static int OrderID
        {
            get { return orderID++; }
        }


        internal static int productID = 2000000;
        internal static int ProductID
        {
            get { return productID++; }
        }
        internal static int orderItemID= 3000000;
        internal static int OrderItemID
        {
            get { return orderItemID++; }
        }

    }
    
    //private static void  OrderInitial()
    //{
    //      for (int i = 0; i < 20; i++)
    //    {

    //        orders[i].UniqID = RandomNumber;
    //        orders[i].CustomerName = "a"+i;
    //        orders[i].CustomerEmail = "email"+i+"@gmail.com";
    //        orders[i].CustomerAdress =  "address"+(i + 55);
    //        orders[i].ShipDate = DateTime.Now;
    //    }
    //}

}

