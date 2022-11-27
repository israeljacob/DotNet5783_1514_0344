using BLApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DalApi;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BLTest;

public class mainProgram
{
    static void Main(string[] args)
    {
        //dl obj initial
        IBL bl = new Bl();

        //BO obj initial
        BO.Product product = new BO.Product();

        BO.Order order = new BO.Order();
        BO.Cart cart = new BO.Cart();


        MainOptions menuChoice;
        do
        {
            ///Display the options for the main menu
            Menu.MainMenu();
            menuChoice = (MainOptions)Menu.IntInput("Enter your choice:");
            switch (menuChoice)
            {
                ///Option-1
                case MainOptions.ProductCheck:
                    ProductCheckSwitch();
                    break;
                ///Option 2
                case MainOptions.OrderCheck:
                    OrderCheckSwitch();
                    break;
                ///Option 3
                case MainOptions.CartCheck:
                    CartCheckSwitch();
                    break;
                default:
                    break;
            }
        } while (menuChoice != 0);

        void ProductCheckSwitch()
        {
            Menu.ProductCheckMenu();
            ProductOptions AddingMenuChoice = (ProductOptions)Menu.IntInput("Enter your choice:");
            switch (AddingMenuChoice)
            {
                case ProductOptions.Add:
                    AddProduct();
                    break;
                case ProductOptions.GetForManagger:
                    GetProductForManagger();
                    break;
                case ProductOptions.GetForCostemor:
                    GetProductForCostemor();
                    break;
                case ProductOptions.GetAllList:
                    GetAllProducts();
                    break;
                case ProductOptions.Update:
                    UpdateProduct();
                    break;
                case ProductOptions.Delete:
                    DeleteProduct();
                    break;
                default:
                    break;
            }

        }

        void OrderCheckSwitch()
        {
            //Menu.OrderCheckMenu();
            //OrderOptions UpdateMenuChoice = (OrderOptions)Menu.IntInput("Enter your choice:");
            //switch (UpdateMenuChoice)
            //{
            //    case OrderOptions.Add:
            //        AddOrder();
            //        break;
            //    case OrderOptions.Get:
            //        GetOrder();
            //        break;
            //    case OrderOptions.GetAllList:
            //        GetAllOrder();
            //        break;
            //    case OrderOptions.Update:
            //        UpdateOrder();
            //        break;
            //    case OrderOptions.Delete:
            //        DeleteOrder();
            //        break;
            //    default:
            //        break;
            //}
        }
        void CartCheckSwitch()
        {
            Menu.CartCheckMenu();
            CartOptions CartMenuChoice = (CartOptions)Menu.IntInput("Enter your choice:");
            switch (CartMenuChoice)
            {
                case CartOptions.Add:
                    addItemToCart();
                    break;
                case CartOptions.Update:
                    updateCart();
                    break;
                case CartOptions.ExecuteOrder:
                    executeOrder();
                    break;
                default:
                    break;
            }
        }


        void AddProduct()
        {
            try
            {
                bl.Product.AddProduct(
                    Menu.IntInput("Enter the ID"),
                    Menu.StringInput("Enter the name"),
                    Menu.IntInput("Enter the price"),
                    Menu.CategoryInput(),
                    Menu.IntInput("Enter the amount that is un stock"));
            }
            catch (BO.EmptyException ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("The product has been added succesfully");
        }

        void GetProductForManagger()
        {
            int ID = Menu.IntInput("Enter the id");
            try { Console.WriteLine(bl.Product.ProductItemForManager(ID)); }
            catch (BO.IdNotExistException ex) { Console.WriteLine(ex.Message); }
        }

        void GetProductForCostemor()
        {
            int ID = Menu.IntInput("Enter the id");
            try { Console.WriteLine(bl.Product.ProductItemForCostemor(ID, cart)); }
            catch (BO.IdNotExistException ex) { Console.WriteLine(ex.Message); }
        }

        void GetAllProducts()
        {
            foreach (BO.ProductForList product in bl.Product.GetListOfProducts())
            {
                Console.WriteLine(product);
            }
        }

        void UpdateProduct()
        {
            try
            {
                bl.Product.UpdateProduct(new BO.Product
                {
                    UniqID = Menu.IntInput("Enter the ID"),
                    Name = Menu.StringInput("Enter the name"),
                    Price = Menu.IntInput("Enter the price"),
                    Category = Menu.CategoryInput(),
                    InStock = Menu.IntInput("Enter the amount that is un stock")
                });
            }
            catch (BO.IdNotExistException ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("The product has been updated succesfully");
        }

        void DeleteProduct()
        {
            int ID = Menu.IntInput("Enter the id");
            try { bl.Product.DeleteProduct(ID); }
            catch (BO.IdNotExistException ex) { Console.WriteLine(ex.Message); }
            Console.WriteLine("The product has been deletted succesfully");
        }

        void AddProduct()
        {
            throw new NotImplementedException();
        }
    }

    
}
