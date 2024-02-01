using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Tests.Repositories
{
    public class ReviewFeedbackRepository_Tests
    {
        private readonly CustomerContext _context;

        public ReviewFeedbackRepository_Tests()
        {
            _context = new CustomerContext(new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_ReviewFeedbackEntity_To_Context_And_SaveChanges()
        {
            // Arrange
            var repo = new ReviewFeedbackRepository(_context);
            var entity = new ReviewFeedbackEntity
            {
                UserId = 1,
                Feedback = "Sample Feedback"
            };

            // Act
            var result = await repo.CreateAsync(entity);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result, _context.ReviewFeedbacks);
        }

        [Fact]
        public async Task GetAsync_Should_Retrieve_ReviewFeedbackEntity_From_Context()
        {
            // Arrange
            var repo = new ReviewFeedbackRepository(_context);
            var entity = new ReviewFeedbackEntity
            {
                UserId = 2,
                Feedback = "Sample Feedback 2"
            };

            _context.ReviewFeedbacks.Add(entity);
            await _context.SaveChangesAsync();

            // Act
            var result = await repo.GetAsync(x => x.UserId == 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Sample Feedback 2", result.Feedback);
        }
    }
}
