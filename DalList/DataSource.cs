using DO;

namespace Dal;
/// <summary>
/// Stores the lists of entities.
/// </summary>
internal class DataSource
{
    // Orders list
    internal List<Order> orders = new List<Order>();
    // Order items list.
    internal List<OrderItem> orderItems = new List<OrderItem>();
    // Products list.
    internal List<Product> products = new List<Product>();

    private static readonly DataSource instace = new DataSource();
    static DataSource() { }
    public static DataSource Instance { get { return instace; } }
    /// <summary>
    /// static constructor
    /// </summary>
    private DataSource()
    {
        ProductInitialize();
        OrderInitialize();
        OrderItemInitialize();
    }

    /// <summary>
    /// Gets an ID for a new entity.
    /// </summary>
    internal static class Config
    {
        static Random r = new Random();
        /// <summary>
        /// Order ID.
        /// </summary>
        internal static int orderID = r.Next(1000000, 1500000);
        internal static int OrderID
        {
            get { return orderID++; }
        }

        /// <summary>
        /// Order item ID.
        /// </summary>
        internal static int orderItemID = r.Next(2000000, 2500000);
        internal static int OrderItemID
        {
            get { return orderItemID++; }
        }

    }

    /// <summary>
    /// Initializes 10 new products.
    /// </summary>
    private void ProductInitialize()
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
        // Store them in the list.
        for (int i = 0; i < 10; i++)
        {
            Product temp = new Product();
            temp.UniqID = r.Next(300000, 350000);

            temp.Name = Productsnames[i];

            // Find the category of the product.
            if (temp.Name.Contains("Shirt"))
            {
                temp.Category = DO.Category.Shirts;
            }
            else if (temp.Name.Contains("Trouser"))
            {

                temp.Category = DO.Category.trousers;

            }
            else if (temp.Name.Contains("Shoe"))
            {
                temp.Category = DO.Category.shoes;

            }
            else if (temp.Name.Contains("Caot"))
            {
                temp.Category = DO.Category.coats;

            }
            else
            {
                temp.Category = DO.Category.sweaters;

            }
            var PriceIndexTrouser = r.Next(ProductsPriceTrouser.Length);
            temp.Price = ProductsPriceTrouser[PriceIndexTrouser];

            var stockindex = r.Next(20);
            if (i == 1)
                temp.InStock = 0;
            else
                temp.InStock = stockindex;
            
            products.Add(temp);
        }
    }

    /// <summary>
    /// Initializes 20 new orders.
    /// </summary>
    private void OrderInitialize()
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
        // Store them in the list.
        for (int i = 0; i < 20; i++)
        {
            Order temp = new Order();
            temp.UniqID = Config.OrderID;
            temp.CustomerName = names[i];
            temp.CustomerEmail = emails[i];
            temp.CustomerAdress = adresses[i];
            temp.OrderDate = DateTime.Now.AddDays(random.Next(-1000, -1));
            if (i <= 15)
            {
                do
                {
                    temp.ShipDate = DateTime.Now.AddDays(random.Next(-1000, -1));
                    ts = temp.ShipDate - temp.OrderDate;
                }
                while (ts.TotalDays < 0);
            }
            else
                temp.ShipDate = DateTime.MinValue;
            if (i <= 10)
            {
                do
                {
                    temp.DeliveryrDate = DateTime.Now.AddDays(random.Next(-1000, -1));
                    ts = temp.DeliveryrDate - temp.ShipDate;
                }
                while (ts.TotalDays < 0);
            }
            else
                temp.DeliveryrDate = DateTime.MinValue;

            orders.Add(temp);
        }
    }
    /// <summary>
    /// Initializes 40 new order items.
    /// </summary>
    private void OrderItemInitialize()
    {
        Random r = new Random();
        // Store them in the list.
        for (int i = 0; i < 40; i++)
        {
            OrderItem temp = new OrderItem();
            int rand = r.Next(1, 9);
            temp.UniqID = Config.OrderItemID;
            temp.ProductID = orders[r.Next(0, 19)].UniqID;
            temp.OrderID = products[rand].UniqID;
            temp.Amount = r.Next(1, 10);
            temp.Price = products[rand].Price * temp.Amount;

        }
    }





}

