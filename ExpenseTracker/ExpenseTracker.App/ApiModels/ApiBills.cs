using System;

namespace ExpenseTracker.App.ApiModels
{
    public class ApiBills
    {
        public int BillId { get; set; }
        public string BillName { get; set; }
        public DateTime DueDate { get; set; }
        public bool BillLate { get; set; }
        public decimal BillPrice { get; set; }

        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}