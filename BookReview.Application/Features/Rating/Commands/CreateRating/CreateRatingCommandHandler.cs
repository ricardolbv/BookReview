using BookReview.Application.Contracts.Infraestructure;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Rating.Commands.CreateRating
{
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, CreateRatingCommandResponse>
    {
        private readonly IMessageBus _bus;
        private readonly IReviewRepository _reviewRepo;
        private readonly ILogger<CreateRatingCommandHandler> _logger;

        public CreateRatingCommandHandler(IMessageBus bus, IReviewRepository reviewRepo, ILogger<CreateRatingCommandHandler> logger)
        {
            _bus = bus;
            _reviewRepo = reviewRepo;
            _logger = logger;
        }

        public async Task<CreateRatingCommandResponse> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateRatingCommandResponse();

            try
            {
                await _bus.PublishMessage(request, EnvVariables.TOPIC_NEW_RATING);
                _logger.LogInformation($"Publish {request} into topic: {EnvVariables.TOPIC_NEW_RATING}");
                response.Message = "Message publishied";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                throw new BadRequestException(ex.Message);
            }

            return response;
        }
    }
}
