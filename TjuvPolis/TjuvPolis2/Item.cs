using System;
using System.Collections.Generic;
using System.Text;

namespace TjuvPolis2
{
    class Item
    {
        public string ItemName { get; set; }
        public int NoOfItems { get; set; }
        public Item(string itemName, int noOfItems)
        {
            ItemName = itemName;
            NoOfItems = noOfItems;
        }
        public override string ToString()
        {
            return $"{ItemName}: {NoOfItems}";
        }
    }
}
