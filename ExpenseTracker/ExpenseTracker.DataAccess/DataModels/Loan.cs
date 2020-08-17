using System;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Loan
    {
        public int LoanId { get; set; }
        public int UnionId { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public decimal MonthlyAmountDue { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalAmountDue { get; set; }
        public int UserId { get; set; }

        public virtual CreditUnion Union { get; set; }
        public virtual Users User { get; set; }
    }
}
