namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Budget
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmtBills { get; set; }

        public virtual Users User { get; set; }
    }
}
