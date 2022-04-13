using AutoMapper;
using BookReview.Application.Contracts.Infraestructure;
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

namespace BookReview.Application.Features.Reviews.Queries.ExportAllReviews
{
    public class ExportReviewQueryHandler : IRequestHandler<ExportReviewQuery, ExportReviewVm>
    {
        private readonly ICsvExporter _csvExporter;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _repo;
        private readonly ILogger<ExportReviewQueryHandler> _logger;

        public ExportReviewQueryHandler(IMapper mapper, ICsvExporter csvExporter, IReviewRepository repo, ILogger<ExportReviewQueryHandler> logger)
        {
            _mapper = mapper;
            _csvExporter = csvExporter;
            _repo = repo;
            _logger = logger;
        }

        public async Task<ExportReviewVm> Handle(ExportReviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _reviews = _mapper.Map<List<ExportReviewDto>>((await _repo.ListAllAsync()));

                var eventsToExport = _csvExporter.ExportReviewsToCsv(_reviews);

                _logger.LogInformation($"Generating the report with the reviews {_reviews}");

                return new ExportReviewVm { Data = eventsToExport, ContentType = "text/csv", FileName = $"{Guid.NewGuid()}.csv"};
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while generating the review report {ex.Message}");

                throw new BadRequestException(ex.Message);
            }
            
        }
    }
}
