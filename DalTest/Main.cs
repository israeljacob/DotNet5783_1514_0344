using Dal;
using DO;

namespace DalTest;




public class mainProgram
{
    static void Main(string[] args)
    {
        
        //dal
        DalProduct dalProduct = new DalProduct();
        DalOrder dalOrder = new DalOrder();
        DalOrderItem dalOrderItem = new DalOrderItem();

        //DO
        Product product = new Product();
        
        Order order = new Order();
        OrderItem orderItem = new OrderItem();

        MainOptions menuChoice;
     //   dalProduct.Read(15);
        do
        {
            Menu.MainMenu();
            menuChoice = (MainOptions)Menu.IntInput("Enter your choice:");
            switch (menuChoice)
            {
                case MainOptions.ProductCheck:
                    Menu.ProductCheckMenu();
                    ProductOptions AddingMenuChoice = (ProductOptions)Menu.IntInput("Enter your choice:");
                    switch (AddingMenuChoice)
                    {
                        case ProductOptions.Add:
                            AddProduct();
                            break;
                        case ProductOptions.Read:
                            ReadProduct();
                            break;
                        case ProductOptions.ReadAllList:
                            ReadAllProduct();
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

                case MainOptions.OrderCheck:
                    Menu.OrderCheckMenu();
                    OrderOptions UpdateMenuChoice = (OrderOptions)Menu.IntInput("Enter your choice:");
                    switch (UpdateMenuChoice)
                    {
                        case OrderOptions.Add:
                            AddOrder();
                            break;
                        case OrderOptions.Read:
                            ReadOrder();
                            break;
                        case OrderOptions.ReadAllList:
                            ReadAllOrder();
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

                case MainOptions.OrderItemCheck:
                    Menu.OrderItemCheckMenu();
                    OrderItemOptions OrderItemMenuChoice = (OrderItemOptions)Menu.IntInput("Enter your choice:");
                    switch (OrderItemMenuChoice)
                    {
                        case OrderItemOptions.Add:
                            AddOrderItem();
                            break;
                        case OrderItemOptions.Read:
                            ReadOrderItem();
                            break;
                        case OrderItemOptions.ReadByOrder:
                            ReadOrderItemByOrder();
                            break;
                        case OrderItemOptions.ReadByProduct:
                            ReadOrderItemByProduct();
                            break;
                        case OrderItemOptions.ReadAllList:
                            ReadAllOrderItem();
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
            product.UniqID = 0;
            product.Name = Menu.StringInput("Enter the name:");
            product.Price = Menu.IntInput("Enter the price:");
            product.Category = Menu.CategoryInput();
            product.InStock = Menu.IntInput("Enter How much is in stock:");
            Console.Write("{0} Was Added Successfully \n", dalProduct.Add(product));
        }
        void ReadProduct()
        {
            int idinput = Menu.IntInput("Enter the ID number:");
            try
            {
                Console.Write(dalProduct.Read(idinput));
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void ReadAllProduct()
        {
            try
            {
                Product[] products1 = dalProduct.ReadAll();
                foreach (var productitem in products1)
                {
                    Console.Write(productitem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void UpdateProduct()
        {
            product.UniqID = Menu.IntInput("Entter the ID:");
            Console.Write(dalProduct.Read(product.UniqID));
            product.Name = Menu.StringInput("Enter the name:");
            product.Price = Menu.IntInput("Enter the price:");
            product.Category = Menu.CategoryInput();
            product.InStock = Menu.IntInput("Enter How much is in stock:");
            dalProduct.Update(product);
            try
            {
                Console.Write("{0} Was Update Successfully \n", product.UniqID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void DeleteProduct()
        {
            try
            {
                int del = Menu.IntInput("Entter the ID:");
                dalProduct.Delete(del);
                Console.Write("{0} Was Delete Successfully \n", del);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void AddOrder()
        {
            order.UniqID = 0;
            order.CustomerName = Menu.StringInput("Enter the name:");
            order.CustomerAdress = Menu.StringInput("Enter the address:");
            order.CustomerEmail = Menu.emailInput();
            Console.WriteLine("Enter Order Date");
                order.OrderDate = Menu.dateinput();
            Console.WriteLine("Enter Ship Date:");
            order.ShipDate = Menu.dateinput();
            Console.WriteLine("Enter Delivery Date:");
            order.DeliveryrDate = Menu.dateinput();
            Console.Write("Order {0} Was Added Successfully \n", dalOrder.Add(order));
        }
        void ReadOrder()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                Console.Write(dalOrder.Read(idinput));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void ReadAllOrder()
        {
            try
            {
                Order[] order1 = dalOrder.ReadAll();
                foreach (var orderitem in order1)
                {
                    Console.WriteLine(orderitem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void UpdateOrder()
        {
            try
            {
                order.UniqID = Menu.IntInput("Entter the ID:");
                Console.Write(dalOrder.Read(order.UniqID));
                order.CustomerName = Menu.StringInput("Enter the name:");
                order.CustomerAdress = Menu.StringInput("Enter the address:");
                order.CustomerEmail = Menu.emailInput();
                Console.WriteLine("Enter Order Date");
                order.OrderDate = Menu.dateinput();
                Console.WriteLine("Enter Ship Date:");
                order.ShipDate = Menu.dateinput();
                Console.WriteLine("Enter Delivery Date:");
                order.DeliveryrDate = Menu.dateinput();
                dalProduct.Update(product);
                Console.Write("\n{0} Was Update Successfully \n", product.UniqID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void DeleteOrder()
        {
            try
            {
                int del = Menu.IntInput("Entter the ID:");
                dalOrder.Delete(del);
                Console.Write("{0} Was Delete Successfully \n", del);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void AddOrderItem()
        {
            try
            {
                orderItem.UniqID = 0;
                orderItem.ProductID = Menu.IntInput("Enter the product ID:");
                orderItem.OrderID = Menu.IntInput("Enter the order ID:");
                orderItem.Price = Menu.IntInput("Enter the price:");
                orderItem.Amount = Menu.IntInput("Enter the Amount:");
                Console.Write("{0} Was Added Successfully \n", dalOrderItem.Add(orderItem));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void ReadOrderItem()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                Console.Write(dalOrderItem.Read(idinput));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void ReadOrderItemByProduct()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                OrderItem[] orderitem1 = dalOrderItem.ReadByProduct(idinput);
                foreach (var orderitem in orderitem1)
                {
                    Console.Write(orderitem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void ReadOrderItemByOrder()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                OrderItem[] orderitem1 = dalOrderItem.ReadByOrder(idinput);
                foreach (var orderitem in orderitem1)
                {
                    Console.Write(orderitem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void ReadAllOrderItem()
        {
            try
            {
                OrderItem[] orderitem1 = dalOrderItem.ReadAll();
                foreach (var orderitem in orderitem1)
                {
                    Console.Write(orderitem);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        void UpdateOrderItem()
        {
            orderItem.UniqID = Menu.IntInput("Entter the ID:");
            Console.Write(dalOrderItem.Read(orderItem.UniqID));
            orderItem.ProductID = Menu.IntInput("Enter the product ID:");
            orderItem.OrderID = Menu.IntInput("Enter the order ID:");
            try
            {
                orderItem.Price = Menu.IntInput("Enter the price:");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            orderItem.Amount = Menu.IntInput("Enter the Amount:");
            Console.Write("\n{0} Was Update Successfully \n", product.UniqID);
        }

        void DeleteOrderItem()
        {
            int del = Menu.IntInput("Entter the ID:");
            dalOrderItem.Delete(del);
            Console.Write("{0} Was Delete Successfully \n", del);

        }
    }
}