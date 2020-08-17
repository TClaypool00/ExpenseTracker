namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class NonBills
    {
        public int NonBillId { get; set; }
        public int UserId { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }

        public virtual Users User { get; set; }
    }
}
