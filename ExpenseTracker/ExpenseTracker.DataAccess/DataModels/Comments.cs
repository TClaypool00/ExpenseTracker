using System;
using System.Collections.Generic;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Comments
    {
        public Comments()
        {
            Reply = new HashSet<Reply>();
        }

        public int CommentId { get; set; }
        public string MessageHead { get; set; }
        public string MessageBody { get; set; }
        public DateTime CommentDate { get; set; }
        public int? ShoeId { get; set; }
        public int IsApproved { get; set; }
        public int? BuyerId { get; set; }

        public virtual Users Buyer { get; set; }
        public virtual Shoes Shoe { get; set; }
        public virtual ICollection<Reply> Reply { get; set; }
    }
}
