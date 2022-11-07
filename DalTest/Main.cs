using Dal;
using DO;

namespace DalTest;


public class mainProgram
{
    static void Main(string[] args)
    {
        //dal obj initial
        DalProduct Dalproduct = new DalProduct();
        DalOrder Dalorder = new DalOrder();
        DalOrderItem Dalorderitem = new DalOrderItem();

        //DO obj initial
        Product product = new Product();
        Order order = new Order();
        OrderItem orderItem = new OrderItem();

        
        MainOptions menuChoice;
        int idInput = 0;
        string name = null, email, address;
        int price, inStock;
        Category category;
         

        ///while the user will not insert 0 we do 
        do
        {
            ///Display the options for the main menu
            Menu.MainMenu();
            ///Ask from the user number 0-3
            menuChoice = (MainOptions)Menu.UserInput();

            switch (menuChoice)
            {
                ///Option-1
                case MainOptions.ProductCheck:
                    Menu.ProductCheckMenu();
                    ProductOptions AddingMenuChoice = (ProductOptions)Menu.UserInput();
                    ///All the sub-option for Option-1
                    switch (AddingMenuChoice)
                    {
                        case ProductOptions.Add:
                            product.UniqID = Menu.IDInput();
                            product.Name = Menu.NameInput();
                            product.Price = Menu.PriceInput();
                            product.Category = Menu.CategoryInput();
                            product.InStock = Menu.InstockInput();
                            Console.Write("{0} Was Added Successfully \n", Dalproduct.Add(product));
                            break;
                        case ProductOptions.Read:
                            int idinput = Menu.IDInput();
                            Console.Write(Dalproduct.Read(idinput).ToString());
                            break;
                        case ProductOptions.ReadAllList:
                            int size = Dalproduct.ReadAll().Length;
                            Product[] products1 = new Product[size];
                            products1 = Dalproduct.ReadAll();
                            foreach (var productitem in products1)
                            {
                                Console.Write(productitem.ToString());
                            }
                            break;
                        case ProductOptions.Update:
                            product.UniqID = Menu.IDInput();
                            Console.Write(Dalproduct.Read(product.UniqID));
                            product.Name = Menu.NameInput();
                            product.Price = Menu.PriceInput();
                            product.Category = Menu.CategoryInput();
                            product.InStock = Menu.InstockInput();
                            Dalproduct.Update(product);
                            Console.Write("{0} Was Update Successfully \n", product.UniqID);
                            break;
                        case ProductOptions.Delete:
                            int del = Menu.IDInput();
                            Dalproduct.Delete(del);
                            Console.Write("{0} Was Delete Successfully \n", del);
                            break;
                        default:
                            break;
                    }
                    break;
                    ///Option 2
                case MainOptions.OrderCheck:
                    Menu.OrderCheckMenu();
                    OrderOptions UpdateMenuChoice = (OrderOptions)Menu.UserInput();
                    ///All the sub-option for Option-2
                    switch (UpdateMenuChoice)
                    {
                        case OrderOptions.Add:
                            order.UniqID = Menu.IDInput();
                            order.CustomerName = Menu.NameInput();
                            order.CustomerAdress = Menu.AdrressInput();
                            order.CustomerEmail = Menu.emailInput();
                            Console.WriteLine("Enter Order Date");
                            order.OrderDate = Menu.dateinput();
                            Console.WriteLine("Enter Ship Date:");
                            order.ShipDate = Menu.dateinput();
                            Console.WriteLine("Enter Delivery Date:");
                            order.DeliveryrDate = Menu.dateinput();
                            Console.Write("Order {0} Was Added Successfully \n", Dalorder.Add(order));
                            break;
                        case OrderOptions.Read:
                            int idinput = Menu.IDInput();
                            Console.Write(Dalorder.Read(idinput).ToString());
                            break;
                        case OrderOptions.ReadAllList:
                            int size = Dalorder.ReadAll().Length;
                            Order[] order1 = new Order[size];
                            order1 = Dalorder.ReadAll();
                            foreach (var orderitem in order1)
                            {
                                Console.Write(orderitem.ToString());
                            }
                            break;
                        case OrderOptions.Update:
                            order.UniqID = Menu.IDInput();
                            Console.Write(Dalorder.Read(order.UniqID).ToString());
                            order.CustomerName = Menu.NameInput();
                            order.CustomerAdress = Menu.AdrressInput();
                            order.CustomerEmail = Menu.emailInput();
                            Console.WriteLine("Enter Order Date");
                            order.OrderDate = Menu.dateinput();
                            Console.WriteLine("Enter Ship Date:");
                            order.ShipDate = Menu.dateinput();
                            Console.WriteLine("Enter Delivery Date:");
                            order.DeliveryrDate = Menu.dateinput();
                            Dalproduct.Update(product);
                            Console.Write("\n{0} Was Update Successfully \n", product.UniqID);
                            break;
                        case OrderOptions.Delete:
                            int del = Menu.IDInput();
                            Dalorder.Delete(del);
                            Console.Write("{0} Was Delete Successfully \n", del);
                            break;
                        default:
                            break;
                    }

                    break;
                    ///Option 3
                case MainOptions.OrderItemCheck:
                    Menu.OrderItemCheckMenu();
                    OrderItemOptions OrderItemMenuChoice = (OrderItemOptions)Menu.UserInput();
                    switch (OrderItemMenuChoice)
                    {
                        case OrderItemOptions.Add:
                            orderItem.UniqID = Menu.IDInput();
                            orderItem.ProductID = Menu.IDProductInput();
                            orderItem.OrderID = Menu.IDOrderInput();
                            orderItem.Price = Menu.PriceInput();
                            orderItem.Amount = Menu.AmountInput();
                            Console.Write("{0} Was Added Successfully \n", Dalorderitem.Add(orderItem));
                            break;
                        case OrderItemOptions.Read:
                            int idinput = Menu.IDInput();
                            Console.Write(Dalorderitem.Read(idinput).ToString());
                            break;
                        case OrderItemOptions.ReadAllList:
                            int size = Dalorderitem.ReadAll().Length;
                            OrderItem[] orderitem1 = new OrderItem[size];
                            orderitem1 = Dalorderitem.ReadAll();
                            foreach (var orderitem in orderitem1)
                            {
                                Console.Write(orderitem.ToString());
                            }
                            break;
                        case OrderItemOptions.Update:
                            orderItem.UniqID = Menu.IDInput();
                            Console.Write(Dalorderitem.Read(orderItem.UniqID).ToString());
                            orderItem.ProductID = Menu.IDProductInput();
                            orderItem.OrderID = Menu.IDOrderInput();
                            orderItem.Price = Menu.PriceInput();
                            orderItem.Amount = Menu.AmountInput();
                            Console.Write("\n{0} Was Update Successfully \n", product.UniqID);
                            break;
                        case OrderItemOptions.Delete:
                            int del = Menu.IDInput();
                            Dalorderitem.Delete(del);
                            Console.Write("{0} Was Delete Successfully \n", del);
                            break;
                        default:
                            break;
                    }
                    break;
               
                   
                default:
                    break;
            }

        } while (menuChoice != 0);
    }



    public static int OnlyNumbersInput(string userT)
    {
        int tempInt;
        while (!(int.TryParse(userT, out tempInt)))
        {
            Console.Write("\t Your Selection Not Valid, Please Try Again: ");
            userT = Console.ReadLine();
        }
        return tempInt;
    }



}



