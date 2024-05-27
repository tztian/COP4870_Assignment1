using COP4870_Assignment1.Services;
using COP4870_Assignment1.Models;

namespace COP4870_Assignment1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var itemSvc = ItemListProxy.Current;

            Console.WriteLine("Welcome to the Marketplace!\n");

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
                            itemSvc = ManageInventory(itemSvc);
                            break;
                        case 2:
                            //Shop(itemSvc);
                            break;
                        case 3:
                            Environment.Exit(-1);
                            break;

                    }
                }
            }
        }

        public static ItemListProxy ManageInventory(ItemListProxy itemSvc)
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
                            return itemSvc;
                    }
                }
            }
        }
    }
}
