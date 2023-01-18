
using DAL;
using DO;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Dal;
/// <summary>
/// Stores the lists of entities.
/// </summary>
internal sealed class DataSourceXml
{
    const string s_orders = @"Orders";
    const string s_products = @"Products";
    const string s_orderItems = @"OrderItems";
    const string s_config = @"data-config";


    // Orders list
    internal List<DO.Order?> orders = new List<DO.Order?>();
    // Order items list.
    internal List<DO.OrderItem?> orderItems = new List<DO.OrderItem?>();
    // Products list.
    internal List<DO.Product?> products = new List<DO.Product?>();

    private static DataSourceXml instance;
    public static DataSourceXml Instance
    {
        get
        {
            return instance;
        }
    }
    /// <summary>
    /// static constructor
    /// </summary>
    static DataSourceXml() => instance = new DataSourceXml();
    private DataSourceXml()
    {
        ProductInitialize();
        OrderInitialize();
        OrderItemInitialize();
        XMLTools.SaveListToXMLSerializer(orders, s_orders);
        XMLTools.SaveListToXMLSerializer(products, s_products);
        XMLTools.SaveListToXMLSerializer(orderItems, s_orderItems);
    }
    /// <summary>
    /// Initializes 10 new products.
    /// </summary>
    private void ProductInitialize()
    {
        var Productsnames = new string[]
         {
                 "Shirt Slender","Shirt Bent","Shirt Attire","Shirt Kin" ,"Trouser Minute",
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
        var ProductsPricesweaters = new double[]
      {
                380,305,389,250
      };

        var r = new Random();
        // Store them in the list.
        for (int i = 0; i < 10; i++)
        {
            DO.Product temp = new DO.Product();
            temp.UniqID = r.Next(300000, 349999);

            temp.Name = Productsnames[i];

            // Find the category of the product.
            if (temp.Name.Contains("Shirt"))
            {
                var PriceIndexshirt = r.Next(ProductsPriceShirt.Length);
                temp.Price = ProductsPriceShirt[PriceIndexshirt];
                temp.Category = DO.Category.Shirts;
            }
            else if (temp.Name.Contains("Trouser"))
            {
                var PriceIndexTrouser = r.Next(ProductsPriceTrouser.Length);
                temp.Price = ProductsPriceTrouser[PriceIndexTrouser];
                temp.Category = DO.Category.trousers;

            }
            else if (temp.Name.Contains("Shoe"))
            {
                var PriceIndexshoe = r.Next(ProductsPriceShoe.Length);
                temp.Price = ProductsPriceShoe[PriceIndexshoe];
                temp.Category = DO.Category.shoes;

            }
            else if (temp.Name.Contains("Caot"))
            {
                var PriceIndexcoat = r.Next(ProductsPriceCoat.Length);
                temp.Price = ProductsPriceCoat[PriceIndexcoat];
                temp.Category = DO.Category.coats;

            }
            else
            {
                temp.Category = DO.Category.sweaters;

            }


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
        TimeSpan? ts;
        // Store them in the list.
        for (int i = 0; i < 20; i++)
        {
            DO.Order temp = new DO.Order();
            temp.UniqID = DataConfig.GetOrderId;
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
                while (ts?.TotalDays < 0);
            }
            else
                temp.ShipDate = null;
            if (i <= 10)
            {
                do
                {
                    temp.DeliveryrDate = DateTime.Now.AddDays(random.Next(-1000, -1));
                    ts = temp.DeliveryrDate - temp.ShipDate;
                }
                while (ts?.TotalDays < 0);
            }
            else
                temp.DeliveryrDate = null;

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
            DO.OrderItem temp = new DO.OrderItem();
            int rand = r.Next(1, 9);
            temp.UniqID = DataConfig.GetOrderItemId;
            temp.ProductID = products[rand]?.UniqID ?? 0;
            temp.OrderID = orders[i / 2]?.UniqID ?? 0;
            temp.Amount = r.Next(1, 10);
            temp.Price = products[rand]?.Price ?? 0;
            orderItems.Add(temp);
        }
    }





}

