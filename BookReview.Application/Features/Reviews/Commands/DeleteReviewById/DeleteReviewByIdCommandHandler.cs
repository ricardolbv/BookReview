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
    public class DeleteReviewByIdCommandHandler : IRequestHandler<DeleteReviewByIdCommand, DeleteReviewByIdResponse>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public DeleteReviewByIdCommandHandler(IMapper mapper, IReviewRepository reviewRepository)
        {
            _mapper = mapper;
            _reviewRepository = reviewRepository;
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
            }

            var _review = await _reviewRepository.GetByIdAsync(request.Id);
            if(_review == null)
            {
                throw new NotFoundException(nameof(Review), request.Id);
            }

            if (response.Success)
            {
                try
                {
                    await _reviewRepository.DeleteAsync(_review);
                    response.Message = "Review deleted with success";
                }
                catch (Exception ex)
                {
                    throw new BadRequestException(ex.Message);
                }
            }

            return response;
        }
    }
}
