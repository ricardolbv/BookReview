using BookReview.Application.Features.Reviews.Queries.ExportAllReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Contracts.Infraestructure
{
    public interface ICsvExporter
    {
        byte[] ExportReviewsToCsv(List<ExportReviewDto> exportReviewsDtos);
    }
}
