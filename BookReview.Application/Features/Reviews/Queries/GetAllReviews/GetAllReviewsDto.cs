using BookReview.Domain.Enums;

namespace BookReview.Application.Features.Reviews.Queries.GetAllReviews
{
    public class GetAllReviewsDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ReviewState State { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Text: {Text}, State: {State}";
        }
    }
}