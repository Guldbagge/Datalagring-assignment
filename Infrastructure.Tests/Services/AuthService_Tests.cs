

using Business.Services;
using Business.Interfaces;
using Infrastructure.Entities;
using Shared.Dtos;
using Moq;
using Xunit;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using System.Linq.Expressions;

namespace Infrastructure.Tests.Services;

    public class AuthService_Tests
    {
    
        private readonly AuthService _authService;
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IAuthRepository> _authRepositoryMock = new Mock<IAuthRepository>();
        private readonly Mock<IRoleRepository> _roleRepositoryMock = new Mock<IRoleRepository>();

        public AuthService_Tests()
        {
            _authService = new AuthService(_userRepositoryMock.Object, _authRepositoryMock.Object, _roleRepositoryMock.Object);
        }

        [Fact]
        public async Task SignUpAsync_ShouldReturnTrue_WhenUserDoesNotExist()
        {
            // Arrange
            var signUpDto = new SignUpDto { Email = "test@example.com", Password = "password" };
            _userRepositoryMock.Setup(x => x.ExistsAsync(It.IsAny<Expression<Func<UserEntity, bool>>>())).ReturnsAsync(false);
            _userRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<UserEntity>(), It.IsAny<string>())).ReturnsAsync(new UserEntity { Id = 1 });
            _authRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<AuthEntity>(), It.IsAny<string>())).ReturnsAsync(new AuthEntity());
            _roleRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<RoleEntity, bool>>>())).ReturnsAsync(new RoleEntity { Id = 1 });

            // Act
            var result = await _authService.SignUpAsync(signUpDto);

            // Assert
            Assert.True(result);
        }
    }


