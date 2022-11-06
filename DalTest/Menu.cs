using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;





public enum MainOptions
{
    ProductCheck=1,
    OrderCheck,
    OrderItemCheck,
    DISPLAY,
    DISPLAY_LIST
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
        Console.WriteLine("4.Display list options");
        Console.WriteLine("0.Exit");

        }
        public static void ProdectCheckMenu()
        {
            Console.WriteLine("1.Add Product");
            Console.WriteLine("2.See Product By ID");
            Console.WriteLine("3.See List of Products");
            Console.WriteLine("4.Update Product");
            Console.WriteLine("4.Delete Product");
            Console.WriteLine("0.Return to main menu");

        }
        public static void OrderCheckMenu()
        {
            Console.WriteLine("1.Add Order");
            Console.WriteLine("2.See Order By ID");
            Console.WriteLine("3.See List of Orders");
            Console.WriteLine("4.Update Order");
            Console.WriteLine("4.Delete Oreder");
            Console.WriteLine("0.Return to main menu");

        }
        public static void OrderItemCheckMenu()
        {
            Console.WriteLine("1.Add Order Item");
            Console.WriteLine("2.See Order Item By ID");
            Console.WriteLine("3.See List of Orders Item");
            Console.WriteLine("4.Update Order Item");
            Console.WriteLine("4.Delete Order Item");
            Console.WriteLine("0.Return to main menu");

        }
        public static void DisplayListMenu()
        {
            Console.WriteLine("1.Display ");
           

        }

        public static int UserInput()
        {
            int option;
            do Console.Write("Choose an option: \n");
            while (!int.TryParse(Console.ReadLine(), out option));
            Console.WriteLine("\n");
            return option;
        }

        public static int IDInput()
        {
            int option;
            do
            { Console.Write("Enter ID: \n"); }
            while (!int.TryParse(Console.ReadLine(), out option) );
            Console.WriteLine("\n");

            return option;
        }
        public static string NameInput()
        {
            string option=null;
            do 
            {
                Console.Write("Enter Name: \n");
                option = Console.ReadLine();

            }
            while(!Regex.IsMatch(option, @"^[a-zA-Z]+$"));

           // while (!int.TryParse(Console.ReadLine(), out option));
            Console.WriteLine("\n");
            return option;
        }
        public static int PriceInput()
        {
            int option;
            do Console.Write("Enter Price: \n");
            while (!int.TryParse(Console.ReadLine(), out option));
            Console.WriteLine("\n");
            return option;
        }
        public static Category CategoryInput()
        {
            int option;
            do Console.Write("Enter Category: 1 - Shirts, 2 - trousers, 3 - shoes, 4 - coats,5 - sweaters  \n");
            while (!int.TryParse(Console.ReadLine(), out option) && (option >0 || option <5));
            Console.WriteLine("\n");
            return (Category)option;
        }
        public static int InstockInput()
        {
            int option;
            do Console.Write("Enter How much in stock: \n");
            while (!int.TryParse(Console.ReadLine(), out option));
            Console.WriteLine("\n");
            return option;
        }


    }
}
