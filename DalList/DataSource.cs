using DO;
using Order = DO.Order;

namespace Dal;

internal static class DataSource
{
    internal static Order[] orders = new Order[100];
    internal static OrderItem[] orderItems = new OrderItem[200];
    internal static Product[] products = new Product[50];

    private static int availableProduct=0;
    internal static int AvailableProduct
    {
        get { return availableProduct++; }
    }

    private static int availableOrder = 0;
    internal static int AvailableOrder
    {
        get { return availableOrder++; }
    }
    private static int availableOrderItem = 0;
    internal static int AvailableOrderItem
    {
        get { return availableOrderItem++; }
    }


    /// <summary>
    /// static constructor
    /// </summary>
    static DataSource()
    {
        ProductInitialize();
        OrderInitialize();
        OrderItemInitialize();
    }

    internal static class Config
    {
        static Random r = new Random();
        internal static int orderID = r.Next(1000000, 1500000);

        internal static int OrderID
        { 
            get { return orderID++; }
        }


        internal static int productID = r.Next(2000000, 2500000) ;
        internal static int ProductID
        {

            get {return productID++; }
        }
        internal static int orderItemID= r.Next(3000000, 3500000);
        internal static int OrderItemID
        {
            get {return orderItemID++; }
        }

    }


    private static void ProductInitialize()
    {
        var Productsnames = new string[]
         {
                "Shirt Slender","Shirt Bent","Shirt Attire","Shirt Kin","Trouser Minute",
             "Trouser Agro","Trouser Places","Trouser Lustrous","Trouser Surge","Shoe Threads",
             "Shoe Array","Shoe Hype","Shoe Gene","Coat Fix","Coat Supreme","Coat Support","Coat Alpha",
             "Sweater Above","Sweater Manage","Sweater Gusto","Sweater Scout"
         };

        var ProductsPriceShirt = new double[]
        {
                99,279,180,200,210,240,100,159,149
        };
        var ProductsPriceTrouser = new double[]
       {
                279,349,300,480,789,599,349
       };
        var ProductsPriceShoe = new double[]
       {
                279,450,800,499,600,350,
       };
        var ProductsPriceCoat = new double[]
       {
                380,299,500,750
       };


        var r = new Random();


        for (int i = 0; i < 10; i++)
        {

            products[i].UniqID = Config.ProductID;
            
            products[i].Name = Productsnames[i];


            if (products[i].Name.Contains("Shirt"))//else
            {
               var PriceIndexShirt = r.Next(ProductsPriceShirt.Length);
                products[i].Price = ProductsPriceShirt[PriceIndexShirt];
                products[i].Category = DO.Category.Shirts;

            }
            else if (products[i].Name.Contains("Trouser"))
            {
               var PriceIndexTrouser = r.Next(ProductsPriceTrouser.Length);
                products[i].Price = ProductsPriceTrouser[PriceIndexTrouser];
                products[i].Category = DO.Category.trousers;

            }
            else if (products[i].Name.Contains("Shoe"))
            {
                var PriceIndexShoe = r.Next(ProductsPriceShoe.Length);
                products[i].Price = ProductsPriceShoe[PriceIndexShoe];
                products[i].Category = DO.Category.shoes;

            }
            else if (products[i].Name.Contains("Caot"))
            {
                 var PriceIndexCoat = r.Next(ProductsPriceCoat.Length);
                products[i].Price = ProductsPriceCoat[PriceIndexCoat];
                products[i].Category = DO.Category.coats;

            }
            else if (products[i].Name.Contains("Sweater"))
            {
                var PriceIndexShirt = r.Next(ProductsPriceShirt.Length);
                products[i].Price = ProductsPriceShirt[PriceIndexShirt];
                products[i].Category = DO.Category.sweaters;

            }


            var stockindex = r.Next(20);
            if (i ==1)
                products[i].InStock = 0;
            else
                products[i].InStock =stockindex ;


        }
    }

    private static void  OrderInitialize()
    {
        var names = new string[]
         {
             "Willian Warner","Jenny Fritz","Tamara Salazar","Letitia Boyd",
             "Lavern Bolton","Stanley Giles","Michal Lang","Maurice Clements",
             "Jamie Flowers","Carey Navarro","Patricia Alexander","Bradley Moran",
             "Donna Luna","Abraham Rangel","Tanya Griffin","Leland Ballard",
             "Agnes Marquez","Penelope Parsons","Lottie Hancock","Lorraine Trevino"

         };
       

        var emails = new string[]
        {
            "gtaylor@aol.com","gordonjcp@yahoo.com","magusnet@icloud.com","ivoibs@yahoo.com",
            "kodeman@hotmail.com","kobayasi@yahoo.com","aibrahim@optonline.net","amichalo@att.net",
            "lcheng@att.net","ryanvm@aol.com","dmiller@comcast.net","dhrakar@gmail.com",
            "vganesh@optonline.net","agolomsh@outlook.com","jginspace@outlook.com","bruck@att.net",
            "parrt@me.com","hstiles@live.com","fallorn@live.com","jimxugle@icloud.com"
        };

        var adresses = new string[]
        {
            "62 Grove Road","SW65 3QL","86 Manchester Road","EC32 0GB","297 Grange Road",
            "WC63 5MV","99 Station Road","E68 5XO","87 Queensway","W37 9GH","14 Richmond Road",
            "WC87 2KG","4 Station Road","N08 4JP","85 New Road","NW61 0IL","90 New Road","SE64 7XG",
            "9469 George Street","SE45 7QS","81 Park Avenue","W28 1FN","42 Church Road","W37 2BD",
            "982 Manor Road","E30 2KT","38 Mill Road","E46 3QJ","36 The Avenue","London","E32 6OY",
            "604 The Green","SW95 2RO","25 The Drive","SE90 5GV","63 Park Avenue","London","W49 8YL",
            "59 North Street","SE59 4KN","9669 Chester Road","WC71 5GK"        };


        Random random = new Random();
        TimeSpan ts;
        for (int i = 0; i < 20; i++)
        {
            orders[i].UniqID = Config.OrderID;
            orders[i].CustomerName = names[i];
            orders[i].CustomerEmail = emails[i];
            orders[i].CustomerAdress = adresses[i];
            orders[i].OrderDate = DateTime.Now.AddDays(random.Next(-1000,-1));
            do
            {
                orders[i].ShipDate = DateTime.Now.AddDays(random.Next(-1000, -1));
                ts = orders[i].ShipDate - orders[i].OrderDate;
            }
            while (ts.TotalDays < 0);
            do
            {
                orders[i].DeliveryrDate = DateTime.Now.AddDays(random.Next(-1000, -1));
                ts = orders[i].DeliveryrDate - orders[i].ShipDate;
            }
            while (ts.TotalDays < 0);

        }
    }

    private static void OrderItemInitialize()
    {
         Random r = new Random();
         
        for (int i = 0; i < 40; i++)
        {
            int rand = r.Next(1, 9);
            orderItems[i].UniqID=Config.OrderItemID;
            orderItems[i].ProductID = orders[r.Next(1,19)].UniqID;
            orderItems[i].OrderID = products[rand].UniqID;
            orderItems[i].Amount= r.Next(1,10);
            orderItems[i].Price = products[rand].Price * orderItems[i].Amount;
            
        }
    }





}

