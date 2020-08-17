using System;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Subscriptions
    {
        public int SubId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }

        public virtual Users User { get; set; }
    }
}
