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

namespace BookReview.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, UpdateReviewReponse>
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateReviewCommandHandler> _logger;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepo, IMapper mapper, ILogger<UpdateReviewCommandHandler> logger)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<UpdateReviewReponse> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateReviewReponse();
            var validation = new UpdateReviewCommandValidator();
            var result = await validation.ValidateAsync(request);

            try
            {
                if (result.Errors.Count > 0)
                {
                    response.Errors = new List<string>();
                    response.Success = false;
                    response.Message = "Have some errors while updating review";

                    foreach (var error in result.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }

                    _logger.LogInformation($"Could not update the review: {request.Id} due the validation errors: {response.Errors}");
                }

                var _review = await _reviewRepo.GetByIdAsync(request.Id);
                if (_review == null)
                {
                    _logger.LogInformation($"Could not found the review: {request.Id} for performing update");

                    throw new NotFoundException(nameof(Review), request.Id);
                }

                if (response.Success)
                {
                    _mapper.Map(request, _review, typeof(UpdateReviewCommand), typeof(Review));
                    var _response = await _reviewRepo.UpdateAsync(_review);
                    response.Review = _mapper.Map<ReviewUpdateDto>(_response);
                    response.Message = "Review updated";

                    _logger.LogInformation($"Review id: {request.Id} was updated");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Some error while updating id: {request.Id} :{ex.Message}");
                throw new BadRequestException(ex.Message);
            }
            

            return response;
        }
    }
}
