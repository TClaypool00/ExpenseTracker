﻿namespace ExpenseTracker.Core.CoreModels
{
    public class CoreNonBills
    {
        public int NonBillId { get; set; }
        public int UserId { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }

        public CoreUsers User { get; set; }
    }
}