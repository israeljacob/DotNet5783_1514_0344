//using DO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;




///// <summary>
///// Enum for the main menu
///// </summary>
//public enum MainOptions
//{
//    ProductCheck = 1,
//    OrderCheck,
//    OrderItemCheck,

//}

///// <summary>
///// Enum for the sub menu option 1
///// </summary>
//public enum ProductOptions
//{
//    Add = 1,
//    Get,
//    GetAllList,
//    Update,
//    Delete
//}

///// <summary>
///// Emun for the sub menu option 2
///// </summary>
//public enum OrderOptions
//{
//    Add = 1,
//    Get,
//    GetAllList,
//    Update,
//    Delete


//}

///// <summary>
///// Enum for the sub menu option 3
///// </summary>
//public enum OrderItemOptions
//{
//    Add = 1,
//    Get,
//    GetByOrder,
//    GetByProduct,
//    GetAllList,
//    Update,
//    Delete
//}


//namespace Dal
//{
//    public class Menu
//    {
//        public Menu()
//        {
//            MainMenu();

//        }


//        /// <summary>
//        /// the main menu options, it keeps running till input 0
//        /// </summary>
//        public static void MainMenu()
//        {
//            Console.WriteLine("\t\t\nMain-Menu\n");
//            Console.WriteLine("1.Product Check");
//            Console.WriteLine("2.Order Check");
//            Console.WriteLine("3.Order Item Check");
//            Console.WriteLine("0.Exit");

//        }
//        /// <summary>
//        /// sub-option from the main-menu: product check 
//        /// </summary>
//        public static void ProductCheckMenu()
//        {
//            Console.WriteLine("1.Add Product");
//            Console.WriteLine("2.See Product By ID");
//            Console.WriteLine("3.See List of Products");
//            Console.WriteLine("4.Update Product");
//            Console.WriteLine("5.Delete Product");
//            Console.WriteLine("0.Return to main menu");

//        }

//        /// <summary>
//        /// sub-option from the main-menu: order check
//        /// </summary>
//        public static void OrderCheckMenu()
//        {
//            Console.WriteLine("1.Add Order");
//            Console.WriteLine("2.See Order By ID");
//            Console.WriteLine("3.See List of Orders");
//            Console.WriteLine("4.Update Order");
//            Console.WriteLine("5.Delete Oreder");
//            Console.WriteLine("0.Return to main menu");

//        }

//        /// <summary>
//        /// sub-option from the main-menu: orderitem check
//        /// </summary>
//        public static void OrderItemCheckMenu()
//        {
//            Console.WriteLine("1.Add order item");
//            Console.WriteLine("2.See order Item by ID");
//            Console.WriteLine("3.See list of order items by order ID");
//            Console.WriteLine("4.See list of order item by product ID");
//            Console.WriteLine("5.See list of order items");
//            Console.WriteLine("6.Update order item");
//            Console.WriteLine("7.Delete order item");
//            Console.WriteLine("0.Return to main menu");

//        }

//        /// <summary>
//        /// every string input to call this function
//        /// </summary>
//        /// <param name="output"></param>
//        /// <returns></returns>
//        public static string StringInput(string output)
//        {
//            string? option;
            
//                Console.WriteLine(output);
//                option = Console.ReadLine();
//            while (!Regex.IsMatch(option, @"^[a-zA-Z]+$"))
//            {
//                Console.WriteLine("you must enter letters onlt. Try again!");
//                option = Console.ReadLine();
//            }
//            Console.WriteLine("\n");
//            return option;
//        }
       
//        /// <summary>
//        /// every category input we call this function
//        /// </summary>
//        /// <returns></returns>
//        public static Category CategoryInput()
//        {
//            int option;
//            do Console.Write("Enter Category: 1 - Shirts, 2 - trousers, 3 - shoes, 4 - coats,5 - sweaters  \n");
//            while (!int.TryParse(Console.ReadLine(), out option) && (option > 0 || option < 5));
//            Console.WriteLine("\n");
//            return (Category)option;
//        }

//        /// <summary>
//        /// every int input we call this function
//        /// </summary>
//        /// <param name="messege"></param>
//        /// <returns></returns>
//        public static int IntInput(string messege)
//        {
//            int option;
//            Console.WriteLine(messege);
//            while (!int.TryParse(Console.ReadLine(), out option))
//                Console.WriteLine("you must entar numbers ony. try again!");
//            return option;
//        }

//        /// <summary>
//        /// every email input we call this function that check if the there is @ or period
//        /// </summary>
//        /// <returns></returns>
//        public static string emailInput()
//        {
//            string option;

//            Console.WriteLine("Enter the email:");
//            option = Console.ReadLine();
//            while (!option.Contains("@")|| !option.Contains("."))
//            {
//                Console.WriteLine("You must enter a correct email address. Try again!");
//                option = Console.ReadLine();
//            }

//            return option;
//        }


//        /// <summary>
//        /// every date input we call this function, to check if the input is incorrect or correct
//        /// </summary>
//        /// <returns></returns>
//        public static DateTime dateinput()
//        {
//            DateTime date = new DateTime();
//            while (!DateTime.TryParse(Console.ReadLine(), out date) )
//                Console.WriteLine("try again");
//            Console.WriteLine("\n");
//            return date;
//        }
//    }
//}
