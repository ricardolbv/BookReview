using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookReview.Domain.Entities;

namespace BookReview.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand : IRequest<Review>
    {
        public string Text { get; set; }
    }
}
