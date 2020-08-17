using System;
using System.Collections.Generic;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Store
    {
        public Store()
        {
            Inventory = new HashSet<Inventory>();
            ShoppingCart = new HashSet<ShoppingCart>();
        }

        public int StoreId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
    }
}
