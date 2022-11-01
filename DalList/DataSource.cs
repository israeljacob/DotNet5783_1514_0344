using DO;

namespace Dal;

internal static class DataSource
{
    static int ID = 1000000;
    static Random r = new Random();
    static readonly int RandomNumber = r.Next(0, 1000);
    internal static Order[] orders=new Order[100];
    internal static OrderItem[] orderItems = new OrderItem[200];
    internal static Product[] Products = new Product[50];

    internal static class Config
    {
        
        
        private static int availableOrder=0;
        internal static int AvailableID
        {
            get { return orderID++; }
        }

        private static int availableProduct=0;
        internal static int AvailableProduct
        {
            get { return orderID++; }
        }
        private static int orderID = 1000000;
        
        internal static int OrderID
        {
            get { return orderID++; }
        }


        private static int productID = 2000000;
        internal static int ProductID
        {
            get { return orderID++; }
        }
        private static int itemOrderID= 3000000;
        internal static int ItemOrderID
        {
            get { return orderID++; }
        }

    }


    private static void  OrderInitial()
    {
        for (int i = 0; i < 20; i++)
        {
            orders[i].UniqID = RandomNumber;
            orders[i].CustomerName = "a"+i;
            orders[i].CustomerEmail = "email"+i+"@gmail.com";
            orders[i].CustomerAdress =  "address"+(i + 55);
            orders[i].ShipDate = DateTime.Now;
        }
    }

}
