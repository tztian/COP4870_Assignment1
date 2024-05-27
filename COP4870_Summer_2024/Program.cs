using COP4870_Assignment1.Services;
using COP4870_Assignment1.Models;
using COP4870_Assign1.Services;
using System.Diagnostics;

namespace COP4870_Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var itemSvc = ItemListProxy.Current;
            var cart = new ShoppingCartProxy();

            Console.WriteLine("Welcome to Assignment 1!!\n");

            while (true)
            {
                // menu
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Inventory Management Page");
                Console.WriteLine("2. Shop Page");
                Console.WriteLine("3. Exit\n");

                var choice = Console.ReadLine();
                if (int.TryParse(choice, out int intChoice))
                {
                    switch (intChoice)
                    {
                        case 1:
                            ManageInventory(itemSvc);
                            break;
                        case 2:
                            Shop(itemSvc, cart);
                            break;
                        case 3:
                            Environment.Exit(-1);
                            break;

                    }
                }
            }
        }

        public static void ManageInventory(ItemListProxy itemSvc)
        {
            while (true)
            {
                Console.WriteLine("Inventory Management Page");
                Console.WriteLine("1. Create an Item");
                Console.WriteLine("2. List All Items");
                Console.WriteLine("3. Update an Item");
                Console.WriteLine("4. Delete an Item");
                Console.WriteLine("5. Return to Main Menu\n");

                var choice = Console.ReadLine();
                if (int.TryParse(choice, out int intChoice))
                {
                    switch (intChoice)
                    {
                        case 1:
                            Console.WriteLine("Enter the name of the item:");
                            var name = Console.ReadLine();
                            Console.WriteLine("Enter a description of the item:");
                            var desc = Console.ReadLine();

                            Console.WriteLine("Enter the price of the item:");
                            double.TryParse(Console.ReadLine(), out double price);

                            Console.WriteLine("Enter the count of the item:");
                            int.TryParse(Console.ReadLine(), out int count);
                            Console.WriteLine("\n");

                            itemSvc?.Add(
                                new Item
                                {
                                    Name = name,
                                    Description = desc,
                                    Price = price,
                                    Count = count
                                });
                            break;

                        case 2:
                            Console.WriteLine("Current Items:");
                            if (itemSvc?.Items?.Count > 0)
                            {
                                foreach (var item in itemSvc.Items)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no items in the inventory.\n");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Enter the ID of an item to update:");
                            if (int.TryParse(Console.ReadLine(), out int target))
                            {
                                bool exists = itemSvc?.Items?.Any(item => item.Id == target) ?? false;
                                if (!exists)
                                {
                                    Console.WriteLine("Invalid ID\n");
                                }
                                else
                                {
                                    // update the item
                                    Console.WriteLine("Enter the new name of the item:");
                                    name = Console.ReadLine();
                                    Console.WriteLine("Enter a new description of the item:");
                                    desc = Console.ReadLine();

                                    Console.WriteLine("Enter the new price of the item:");
                                    double.TryParse(Console.ReadLine(), out price);

                                    Console.WriteLine("Enter the new count of the item:");
                                    int.TryParse(Console.ReadLine(), out count);

                                    Console.WriteLine("\n");

                                    var newItem = new Item
                                    {
                                        Id = target,
                                        Name = name,
                                        Description = desc,
                                        Price = price,
                                        Count = count
                                    };

                                    itemSvc?.Update(newItem);
                                }

                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter the ID of an item to delete:");
                            if (int.TryParse(Console.ReadLine(), out target))
                            {
                                bool exists = itemSvc?.Items?.Any(item => item.Id == target) ?? false;
                                if (!exists)
                                {
                                    Console.WriteLine("Invalid ID\n");
                                }
                                else
                                {
                                    itemSvc?.Delete(target);
                                }
                            }
                            break;

                        case 5:
                            return;
                    }
                }
            }
        }
        
        public static void Shop(ItemListProxy itemSvc, ShoppingCartProxy cart)
        {
            while(true)
            {
                Console.WriteLine("Shopping Page");
                Console.WriteLine("1. List Inventory");
                Console.WriteLine("2. List Shopping Cart");
                Console.WriteLine("3. Add Item to Cart");
                Console.WriteLine("4. Remove Item from Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Return to Main Menu\n");

                var choice = Console.ReadLine();
                if (int.TryParse(choice, out int intChoice))
                {
                    switch (intChoice)
                    {
                        case 1:
                            Console.WriteLine("Current Items:");
                            if (itemSvc?.Items?.Count > 0)
                            {
                                foreach (var item in itemSvc.Items)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no items in the inventory.\n");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Shopping Cart:");
                            if (cart?.Contents?.Count > 0)
                            {
                                foreach (var item in cart.Contents)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Your shopping cart is empty.\n");
                            }
                            break;

                        case 3:
                            Console.WriteLine("Enter the ID of the item to add to cart:");
                            int.TryParse(Console.ReadLine(), out int id);
                            bool exists = itemSvc?.Items?.Any(item => item.Id == id) ?? false;
                            if (!exists)
                            {
                                Console.WriteLine("Invalid ID\n");
                                break;
                            }

                            Console.WriteLine("Enter the amount you want to buy:");
                            int.TryParse(Console.ReadLine(), out int count);

                            if (itemSvc.UpdateCount(id, count, true))
                            {
                                var itemToAdd = itemSvc.GetItemByID(id);
                                itemToAdd.Count = count;
                                cart.AddItem(itemToAdd);
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter the ID of the item to remove from cart:");
                            int.TryParse(Console.ReadLine(), out id);
                            exists = cart?.Contents?.Any(item => item.Id == id) ?? false;
                            if (!exists)
                            {
                                Console.WriteLine("Item is not in your cart\n");
                                break;
                            }

                            Console.WriteLine("Enter the amount you want to remove:");
                            int.TryParse(Console.ReadLine(), out count);
                            if (cart.RemoveItem(id, count))
                            {
                                itemSvc.UpdateCount(id, count, false);
                            }
                            break;

                        case 5:
                            // checkout
                            Console.WriteLine(cart.Checkout());
                            break;

                        case 6:
                            return;
                    }
                }
            }

        }
    
    }
}
