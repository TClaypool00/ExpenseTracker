using System;

namespace ExpenseTracker.App.ApiModels
{
    public class ApiLoan
    {
        public int LoanId { get; set; }
        public int UnionId { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public decimal MonthlyAmountDue { get; set; }
        public decimal Deposit { get; set; }
        public decimal TotalAmountDue { get; set; }

        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public string CreditUnionName { get; set; }
    }
}
