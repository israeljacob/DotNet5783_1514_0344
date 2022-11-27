using BLApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DalApi;

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
                    GetProductForCostemor;
                    break;
                case ProductOptions.GetAllList:
                    GetAllProduct();
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
        }
        void CartCheckSwitch()
        {
            Menu.CartCheckMenu();
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
        }


        void GetProductForManagger()
        {
            throw new NotImplementedException();
        }

        void AddProduct()
        {
            throw new NotImplementedException();
        }
    }

    
}
