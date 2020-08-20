using System;

namespace ExpenseTracker.Core.CoreModels
{
    public class CoreSubscriptions
    {
        public int SubId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }

        public CoreUsers User { get; set; }
    }
}