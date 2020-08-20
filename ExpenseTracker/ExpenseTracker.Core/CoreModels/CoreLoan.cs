using System;

namespace ExpenseTracker.Core.CoreModels
{
    public class CoreLoan
    {
        public int LoanId { get; set; }
        public int UnionId { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public decimal MonthlyAmountDue { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalAmountDue { get; set; }
        public int UserId { get; set; }

        public CoreCreditUnion Union { get; set; }
        public CoreUsers User { get; set; }
    }
}
