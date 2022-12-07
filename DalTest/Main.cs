using Dal;
using DalApi;
using DO;

namespace DalTest;


public class mainProgram
{
    static void Main(string[] args)
    {
        //dal obj initial
        IDal dalList = DalList.Instance;

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
                        case ProductOptions.Get:
                            GetProduct();
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


        /// function to add product
        void AddProduct()
        {
            product.UniqID = Menu.IntInput("Enter the ID:");
            product.Name = Menu.StringInput("Enter the name:");
            product.Price = Menu.IntInput("Enter the price:");
            product.Category = Menu.CategoryInput();
            product.InStock = Menu.IntInput("Enter How much is in stock:");
            Console.Write("{0} Was Added Successfully \n", dalList.Product.Add(product));
        }

        /// function to get product by ID
        void GetProduct()
        {
            int idinput = Menu.IntInput("Enter the ID number:");
            try
            {
                Console.Write(dalList.Product.Get(idinput));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// function to see all the products
        void GetAllProduct()
        {
            try
            {
                IEnumerable<Product?> products = dalList.Product.GetAll();
                foreach (var productitem in products)
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
            Console.Write(dalList.Product.Get(product.UniqID));
            product.Name = Menu.StringInput("Enter the name:");
            product.Price = Menu.IntInput("Enter the price:");
            product.Category = Menu.CategoryInput();
            product.InStock = Menu.IntInput("Enter How much is in stock:");
            dalList.Product.Update(product);
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
                dalList.Product.Delete(del);
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
            Console.Write("Order {0} Was Added Successfully \n", dalList.Order.Add(order));
        }
        /// function to get an order by ID
        void GetOrder()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                Console.Write(dalList.Order.Get(idinput));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// function to see all orders
        void GetAllOrder()
        {
            try
            {
                IEnumerable<Order?> order1 = dalList.Order.GetAll();
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
                Console.Write(dalList.Order.Get(order.UniqID));
                order.CustomerName = Menu.StringInput("Enter the name:");
                order.CustomerAdress = Menu.StringInput("Enter the address:");
                order.CustomerEmail = Menu.emailInput();
                Console.WriteLine("Enter Order Date");
                order.OrderDate = Menu.dateinput();
                Console.WriteLine("Enter Ship Date:");
                order.ShipDate = Menu.dateinput();
                Console.WriteLine("Enter Delivery Date:");
                order.DeliveryrDate = Menu.dateinput();
                dalList.Product.Update(product);
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
                dalList.Order.Delete(del);
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
                Console.Write("{0} Was Added Successfully \n", dalList.OrderItem.Add(orderItem));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// function to see an orderitem by id
        void GetOrderItem()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                Console.Write(dalList.OrderItem.Get(idinput));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// function to see an orderitem by product id
        void GetOrderItemByProduct()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                Func<OrderItem?, bool> func = orderItem => orderItem?.ProductID == idinput;
                IEnumerable < OrderItem?> orderitem1 = dalList.OrderItem.GetAll(func);
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
        void GetOrderItemByOrder()
        {
            try
            {
                int idinput = Menu.IntInput("Entter the ID:");
                Func<OrderItem?, bool> func = orderItem => orderItem?.OrderID == idinput;
                IEnumerable<OrderItem?> orderitem1 = dalList.OrderItem.GetAll(func);
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
        void GetAllOrderItem()
        {
            try
            {
                IEnumerable<OrderItem?> orderitem1 = dalList.OrderItem.GetAll();
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
            Console.Write(dalList.OrderItem.Get(orderItem.UniqID));
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
            dalList.OrderItem.Delete(del);
            Console.Write("{0} Was Delete Successfully \n", del);

        }
    }
}