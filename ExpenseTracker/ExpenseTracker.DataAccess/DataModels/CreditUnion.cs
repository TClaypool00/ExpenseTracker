using System.Collections.Generic;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class CreditUnion
    {
        public CreditUnion()
        {
            Loan = new HashSet<Loan>();
        }

        public int UnionId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string CreditUnionName { get; set; }

        public virtual ICollection<Loan> Loan { get; set; }
    }
}
