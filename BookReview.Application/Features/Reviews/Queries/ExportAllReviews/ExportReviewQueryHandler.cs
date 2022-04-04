using AutoMapper;
using BookReview.Application.Contracts.Infraestructure;
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

namespace BookReview.Application.Features.Reviews.Queries.ExportAllReviews
{
    public class ExportReviewQueryHandler : IRequestHandler<ExportReviewQuery, ExportReviewVm>
    {
        private readonly ICsvExporter _csvExporter;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _repo;

        public ExportReviewQueryHandler(IMapper mapper, ICsvExporter csvExporter, IReviewRepository repo)
        {
            _mapper = mapper;
            _csvExporter = csvExporter;
            _repo = repo;
        }

        public async Task<ExportReviewVm> Handle(ExportReviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var _reviews = _mapper.Map<List<ExportReviewDto>>((await _repo.ListAllAsync()));

                var eventsToExport = _csvExporter.ExportReviewsToCsv(_reviews);

                return new ExportReviewVm { Data = eventsToExport, ContentType = "text/csv", FileName = $"{Guid.NewGuid()}.csv"};
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
            
        }
    }
}
