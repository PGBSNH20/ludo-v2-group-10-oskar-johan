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
using Xunit.Abstractions;

namespace Ludo_API_Test.UnitTests
{
    public class UnitTest_GameplayController
    {
        private readonly ITestOutputHelper _output;

        public UnitTest_GameplayController(ITestOutputHelper output)
        {
            _output = output;
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dieRoll">The die roll.</param>
        /// <param name="startSquareIndex">The index of the square the player has a piece on.</param>
        /// <param name="playerId">The index in `players`(as declared in the test below) of the player we want run the test for.</param>
        /// <returns></returns>
        //[Fact]
        [Theory]
        [InlineData(2, 6, 0, 8)]
        [InlineData(3, 6, 0, 9)]
        [InlineData(4, 6, 0, 10)]
        [InlineData(5, 6, 0, 11)]
        [InlineData(2, 6, 1, 8)]
        [InlineData(3, 6, 1, 9)]
        [InlineData(4, 6, 1, 45)]
        [InlineData(5, 6, 1, 46)]
        public async Task On_PostCastDie__When_Roll_2_3_4_5_And_And_Pieces_On_Gameboard__Expect_Actions(
            int dieRoll,
            int startSquareIndex,
            int playerId,
            int expectedDestSquareIndex
        )
        {
            // Arrange
            //// Arrange repositories and mocks and "mock" data:
            Gameboard.CreateTracks();

            int gameboardId = 1;

            var players = new List<Player>
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

            var squareTentant = new SquareTenant
            {
                ID = 1,
                Player = players[playerId],
                PieceCount = 1,
                SquareIndex = startSquareIndex,
            };
            gameboards[0].Squares[startSquareIndex].Tenant = squareTentant;

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
                PlayerId = gameboards[0].Players[playerId].ID,
            };

            //// Expected Destination SquareIndex:
            //// The red player's track begins at square index 10, and since the gameboard is a loop, the square indices of the "public"/shared squares go from 0-39, the red player has to go from 10-39, and then 0-9 to complete a lap of the gameboard. Thus we first have to find the the start square ("public/shared squares) in the player's track ...
            //int indexOftartSquareInPlayerTrack = gameboards[0].Players[playerId].Track.IndexOf(startSquareIndex);
            //// ... and add the number of squares the piece can move to this index, and then use this new index to get the actual index of the square from the player's track.
            //int expectedDestSquareIndex = gameboards[0].Players[playerId].Track[indexOftartSquareInPlayerTrack + dieRoll];

            // Act
            var moveActions_ActionResult = await gameplayController.PostRollDie(postRollDieDTO);
            var moveActions_OkObjectResult = (OkObjectResult)(moveActions_ActionResult).Result;
            var moveActions = (List<MoveAction>)moveActions_OkObjectResult.Value;

            // Assert
            Assert.Collection(moveActions, item => Assert.Equal(expectedDestSquareIndex, item.DestinationSquare.SquareIndex) );
        }
    }
}
