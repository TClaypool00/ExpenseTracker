using System.Collections.Generic;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Shoes
    {
        public Shoes()
        {
            Comments = new HashSet<Comments>();
        }

        public int ShoeId { get; set; }
        public bool BothShoes { get; set; }
        public string Manufacter { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public decimal? RightSize { get; set; }
        public decimal? LeftSize { get; set; }
        public int? UserId { get; set; }
        public bool Gender { get; set; }
        public string Description { get; set; }
        public bool IsSold { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
