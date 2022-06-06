using BookReview.Domain.Common;

namespace BookReview.Domain.Events
{
    public class RatingEvent : BaseMessageBus
    {
        public int ReviewId  { get; set; }
        public string RatingMessage { get; set; }
        public int RatingStars { get; set; }

    }
}
