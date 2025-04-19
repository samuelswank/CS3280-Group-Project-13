using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Common
{
    /// <summary>
    /// Represents an item for sale with its cost
    /// </summary>
    public class clsItem
    {
        /// <summary>
        /// Single latin letter acting as the unique primary for the desired item
        /// </summary>
        public string ItemCode { get; set; }
        /// <summary>
        /// Name of the item
        /// </summary>
        public string ItemDesc { get; set; }
        /// <summary>
        /// Cost in USD dollars and cents of the item formatted as ${dollars}.{cents}
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// Overriden ToString method for class clsItem, displays data as
        /// { ItemCode, ItemDesc, Cost }
        /// </summary>
        /// <returns>string representation of clsItem instance</returns>
        public override string ToString() { return "{ " + ItemCode + ", " + ItemDesc + ", " + Cost + " }"; }
    }
} 
