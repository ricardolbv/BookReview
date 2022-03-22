using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Commands.DeleteReviewById
{
    public class DeleteReviewByIdValidator : AbstractValidator<DeleteReviewByIdCommand>
    {
        public DeleteReviewByIdValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty().WithMessage("Must have a ID for deleting")
                .GreaterThan(0).WithMessage("Must have a valid");
        }
    }
}
