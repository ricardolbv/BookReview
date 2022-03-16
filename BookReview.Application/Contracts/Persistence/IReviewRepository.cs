using BookReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Contracts.Persistence
{
    public interface IReviewRepository : IAsyncRepository<Review>
    {
    }
}
