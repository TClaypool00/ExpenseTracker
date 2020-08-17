using System;

namespace ExpenseTracker.DataAccess.DataModels
{
    public partial class Reply
    {
        public int ReplyId { get; set; }
        public string ReplyBody { get; set; }
        public DateTime ReplyDate { get; set; }
        public int? ReplyUserId { get; set; }
        public int? CommentId { get; set; }

        public virtual Comments Comment { get; set; }
        public virtual Users ReplyUser { get; set; }
    }
}
