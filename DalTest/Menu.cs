using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;





public enum MainOptions
{
    ProductCheck = 1,
    OrderCheck,
    OrderItemCheck,
   
}
public enum ProductOptions
{
    Add = 1,
    Read,
    ReadAllList,
    Update,
    Delete
}
public enum OrderOptions
{
    Add = 1,
    Read,
    ReadAllList,
    Update,
    Delete


}

public enum OrderItemOptions
{
    Add = 1,
    Read,
    ReadByOrder,
    ReadByProduct,
    ReadAllList,
    Update,
    Delete
}


namespace Dal
{
    public class Menu
    {
        public Menu()
        {
            MainMenu();

        }

        public static void MainMenu()
        {
            Console.WriteLine("\t\t\nMain-Menu\n");
            Console.WriteLine("1.Product Check");
            Console.WriteLine("2.Order Check");
            Console.WriteLine("3.Order Item Check");
            Console.WriteLine("0.Exit");

        }
        public static void ProductCheckMenu()
        {
            Console.WriteLine("1.Add Product");
            Console.WriteLine("2.See Product By ID");
            Console.WriteLine("3.See List of Products");
            Console.WriteLine("4.Update Product");
            Console.WriteLine("5.Delete Product");
            Console.WriteLine("0.Return to main menu");

        }
        public static void OrderCheckMenu()
        {
            Console.WriteLine("1.Add Order");
            Console.WriteLine("2.See Order By ID");
            Console.WriteLine("3.See List of Orders");
            Console.WriteLine("4.Update Order");
            Console.WriteLine("5.Delete Oreder");
            Console.WriteLine("0.Return to main menu");

        }
        public static void OrderItemCheckMenu()
        {
            Console.WriteLine("1.Add Order Item");
            Console.WriteLine("2.See Order Item By ID");
            Console.WriteLine("3.See List of Orders Item");
            Console.WriteLine("4.Update Order Item");
            Console.WriteLine("5.Delete Order Item");
            Console.WriteLine("0.Return to main menu");

        }
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
       
        public static Category CategoryInput()
        {
            int option;
            do Console.Write("Enter Category: 1 - Shirts, 2 - trousers, 3 - shoes, 4 - coats,5 - sweaters  \n");
            while (!int.TryParse(Console.ReadLine(), out option) && (option > 0 || option < 5));
            Console.WriteLine("\n");
            return (Category)option;
        }
        public static int IntInput(string messege)
        {
            int option;
            Console.WriteLine(messege);
            while (!int.TryParse(Console.ReadLine(), out option))
                Console.WriteLine("you must entar numbers ony. try again!");
            return option;
        }
        
        public static string emailInput()
        {
            string ?option;

            Console.WriteLine("Enter the email:");
            option = Console.ReadLine();
            while (!option.Contains("@")|| !option.Contains("."))
            {
                Console.WriteLine("You must enter a correct email address. Try again!");
                option = Console.ReadLine();
            }

            return option;
        }

        public static DateTime dateinput()
        {
            DateTime date = new DateTime();
            while (!DateTime.TryParse(Console.ReadLine(), out date) )
                Console.WriteLine("try again");
            Console.WriteLine("\n");
            return date;
        }
    }
}
