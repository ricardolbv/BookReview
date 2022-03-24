using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommand : IRequest<ReviewUpdateDto>
    {
        public int Id { get; set; } 
        public string Text { get; set; }    
    }
}
