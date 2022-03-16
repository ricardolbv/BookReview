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
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Review>
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;

        public CreateReviewCommandHandler(IMapper mapper, IReviewRepository reviewRepo)
        {
            _mapper = mapper;
            _reviewRepo = reviewRepo;
        }

        public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateReviewCommandValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var _review = _mapper.Map<Review>(request);
            
            return await _reviewRepo.AddAsync(_review);
        }
    }
}
