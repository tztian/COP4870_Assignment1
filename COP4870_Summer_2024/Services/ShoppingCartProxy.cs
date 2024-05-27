using COP4870_Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COP4870_Assign1.Services
{
    public class ShoppingCartProxy
    {
        private List<Item> contents;
        private double taxRate;

        public ReadOnlyCollection<Item>? Contents
        {
            get
            {
                return contents?.AsReadOnly();
            }
        }

        // referenceing Shopping Cart Example 3
        public double Subtotal
        {
            get
            {
                return contents.Sum(c => c.Price * c.Count);
            }
        }

        public double Taxes
        {
            get
            {
                return taxRate * Subtotal;
            }
        }

        public double Total
        {
            get
            {
                return Subtotal + Taxes;
            }
        }

        public string Receipt
        {
            get
            {
                var receipt = "Thank you for shopping!\n";

                foreach (var item in contents)
                {
                    receipt += $"{item}\n";
                }

                receipt += $"Subtotal: {Subtotal:C}\nTaxes: {Taxes:C}\nTotal: {Total:C}\n\n";

                return receipt;
            }
        }

        public ShoppingCartProxy(double tRate = 0.07)
        {
            contents = new List<Item>();
            taxRate = tRate;
        }

        public void AddItem(Item item)
        {
            //does my cart already contain this item?
            if (contents.Any(c => c.Id.Equals(item.Id)))
            {
                //if so add to the amount in the cart
                contents.First(c => c.Id.Equals(item.Id)).Count += item.Count;
            }
            else
            {
                //otherwise, just add the product to the cart
                contents.Add(item);
            }
        }

        public bool RemoveItem(int id, int count)
        {
            //if the cart contains this product
            var itemInCart = contents.FirstOrDefault(c => c.Id.Equals(id));
            if (itemInCart != null)
            {
                if (count > itemInCart.Count)
                {
                    Console.WriteLine("There are not enough items in your cart.");
                    return false;
                }

                itemInCart.Count -= count;

                //if we removed the last of the amount, remove the entire product from the cart
                if (itemInCart.Count <= 0)
                {
                    contents.Remove(itemInCart);
                }
                return true;
            }
            return false;
        }

        public string Checkout()
        {
            var receipt = Receipt;
            contents = new List<Item>();
            return receipt;
        }
    }
}
