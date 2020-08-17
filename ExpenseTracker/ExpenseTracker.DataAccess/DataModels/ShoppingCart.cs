using System;
using System.Collections.Generic;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            CartItem = new HashSet<CartItem>();
            Orders = new HashSet<Orders>();
        }

        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<CartItem> CartItem { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
