using System;
using System.Collections.Generic;
using System.Text;

namespace KnapsackSubmission
{
    public class Item
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return "Name : " + Name + "\tCost : " + Cost + "\tValue : " + Value;
        }
    }
}
