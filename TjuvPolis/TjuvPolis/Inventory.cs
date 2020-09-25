using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvPolis
{
    class Inventory
    {
        public string Item { get; set; }
        public int NumberOfItems { get; set; }

        public Inventory(string item, int numberOfItems)
        {
            Item = item;
            NumberOfItems = numberOfItems;
        }
    }
}
