using Dal;
using DO;

namespace DalTest;


public class mainProgram
{
    static void Main(string[] args)
    {

        //dal obj initial
        DalProduct dalProduct = new DalProduct();
        DalOrder dalOrder = new DalOrder();
        DalOrderItem dalOrderItem = new DalOrderItem();

        //DO obj initial
        Product product = new Product();
        
        Order order = new Order();
        OrderItem orderItem = new OrderItem();

        
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
                    ///Option 2
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
                    ///Option 3
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


        /// function to add product
        void AddProduct()
        {
            product.UniqID = 0;
            product.Name = Menu.StringInput("Enter the name:");
            product.Price = Menu.IntInput("Enter the price:");
            product.Category = Menu.CategoryInput();
            product.InStock = Menu.IntInput("Enter How much is in stock:");
            Console.Write("{0} Was Added Successfully \n", dalProduct.Add(product));
        }

        /// function to read product by ID
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
        /// function to see all the products
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

        /// function to update a product
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
        /// function to delete product
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

        /// function to add an order
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
        /// function to read an order by ID
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
        /// function to see all orders
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
        /// function to update an order
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
        /// function to delete an order
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

        /// function to add an orderitem
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

        /// function to see an orderitem by id
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
        /// function to see an orderitem by product id
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
        /// function to see an orderitem by order id
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

        /// function to see all the orderitems
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

        /// function to update an orderitem
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

        /// function to delete orderitem
        void DeleteOrderItem()
        {
            int del = Menu.IntInput("Entter the ID:");
            dalOrderItem.Delete(del);
            Console.Write("{0} Was Delete Successfully \n", del);

        }
    }
}