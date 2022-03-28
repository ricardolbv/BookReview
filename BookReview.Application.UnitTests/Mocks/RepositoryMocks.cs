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
                (int id) => reviews.FirstOrDefault(r => r.Id == id));

            mockReviewRepository.Setup(repo => repo.DeleteAsync(It.IsAny<Review>())).Callback(
                (Review review) => reviews.RemoveAll(r => r.Id == review.Id));

            mockReviewRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Review>())).ReturnsAsync(
                (Review review) =>
                {
                    if (review == null)
                    {
                        return null;
                    }

                    Review _review = (Review) reviews.FirstOrDefault(r => r.Id == review.Id);
                    if (_review == null)
                    {
                        return null;
                    }

                    _review.State = review.State;
                    _review.Text = review.Text;

                    return _review;
                });


            return mockReviewRepository;
        }
    }
}
