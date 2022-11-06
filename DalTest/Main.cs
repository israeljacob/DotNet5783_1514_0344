using Dal;
using DO;

namespace DalTest;




public class mainProgram
{

    DalProduct Dalproduct=new DalProduct();    
    DalOrder Dalorder=new DalOrder();
    DalOrderItem Dalitem=new DalOrderItem();


    Product product = new();
    DalOrder order = new DalOrder();
    DalOrderItem item = new DalOrderItem();
    static void Main(string[] args)
    {
        MainOptions menuChoice;
        int idInput=0;
        string ?name = null ;
        double price;
        Category category;
        int inStock;


        do
        {
            Menu.MainMenu();
            menuChoice = (MainOptions)Menu.UserInput();


            switch (menuChoice)
            {
                case MainOptions.ProductCheck:
                    Menu.ProductCheckMenu();
                    ProductOptions AddingMenuChoice = (ProductOptions)Menu.UserInput();
                    switch (AddingMenuChoice)
                    {
                        case ProductOptions.Add:
                            idInput = Menu.UserInput();
                            name=Console.ReadLine();
                            price= Menu.UserInput();
                            category = (Category)Menu.UserInput();
                            inStock = Menu.UserInput();
                           // product(idInput, name, price, category, inStock);
                            break;
                        case ProductOptions.Read:
                            break;
                        case ProductOptions.ReadAllList:
                            break;
                        case ProductOptions.Update:
                            break;
                        case ProductOptions.Delete:
                            break;
                        default:
                            break;
                    }
                    break;

                case MainOptions.OrderCheck:
                    Menu.OrderCheckMenu();
                    OrderOptions UpdateMenuChoice = (OrderOptions)Menu.UserInput();
                    switch (UpdateMenuChoice)
                    {
                        case OrderOptions.Add:
                            break;
                        case OrderOptions.Read:
                            break;
                        case OrderOptions.ReadAllList:
                            break;
                        case OrderOptions.Update:
                            break;
                        case OrderOptions.Delete:
                            break;
                        default:
                            break;
                    }

                    break;

                case MainOptions.OrderItemCheck:
                    Menu.OrderItemCheckMenu();
                    OrderItemOptions OrderItemMenuChoice = (OrderItemOptions)Menu.UserInput();
                    switch (OrderItemMenuChoice)
                    {
                        case OrderItemOptions.Add:
                            break;
                        case OrderItemOptions.Read:
                            break;
                        case OrderItemOptions.ReadAllList:
                            break;
                        case OrderItemOptions.Update:
                            break;
                        case OrderItemOptions.Delete:
                            break;
                        default:
                            break;
                    }
                    break;
                case MainOptions.DISPLAY:
                    break;
                case MainOptions.DISPLAY_LIST:
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



