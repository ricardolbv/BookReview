using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Application.Exceptions;
using BookReview.Domain.Entities;
using MediatR;
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

        public CreateReviewCommandHandler(IMapper mapper, IReviewRepository reviewRepo)
        {
            _mapper = mapper;
            _reviewRepo = reviewRepo;
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
            }

            if (response.Success)
            {
                var _review = _mapper.Map<Review>(request);
                await _reviewRepo.AddAsync(_review);
                response.Review = _mapper.Map<CreateReviewDto>(_review);
                response.Message = "Review Created";
            }
            
            return response;
        }
    }
}
