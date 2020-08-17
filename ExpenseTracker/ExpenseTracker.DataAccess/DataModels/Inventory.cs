namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int StoreId { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }

        public virtual Foods Food { get; set; }
        public virtual Store Store { get; set; }
    }
}
