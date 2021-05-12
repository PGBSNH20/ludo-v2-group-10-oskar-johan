using Ludo_API.Controllers;
using Ludo_API.Models;
using Ludo_API.Models.DTO;
using Ludo_API.Repositories;
using Ludo_API_Test.TestRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace Ludo_API_Test
{
    public class UnitTest_GamesController
    {
        [Fact]
        public async void On_DELETE_When_GameExists_Expect_GameDeletedSuccess()
        {
            // Arrange
            List<Gameboard> gameboards = new()
            {
                new() { ID = 1 },
                new() { ID = 2 },
                new() { ID = 3 }
            };

            IGamesRepository gameRepo = new TestGamesRepository(gameboards);
            GamesController gamesController = new(null, gameRepo);

            // Act
            bool success = await gamesController.Delete(1);

            // Assert
            Assert.True(success);
            Assert.Equal(2, gameboards.Count);
            Assert.DoesNotContain(gameboards, g => g.ID == 1);
        }

        [Fact]
        public async void On_DELETE_When_GameDoesntExists_Expect_GameDeletedFail()
        {
            // Arrange
            List<Gameboard> gameboards = new()
            {
                new() { ID = 1 },
                new() { ID = 2 },
                new() { ID = 3 }
            };

            IGamesRepository gameRepo = new TestGamesRepository(gameboards);
            GamesController gamesController = new(null, gameRepo);

            // Act
            bool success = await gamesController.Delete(5);

            // Assert
            Assert.False(success);
            Assert.Equal(3, gameboards.Count);
        }

        [Fact]
        public async void On_POST_When_RequestBodyListOfPlayerDTO_Expect_PlayersSquaresAndGameboardCreated()
        {
            // Arrange
            List<PlayerDTO> playerDTOs = new()
            {
                new() { Name = "Player1", Color = Color.Blue.ToArgb() },
                new() { Name = "Player2", Color = Color.Red.ToArgb() },
                new() { Name = "Player2", Color = Color.Yellow.ToArgb() },
                new() { Name = "Player2", Color = Color.Green.ToArgb() },
            };

            IGamesRepository gameRepo = new TestGamesRepository();
            GamesController gamesController = new(null, gameRepo);

            // Act
            var actionResult = await gamesController.Post(playerDTOs);

            // Assert
            Assert.IsType<ActionResult<string>>(actionResult);
            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.IsType<Guid>(Guid.Parse((string)result.Value));
        }
    }
}
