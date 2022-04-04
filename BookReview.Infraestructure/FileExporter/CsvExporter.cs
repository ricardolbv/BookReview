using BookReview.Application.Contracts.Infraestructure;
using BookReview.Application.Features.Reviews.Queries.ExportAllReviews;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Infraestructure.FileExporter
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportReviewsToCsv(List<ExportReviewDto> exportReviewsDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, null);
                csvWriter.WriteRecords(exportReviewsDtos);
            }

            return memoryStream.ToArray();
        }
    }
}
