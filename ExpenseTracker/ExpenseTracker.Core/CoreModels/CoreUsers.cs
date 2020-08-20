using System.Collections.Generic;

namespace ExpenseTracker.Core.CoreModels
{
    public class CoreUsers
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }

        public List<CoreBills> Bills { get; set; } = new List<CoreBills>();
        public List<CoreBudget> Budget { get; set; } = new List<CoreBudget>();
        //public virtual ICollection<Comments> Comments { get; set; }
        public List<CoreLoan> Loan { get; set; } = new List<CoreLoan>();
        public List<CoreNonBills> NonBills { get; set; } = new List<CoreNonBills>();
        //public  ICollection<Reply> Reply { get; set; }
        //public  ICollection<Shoes> Shoes { get; set; }
        //public  ICollection<ShoppingCart> ShoppingCart { get; set; }
        public List<CoreSubscriptions> Subscriptions { get; set; } = new List<CoreSubscriptions>();
    }
}
