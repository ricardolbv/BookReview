using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Queries.ExportAllReviews
{
    public class ExportReviewVm
    {
        public byte [] Data { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
