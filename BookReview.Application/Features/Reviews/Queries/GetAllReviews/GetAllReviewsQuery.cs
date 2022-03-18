using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Queries.GetAllReviews
{
    public class GetAllReviewsQuery : IRequest<List<GetAllReviewsDto>>
    {
    }
}
