using System;

namespace ExpenseTracker.Core.CoreModels
{
    public class CoreBills
    {
        public int BillId { get; set; }
        public string BillName { get; set; }
        public DateTime DueDate { get; set; }
        public bool BillLate { get; set; }
        public decimal BillPrice { get; set; }
        public int UserId { get; set; }

        public CoreUsers User { get; set; }
    }
}
