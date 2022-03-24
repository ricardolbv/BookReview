using BookReview.Domain.Enums;

namespace BookReview.Application.Features.Reviews.Commands.UpdateReview
{
    public class ReviewUpdateDto
    {
        public string Text { get; set; }
        public ReviewState State { get; set; }
    }
}