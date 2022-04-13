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

namespace BookReview.Application.Features.Reviews.Commands.DeleteReviewById
{
    public class DeleteReviewByIdCommandHandler : IRequestHandler<DeleteReviewByIdCommand, DeleteReviewByIdResponse>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteReviewByIdCommandHandler> _logger;

        public DeleteReviewByIdCommandHandler(IMapper mapper, IReviewRepository reviewRepository, ILogger<DeleteReviewByIdCommandHandler> logger)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public async Task<DeleteReviewByIdResponse> Handle(DeleteReviewByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteReviewByIdResponse(); 
            var validator = new DeleteReviewByIdValidator();
            var result = await validator.ValidateAsync(request);

            if (result.Errors.Count > 0) 
            {
                response.Success = false;
                response.Message = "Some errors while deleting was found";
                response.Errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    response.Errors.Add(error.ErrorMessage);
                }

                _logger.LogError($"Could not delete the review, errors: {response.Errors}");
            }

            var _review = await _reviewRepository.GetByIdAsync(request.Id);
            if(_review == null)
            {
                _logger.LogInformation($"Could not found the following review id {request.Id}");

                throw new NotFoundException(nameof(Review), request.Id);
            }

            if (response.Success)
            {
                try
                {
                    await _reviewRepository.DeleteAsync(_review);
                    response.Message = "Review deleted with success";

                    _logger.LogInformation($"Review with the id {request.Id} was deleted");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Could not delete the review {request.Id} due the error {ex.Message}");

                    throw new BadRequestException(ex.Message);
                }
            }

            return response;
        }
    }
}
