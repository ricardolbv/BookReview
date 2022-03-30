using BookReview.Application.Features.Reviews.Commands.UpdateReview;
using BookReview.Application.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewReponse : BaseResponse
    {
        public ReviewUpdateDto Review { get; set; }
    }
}
