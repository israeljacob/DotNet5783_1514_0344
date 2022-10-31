using DO;

namespace Dal;

internal static class DataSource
{
    static Random r = new Random();
    static readonly int RandomNumber= r.Next(0, 1000);

    internal static Order[] orders=new Order[100];
    internal static OrderItem[] orderItems = new OrderItem[200];
    internal static Product[] Products = new Product[50];


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
