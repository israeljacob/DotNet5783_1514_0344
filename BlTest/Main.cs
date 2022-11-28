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
using BO;

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
        /// <summary>
        /// If order has been chosen
        /// </summary>
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
        /// <summary>
        /// If order has been chosen
        /// </summary>
        void OrderCheckSwitch()
        {
            Menu.OrderCheckMenu();
            OrderOptions UpdateMenuChoice = (OrderOptions)Menu.IntInput("Enter your choice:");
            switch (UpdateMenuChoice)
            {
                case OrderOptions.Get:
                    getOrder();
                    break;
                case OrderOptions.GetAllList:
                    getAllOrders();
                    break;
                case OrderOptions.Update:
                    updateOrder();
                    break;
                case OrderOptions.UpdateShipDate:
                    updateShipDate();
                    break;
                case OrderOptions.UpdateDeliveryDate:
                    updateDeliveryDate();
                    break;
                case OrderOptions.Track:
                    trackOrder();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// If cart has been chosen
        /// </summary>
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

        /// <summary>
        /// Add product
        /// </summary>
        void AddProduct()
        {
            // Get the details
            int ID = Menu.IntInput("Enter the ID");
            string name = Menu.StringInput("Enter the name");
            int price = Menu.IntInput("Enter the price");
            BO.Category category = Menu.CategoryInput();
            int inStock = Menu.IntInput("Enter the amount that is un stock");
            // Try to add and throw an exception if not succeed
            try
            {
               bl.Product.AddProduct(ID, name, price, category, inStock);
            }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
            Console.WriteLine("The product has been added succesfully");
        }
        ///<summary>
        ///
        /// </summary>
        void GetProductForManagger()
        {
            // Try to add and throw an exception if not succeed
            int ID = Menu.IntInput("Enter the id");
            try { Console.WriteLine(bl.Product.ProductItemForManager(ID)); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
          //  catch (BO.IdNotExistException ex) { Console.WriteLine(ex.Message);return; }
        }
        ///<summary>
        ///Get product for costemor
        /// </summary>
        void GetProductForCostemor()
        {
            // Try to get and throw an exception if not succeed
            int ID = Menu.IntInput("Enter the id");
            try { Console.WriteLine(bl.Product.ProductItemForCostemor(ID, cart)); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
        }
        ///<summary>
        ///Get all products
        /// </summary>
        void GetAllProducts()
        {
            /// print all products
            foreach (BO.ProductForList product in bl.Product.GetListOfProducts())
            {
                Console.WriteLine(product);
            }
        }
        ///<summary>
        ///Update product
        /// </summary>
        void UpdateProduct()
        {
            // Try to update and throw an exception if not succeed
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
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
            Console.WriteLine("The product has been updated succesfully");
        }
        ///<summary>
        ///Delete product
        /// </summary>
        void DeleteProduct()
        {
            // Try to delete and throw an exception if not succeed
            int ID = Menu.IntInput("Enter the id");
            try { bl.Product.DeleteProduct(ID); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
            Console.WriteLine("The product has been deletted succesfully");
        }

        ///<summary>
        ///
        /// </summary>
        void addItemToCart()
        {
            int ID = Menu.IntInput("Enter the ID");
            try
            {
                bl.Cart.AddToCart(cart, ID);
            }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
            Console.WriteLine("The item has been added succesfully");
        }
        ///<summary>
        ///
        /// </summary>
        void updateCart()
        {
            int ID = Menu.IntInput("Enter the ID"), amount = Menu.IntInput("Enter the new amount");
            try
            {
                bl.Cart.UpdateCart(cart, ID, amount);
            }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
            Console.WriteLine("The amount of item has been updated succesfully");
        }
        ///<summary>
        ///
        /// </summary>
        void executeOrder()
        {
            try
            {
                bl.Cart.ExecuteOrder(cart);
            }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
            Console.WriteLine("The order has been executed succesfully");
        }
        ///<summary>
        ///
        /// </summary>
        void getOrder()
        {
            int ID = Menu.IntInput("Enter the ID");
            try { Console.WriteLine(bl.Order.OrderBYID(ID)); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };

        }
        ///<summary>
        ///
        /// </summary>
        void getAllOrders()
        {
            foreach (BO.OrderForList order in bl.Order.GetListOfOrders())
            {
                Console.WriteLine(order);
            }
        }
        ///<summary>
        ///
        /// </summary>
        void updateOrder()
        {
            int ID = Menu.IntInput("Enter the ID");
            try { Console.WriteLine(bl.Order.UpdateOrderItemAmount(bl.Order.GetOrderItem(ID))); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
        }
        ///<summary>
        ///
        /// </summary>
        void updateShipDate()
        {
            int ID = Menu.IntInput("Enter the ID");
            try { Console.WriteLine(bl.Order.UpdateShipDate(ID)); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
        }
        ///<summary>
        ///
        /// </summary>
        void updateDeliveryDate()
        {
            int ID = Menu.IntInput("Enter the ID");
            try { Console.WriteLine(bl.Order.UpdateDeliveryDate(ID)); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };

        }
        ///<summary>
        ///
        /// </summary>
        void trackOrder()
        {
            int ID = Menu.IntInput("Enter the ID");
            try { Console.WriteLine(bl.Order.OrderTrack(ID)); }
            catch (AggregateException ex) { Console.WriteLine(ex.Message); return; };
        }
    }
}
