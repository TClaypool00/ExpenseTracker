namespace ExpenseTracker.App.ApiModels
{
    public class ApiNonBills
    {
        public int NonBillId { get; set; }
        public int UserId { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}