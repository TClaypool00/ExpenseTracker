using System;
using System.Collections.Generic;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Foods
    {
        public Foods()
        {
            CartItem = new HashSet<CartItem>();
            Inventory = new HashSet<Inventory>();
        }

        public int FoodId { get; set; }
        public int FoodGroupId { get; set; }
        public string FoodTitle { get; set; }
        public decimal Price { get; set; }

        public virtual FoodGroup FoodGroup { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
