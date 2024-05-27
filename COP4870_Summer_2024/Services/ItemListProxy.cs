using COP4870_Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COP4870_Assignment1.Services
{
    public class ItemListProxy
    {
        private ItemListProxy()
        {
            items = new List<Item>();
        }

        private static ItemListProxy? instance;
        private static object instanceLock = new object();

        public static ItemListProxy Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ItemListProxy();
                    }
                }
                return instance;
            }
        }

        private List<Item> items;
        public ReadOnlyCollection<Item>? Items
        {
            get
            {
                return items?.AsReadOnly();
            }
        }

        public int LastId
        {
            get
            {
                if(items?.Any() ?? false)
                {
                    return items?.Select(c => c.Id).Max() ?? 0;

                }
                return 0;
            }
        }

        public Item? Add(Item item)
        {
            if (items == null)
            {
                return null;
            }

            var isAdd = false;

            if (item.Id == 0)
            {
                item.Id = LastId + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                items.Add(item);
            }
            return item;
        }

        public Item? Update(Item item)
        {
            if (items == null)
            {
                return null;
            }

            var existingItem = items.Find(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
                existingItem.Price = item.Price;
                existingItem.Count = item.Count;

                return item;
            }
            else
            {
                throw new ArgumentException($"Item with ID {item.Id} does not exist.");
            }
        }

        public void Delete(int id)
        {
            if (items == null)
            {
                return;
            }

            var itemToDelete = items.FirstOrDefault(c => c.Id == id);

            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
        }

        public Item? GetItemByID(int id)
        {
            var item = items.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                var newItem = new Item
                {
                    Id = id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Count = item.Count
                };

                return newItem;
            }
            return null;
        }

        public bool UpdateCount(int id, int count, bool remove)
        {
            if (items == null)
            {
                return false;
            }

            var existingItem = items.Find(i => i.Id == id);
            if (existingItem == null)
            {
                return false;
            }

            if (remove == true)
            {
                if (existingItem.Count < count)
                {
                    Console.WriteLine("There are not enough items left.\n");
                    return false;
                }
                existingItem.Count -= count;
                return true;
            }
            else
            {
                existingItem.Count += count;
                return true;
            }

        }
            
    }
}
