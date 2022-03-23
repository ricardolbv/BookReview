using BookReview.Application.Contracts.Persistence;
using BookReview.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.UnitTests.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IReviewRepository> GetMockReviewRepository()
        {
            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = 1,
                    Text = "Text it's gonna Be greaternfe ok jekejfkefmkefkef"
                },

                new Review()
                {
                    Id = 2,
                    Text = "Texttsgrgçrekglwekgpo lekflekl fefmemklefm mmmmm"
                },

                new Review()
                {
                    Id = 3,
                    Text = "fefwegpkj efjowejiorjir wpoefkwepokfpoew k"
                }
            };

            var mockReviewRepository = new Mock<IReviewRepository>();

            mockReviewRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(reviews);

            mockReviewRepository.Setup(repo => repo.AddAsync(It.IsAny<Review>())).ReturnsAsync(
                    (Review review) =>
                    {
                        reviews.Add(review);
                        return review;
                    }
                );

            mockReviewRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(
                (int id) => reviews.Where(r => r.Id == id).Single());

            mockReviewRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Review>())).Callback(
                (Review review) => reviews.RemoveAll(r => r.Id == review.Id));

            return mockReviewRepository;
        }
    }
}
