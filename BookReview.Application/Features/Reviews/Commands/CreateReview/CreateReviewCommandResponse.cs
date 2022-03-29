using BookReview.Application.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandResponse : BaseResponse
    {
        public CreateReviewDto Review { get; set; }
    }
}
