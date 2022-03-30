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
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, UpdateReviewReponse>
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        public async Task<UpdateReviewReponse> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateReviewReponse();
            var validation = new UpdateReviewCommandValidator();
            var result = await validation.ValidateAsync(request);

            if(result.Errors.Count > 0)
            {
                response.Errors = new List<string>();
                response.Success = false;
                response.Message = "Have some errors while updating review";

                foreach(var error in result.Errors)
                {
                    response.Errors.Add(error.ErrorMessage);
                }
            }

            var _review = await _reviewRepo.GetByIdAsync(request.Id);
            if(_review == null)
            {
                throw new NotFoundException(nameof(Review), request.Id);
            }

            if (response.Success)
            {
                _mapper.Map(request, _review, typeof(UpdateReviewCommand), typeof(Review));
                var _response = await _reviewRepo.UpdateAsync(_review);
                response.Review = _mapper.Map<ReviewUpdateDto>(_response);
                response.Message = "Review updated";
            }

            return response;
        }
    }
}
