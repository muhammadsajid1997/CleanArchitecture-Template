﻿using CleanTemplate.Common.Exceptions;
using CleanTemplate.Domain.Entities.Users;
using CleanTemplate.Domain.IRepositories;
using CleanTemplate.Persistance.CommandHandlers.Users;
using CleanTemplate.Persistance.Jwt;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanTemplate.CommandHandler.Test
{
    public class RefreshTokenCommandHandlerTest
    {
        [Fact]
        public async Task Should_ThrowException_When_InputIsNull()
        {
            // Arrange
            var userStore = new Mock<IUserStore<User>>();
            userStore.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
                .ReturnsAsync(new User()
                {
                    UserName = "testUserName",
                    Id = 123
                });

            var userManager = new UserManager<User>(userStore.Object, null, null, null, null, null, null, null, null);
            var jwtService = new Mock<IJwtService>();
            var refreshTokenRepository = new Mock<IRefreshTokenRepository>();

            //Act
            var commadHandler = new RefreshTokenCommandHandler(userManager, jwtService.Object, refreshTokenRepository.Object);

            //Assert
            await Assert.ThrowsAsync<InvalidNullInputException>(()=>commadHandler.Handle(null,CancellationToken.None));
        }
    }
}
