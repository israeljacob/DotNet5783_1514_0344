using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;




/// <summary>
/// Enum for the main menu
/// </summary>
public enum MainOptions
{
    ProductCheck = 1,
    OrderCheck,
    CartCheck,

}

/// <summary>
/// Enum for the sub menu option 1
/// </summary>
public enum ProductOptions
{
    Add = 1,
    GetForManagger,
    GetForCostemor,
    GetAllList,
    Update,
    Delete
}

/// <summary>
/// Emun for the sub menu option 2
/// </summary>
public enum OrderOptions
{
    Get = 1,
    GetAllList,
    Update,
    UpdateShipDate,
    UpdateDeliveryDate,
    Track
}

/// <summary>
/// Enum for the sub menu option 3
/// </summary>
public enum CartOptions
{
    Add = 1,
    Update,
    ExecuteOrder
}


namespace Dal
{
    public class Menu
    {
        public Menu()
        {
            MainMenu();

        }


        /// <summary>
        /// the main menu options, it keeps running till input 0
        /// </summary>
        public static void MainMenu()
        {
            Console.WriteLine("\t\t\nMain-Menu\n");
            Console.WriteLine("1.Product Check");
            Console.WriteLine("2.Order Check");
            Console.WriteLine("3.Cart Check");
            Console.WriteLine("0.Exit");

        }
        /// <summary>
        /// sub-option from the main-menu: product check 
        /// </summary>
        public static void ProductCheckMenu()
        {
            Console.WriteLine("1.Add Product");
            Console.WriteLine("2.See Product By ID for managger");
            Console.WriteLine("3.See Product By ID for costemor");
            Console.WriteLine("4.See List of Products");
            Console.WriteLine("5.Update Product");
            Console.WriteLine("6.Delete Product");
            Console.WriteLine("0.Return to main menu");

        }

        /// <summary>
        /// sub-option from the main-menu: order check
        /// </summary>
        public static void OrderCheckMenu()
        {
            Console.WriteLine("1.See Order By ID");
            Console.WriteLine("2.See List of Orders");
            Console.WriteLine("3.Update Order");
            Console.WriteLine("4.Update ship date");
            Console.WriteLine("5.Update delivery date");
            Console.WriteLine("6.Track order");
            Console.WriteLine("0.Return to main menu");

        }

        /// <summary>
        /// sub-option from the main-menu: orderitem check
        /// </summary>
        public static void CartCheckMenu()
        {
            Console.WriteLine("1.Add order item");
            Console.WriteLine("2.Update order item");
            Console.WriteLine("3.Execute order");
            Console.WriteLine("0.Return to main menu");

        }

        /// <summary>
        /// every string input to call this function
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public static string StringInput(string output)
        {
            string? option;

            Console.WriteLine(output);
            option = Console.ReadLine();
            while (!Regex.IsMatch(option, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine("you must enter letters onlt. Try again!");
                option = Console.ReadLine();
            }
            Console.WriteLine("\n");
            return option;
        }

        /// <summary>
        /// every category input we call this function
        /// </summary>
        /// <returns></returns>
        public static Category CategoryInput()
        {
            int option;
            do Console.Write("Enter Category: 1 - Shirts, 2 - trousers, 3 - shoes, 4 - coats,5 - sweaters  \n");
            while (!int.TryParse(Console.ReadLine(), out option) && (option > 0 || option < 5));
            Console.WriteLine("\n");
            return (Category)option;
        }

        /// <summary>
        /// every int input we call this function
        /// </summary>
        /// <param name="messege"></param>
        /// <returns></returns>
        public static int IntInput(string messege)
        {
            int option;
            Console.WriteLine(messege);
            while (!int.TryParse(Console.ReadLine(), out option))
                Console.WriteLine("you must entar numbers ony. try again!");
            return option;
        }

        /// <summary>
        /// every email input we call this function that check if the there is @ or period
        /// </summary>
        /// <returns></returns>
        public static string emailInput()
        {
            string option;

            Console.WriteLine("Enter the email:");
            option = Console.ReadLine();
            while (!option.Contains("@") || !option.Contains("."))
            {
                Console.WriteLine("You must enter a correct email address. Try again!");
                option = Console.ReadLine();
            }

            return option;
        }


        /// <summary>
        /// every date input we call this function, to check if the input is incorrect or correct
        /// </summary>
        /// <returns></returns>
        public static DateTime dateinput()
        {
            DateTime date = new DateTime();
            while (!DateTime.TryParse(Console.ReadLine(), out date))
                Console.WriteLine("try again");
            Console.WriteLine("\n");
            return date;
        }
    }
}
