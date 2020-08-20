namespace ExpenseTracker.App.ApiModels
{
    public class ApiBudget
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmtBills { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}