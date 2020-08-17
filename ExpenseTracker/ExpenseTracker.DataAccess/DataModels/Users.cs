using System.Collections.Generic;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Users
    {
        public Users()
        {
            Bills = new HashSet<Bills>();
            Budget = new HashSet<Budget>();
            Comments = new HashSet<Comments>();
            Loan = new HashSet<Loan>();
            NonBills = new HashSet<NonBills>();
            Reply = new HashSet<Reply>();
            Shoes = new HashSet<Shoes>();
            ShoppingCart = new HashSet<ShoppingCart>();
            Subscriptions = new HashSet<Subscriptions>();
        }

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

        public virtual ICollection<Bills> Bills { get; set; }
        public virtual ICollection<Budget> Budget { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Loan> Loan { get; set; }
        public virtual ICollection<NonBills> NonBills { get; set; }
        public virtual ICollection<Reply> Reply { get; set; }
        public virtual ICollection<Shoes> Shoes { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }
        public virtual ICollection<Subscriptions> Subscriptions { get; set; }
    }
}
