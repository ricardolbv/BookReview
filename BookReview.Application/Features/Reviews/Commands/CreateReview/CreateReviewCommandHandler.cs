using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Exceptions;
using BookReview.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreateReviewCommandResponse>
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateReviewCommandHandler> _logger;

        public CreateReviewCommandHandler(IMapper mapper, IReviewRepository reviewRepo, ILogger<CreateReviewCommandHandler> logger)
        {
            _mapper = mapper;
            _reviewRepo = reviewRepo;
            _logger = logger;
        }

        public async Task<CreateReviewCommandResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateReviewCommandResponse();   
            var validator = new CreateReviewCommandValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.Errors = new List<string>();
                response.Message = "Review with some validation errors!";

                foreach (var error in validationResult.Errors)
                {
                    response.Errors.Add(error.ErrorMessage);
                }

                _logger.LogError($"Could not create the review due the problems {response.Errors}");
            }

            if (response.Success)
            {
                var _review = _mapper.Map<Review>(request);
                await _reviewRepo.AddAsync(_review);
                response.Review = _mapper.Map<CreateReviewDto>(_review);
                response.Message = "Review Created";

                _logger.LogInformation($"Creating the review with the infos: {_review}");
            }
            
            return response;
        }
    }
}
