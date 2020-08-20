using System;

namespace ExpenseTracker.App.ApiModels
{
    public class ApiSubscriptions
    {
        public int SubId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public DateTime DueDate { get; set; }
        public decimal AmountDue { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}