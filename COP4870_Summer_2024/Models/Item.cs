using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COP4870_Assignment1.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        private double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                price = Math.Round(value, 2);
            }
        }

        private int count;
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Count cannot be negative.");
                }
                count = value;
            }
        }

        // default constructor
        public Item() { }

        public override string ToString()
        {
            return $"[{Id}] {Name}: {Description}\nPrice: ${Price}\nCount: {Count}\n";
        }
    }
}
