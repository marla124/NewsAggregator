using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregatingProject.Models
{
    internal class CommentModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateAndTime { get; set; }
        public Guid NewsId { get; set; }
        public Guid? ParentCommentId { get; set; }
    }
}
