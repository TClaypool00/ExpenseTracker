using System.Collections.Generic;

namespace ExpenseTracker.Core.CoreModels
{
    public class CoreCreditUnion
    {
        public int UnionId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string CreditUnionName { get; set; }

        public List<CoreLoan> Loan { get; set; } = new List<CoreLoan>();
    }
}
