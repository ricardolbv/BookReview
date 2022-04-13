using AutoMapper;
using BookReview.Application.Contracts.Persistence;
using BookReview.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Features.Reviews.Queries.GetAllReviews
{
    public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, List<GetAllReviewsDto>>
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllReviewsQueryHandler> _logger;

        public GetAllReviewsQueryHandler(IReviewRepository reviewRepo, IMapper mapper, ILogger<GetAllReviewsQueryHandler> logger)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<GetAllReviewsDto>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepo.ListAllAsync();

            _logger.LogInformation($"Listing all reviews {reviews}");

            return reviews.Select(review => _mapper.Map<GetAllReviewsDto>(review)).ToList();
        }
    }
}
