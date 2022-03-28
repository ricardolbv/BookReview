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

namespace BookReview.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewUpdateDto>
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        public async Task<ReviewUpdateDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var validation = new UpdateReviewCommandValidator();
            var result = await validation.ValidateAsync(request);

            if(result.Errors.Count > 0)
            {
                throw new ValidationException(result);
            }

            var _review = await _reviewRepo.GetByIdAsync(request.Id);
            if(_review == null)
            {
                throw new NotFoundException(nameof(Review), request.Id);
            }

            _mapper.Map(request, _review, typeof(UpdateReviewCommand), typeof(Review));

            var response = await _reviewRepo.UpdateAsync(_review);

            return _mapper.Map<ReviewUpdateDto>(response);
        }
    }
}
