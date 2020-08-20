namespace ExpenseTracker.Core.CoreModels
{
    public class CoreBudget
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmtBills { get; set; }

        public CoreUsers User { get; set; }
    }
}
