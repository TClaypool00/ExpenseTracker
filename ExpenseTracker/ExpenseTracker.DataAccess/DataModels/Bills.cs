using System;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Bills
    {
        public int BillId { get; set; }
        public string BillName { get; set; }
        public DateTime DueDate { get; set; }
        public bool BillLate { get; set; }
        public decimal BillPrice { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
