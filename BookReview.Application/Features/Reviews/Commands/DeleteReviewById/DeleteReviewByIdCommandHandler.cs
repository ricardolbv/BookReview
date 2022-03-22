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

namespace BookReview.Application.Features.Reviews.Commands.DeleteReviewById
{
    public class DeleteReviewByIdCommandHandler : IRequestHandler<DeleteReviewByIdCommand, int>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public DeleteReviewByIdCommandHandler(IMapper mapper, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        public async Task<int> Handle(DeleteReviewByIdCommand request, CancellationToken cancellationToken)
        {
            var validator =new DeleteReviewByIdValidator();
            var result = await validator.ValidateAsync(request);

            if (result.Errors.Count > 0)
                throw new ValidationException(result);

            var _review = await _reviewRepository.GetByIdAsync(request.Id);
            //More validation for response

            await _reviewRepository.DeleteAsync(_review);
            return 1;
        }
    }
}
