using BookReview.Domain.Common;
using BookReview.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Domain.Entities
{
    public class Review : AuditableEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ReviewState State { get; set; }

    }
}
