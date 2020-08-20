namespace ExpenseTracker.App.ApiModels
{
    public class ApiCreditUnion
    {
        public int UnionId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string CreditUnionName { get; set; }
    }
}