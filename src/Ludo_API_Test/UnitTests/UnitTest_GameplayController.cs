using Ludo_API.Controllers;
using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
using Ludo_API.Models.DTO;
using Ludo_API.Repositories;
using Ludo_API_Test.TestRepositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ludo_API_Test.UnitTests
{
    public class UnitTest_GameplayController
    {
        //[Fact]
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task On_PostCastDie__When_Roll_2_3_4_5_And_No_Pieces_On_Gameboard__Expect_NoMoveActions(
            int dieRoll
        )
        {
            // Arrange
            //// Arrange repositories and mocks and "mock" data:
            Gameboard.CreateTracks();

            int gameboardId = 1;

            var players = new List<Player>()
            {
                new Player { ID = 1, Color = "Yellow" },
                new Player { ID = 2, Color = "Red" },
            };
            players.ForEach(p => p.SetTrack());

            var gameboards = new List<Gameboard>
            {
                new Gameboard(players),
            };
            gameboards[0].ID = gameboardId;

            IGamesRepository gamesRepo = new TestGamesRepository
            {
                Gameboards = gameboards,
            };
            IMoveActionsRepository moveActionsRepo = new TestMoveActionsRepository();

            var dieMock = new Mock<IDie>();
            dieMock.Setup(d => d.RollDie()).Returns(dieRoll);

            //// Arrange real classes:
            Game game = new();
            ITurnManager turnManager = new TurnManager(null, gamesRepo, game, dieMock.Object);
            GameplayController gameplayController = new(null, gamesRepo, moveActionsRepo, turnManager);

            //// Arrange the data we act upon:
            var postRollDieDTO = new PostRollDieDTO
            {
                GameId = gameboardId,
                PlayerId = gameboards[0].Players[1].ID,
            };

            // Act
            var moveActions_ActionResult = await gameplayController.PostRollDie(postRollDieDTO);
            var moveActions_OkObjectResult = (OkObjectResult)(moveActions_ActionResult).Result;
            var moveActions = (List<MoveAction>)moveActions_OkObjectResult.Value;

            // Assert
            Assert.Empty(moveActions);
        }
    }
}
