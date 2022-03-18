using BookReview.Application.Contracts.Persistence;
using BookReview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Persistence.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(BookReviewDbContext dbContext) : base(dbContext)
        {
        }
    }
}
