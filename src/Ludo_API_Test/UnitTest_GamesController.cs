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
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async void On_DELETE_When_GameExists_Expect_GameDeletedSuccess(int id)
        {
            // Arrange
            List<Gameboard> gameboards = new()
            {
                new() { ID = 1 },
                new() { ID = 2 },
                new() { ID = 3 }
            };

            IGamesRepository gameRepo = new TestGamesRepository
            {
                Gameboards = gameboards,
            };
            GamesController gamesController = new(null, gameRepo);

            // Act
            bool success = await gamesController.Delete(id);

            // Assert
            Assert.True(success);
            Assert.Equal(2, gameboards.Count);
            Assert.DoesNotContain(gameboards, g => g.ID == id);
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

            IGamesRepository gameRepo = new TestGamesRepository
            {
                Gameboards = gameboards,
            };
            GamesController gamesController = new(null, gameRepo);

            // Act
            bool success = await gamesController.Delete(5);

            // Assert
            Assert.False(success);
            Assert.Equal(3, gameboards.Count);
        }

        // Disabled 2021-05-18 12:14
        //[Fact]
        //public async void On_POST_When_RequestBodyListOfPlayerDTO_Expect_PlayersSquaresAndGameboardCreated()
        //{
        //    // Arrange
        //    List<PlayerDTO> playerDTOs = new()
        //    {
        //        //new() { Name = "Player1", Color = Color.Blue.ToArgb() },
        //        new() { Name = "Player1", Color = "#0000ff" },
        //        new() { Name = "Player2", Color = "#ff0000" },
        //        new() { Name = "Player3", Color = "#ffff00" },
        //        new() { Name = "Player4", Color = "#008000" },
        //    };

        //    IGamesRepository gameRepo = new TestGamesRepository();
        //    GamesController gamesController = new(null, gameRepo);

        //    // Act
        //    var actionResult = await gamesController.Post(playerDTOs);
        //    var gameboards = await gameRepo.GetAllGames(null);

        //    // Assert
        //    //Assert.IsType<ActionResult<string>>(actionResult);
        //    Assert.IsType<ActionResult<int>>(actionResult);
        //    var result = Assert.IsType<OkObjectResult>(actionResult.Result);
        //    //Assert.IsType<Guid>(Guid.Parse((string)result.Value));
        //    Assert.Equal(4, gameboards[0].Players.Count);
        //    Assert.Equal("Player4", gameboards[0].Players.Last().Name);
        //}

        // note: this might not be possible in a unit test (has to be an integration test?)
        //[Theory]
        ////[InlineData("Player1", "#0000f", "Invalid color format, value should be a hex color (e.g. #123cef)")]
        //[InlineData("Player1", "0000ff", "Invalid color format, value should be a hex color (e.g. #123cef)")]
        //[InlineData("Player1", "", "Invalid color format, value should be a hex color (e.g. #123cef)")]
        //[InlineData("Player1", "#", "Invalid color format, value should be a hex color (e.g. #123cef)")]
        //[InlineData("Player1", "fff", "Invalid color format, value should be a hex color (e.g. #123cef)")]
        //[InlineData("Player1", null, "Invalid color format, value should be a hex color (e.g. #123cef)")]
        //[InlineData("", "#0000ff", "Name length must be between 1 and 25.")] // todo: regex match instead of hardcoding 'name', '1' and '25'?
        //[InlineData("namenamenamenamenamenamename", "#0000ff", "Name length must be between 1 and 25.")] // todo: regex match instead of hardcoding 'name', '1' and '25'?
        //[InlineData(null, "#0000ff", "Name length must be between 1 and 25.")] // todo: regex match instead of hardcoding 'name', '1' and '25'?
        //public async void On_POST_When_Invalid_Expect_BadRequest(string name, string color, string errorMessage)
        //{
        //    // Arrange
        //    List<PlayerDTO> playerDTOs = new()
        //    {
        //        new() { Name = name, Color = color },
        //    };

        //    IGamesRepository gameRepo = new TestGamesRepository();
        //    GamesController gamesController = new(null, gameRepo);

        //    // Act
        //    var actionResult = await gamesController.Post(playerDTOs);
        //    var gameboards = await gameRepo.GetAllGames(null);
        //    // actionResult = Actionresult<int> where actionResult.Result = BadRequestObjectResult and actionResult.Result.Value == errorMessage

        //    // Assert
        //    var result = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        //    Assert.Equal(errorMessage, (string)result.Value);
        //    //Assert.
        //}
    }
}
