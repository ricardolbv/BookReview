using BookReview.Domain.Events;
using MediatR;
using System;


namespace BookReview.Application.Features.Rating.Commands.CreateRating
{
    public class CreateRatingCommand : RatingEvent, IRequest<CreateRatingCommandResponse>
    {
    }
}
