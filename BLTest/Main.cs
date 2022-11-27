using BLApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DalApi;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BLTest;

public class mainProgram
{
    static void Main(string[] args)
    {
        int id, instock;
        string name;
        double price;
        BO.Category _category;

        BO.DisplayException _displayException =new BO.DisplayException(null);
        //dl obj initial
        IBL bl = new Bl();

        //BO obj initial
        BO.Product product = new BO.Product();

        BO.Order order = new BO.Order();
        BO.OrderItem orderItem = new BO.OrderItem();


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
                    Menu.ProductCheckMenu();
                    ProductOptions AddingMenuChoice = (ProductOptions)Menu.IntInput("Enter your choice:");
                    switch (AddingMenuChoice)
                    {
                        case ProductOptions.Add:
                           AddProduct();
                            try { bl.Product.AddProduct(id, name, price, _category, instock); Console.WriteLine("Success to add product"); }
                            catch (AggregateException ex) { _displayException(ex); };
                          
                            break;
                        case ProductOptions.GetForManagger:
                            ProductItemForManager();
                            break;
                        case ProductOptions.GetForCostemor:
                            ProductItemForCostemor();
                            break;
                        case ProductOptions.GetAllList:
                            GetListOfProducts();
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
                    break;
                ///Option 2
                case MainOptions.OrderCheck:
                    OrderCheckSwitch();
                    Menu.OrderCheckMenu();
                    OrderOptions UpdateMenuChoice = (OrderOptions)Menu.IntInput("Enter your choice:");
                    switch (UpdateMenuChoice)
                    {
                        case OrderOptions.Add:
                            AddOrder();
                            break;
                        case OrderOptions.Get:
                            GetOrder();
                            break;
                        case OrderOptions.GetAllList:
                            GetAllOrder();
                            break;
                        case OrderOptions.Update:
                            UpdateOrder();
                            break;
                        case OrderOptions.Delete:
                            DeleteOrder();
                            break;
                        default:
                            break;
                    }

                    break;
                ///Option 3
                case MainOptions.OrderItemCheck:
                    Menu.OrderItemCheckMenu();
                    OrderItemOptions OrderItemMenuChoice = (OrderItemOptions)Menu.IntInput("Enter your choice:");
                    switch (OrderItemMenuChoice)
                    {
                        case OrderItemOptions.Add:
                            AddOrderItem();
                            break;
                        case OrderItemOptions.Get:
                            GetOrderItem();
                            break;
                        case OrderItemOptions.GetByOrder:
                            GetOrderItemByOrder();
                            break;
                        case OrderItemOptions.GetByProduct:
                            GetOrderItemByProduct();
                            break;
                        case OrderItemOptions.GetAllList:
                            GetAllOrderItem();
                            break;
                        case OrderItemOptions.Update:

                            UpdateOrderItem();
                            break;

                        case OrderItemOptions.Delete:

                            DeleteOrderItem();
                            break;
                        default:
                            break;
                    }
                    break;


                default:
                    break;
            }
        } while (menuChoice != 0);

        void AddProduct()
        {
            int UniqID = Menu.IntInput("Enter the ID:");
            string Name = Menu.StringInput("Enter the name:");
            double Price = Menu.IntInput("Enter the price:");
            BO.Category category = Menu.CategoryInput();
            int InStock = Menu.IntInput("Enter How much is in stock:");
            try { bl.Product.AddProduct(UniqID, Name, Price, category, InStock); }
            catch (AggregateException ex) { throw new BO.DisplayException(ex.Message); };
           
            Console.Write("{0} Was Added Successfully \n", bl.Product.ProductItemForManager(UniqID));
        }


    }

    private static void OrderCheckSwitch()
    {
        throw new NotImplementedException();
    }
}
