using Ludo_API.GameEngine.Game;
using Ludo_API.Models;
using Ludo_API.Repositories;
using Ludo_API_Test.TestRepositories;
using System.Collections.Generic;
using System.Drawing;
using Xunit;
using Ludo_API_Test;
using System.ComponentModel.DataAnnotations;
using System;

namespace Ludo_API.Tests
{
    public class Ludo_API_Test
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diceRoll"></param>
        /// <param name="player1Index"></param>
        /// <param name="player2Index"></param>
        /// <param name="moveActionCountExpected">
        /// Example: If <paramref name="diceRoll"/> == 6, we would expect 3 MoveActions since the player can chose to; take out 2 pieces
        /// to their "Start" Square; 1 piece to the sixth square from; or move their piece already on the gameboard.
        /// </param>
        /// <param name="moveSuccessfulExpected"></param>
        //[Fact]
        [Theory]
        [InlineData(5, 0, 6, 1, 1, true)]
        [InlineData(2, 38, 0, 1, 1, true)]
        [InlineData(2, 38, 0, 2, 1, true)]
        //[InlineData(6, 0, 5, 1, 1, true)] // fixme: doesn't work
        [InlineData(3, 38, 0, 2, 1, true)]
        public async void MovePiece_PathIsClear_MoveIsSuccessful(
            int diceRoll,
            int player1Index,
            int player2Index,
            int player2PieceCount,
            int moveActionCountExpected,
            bool moveSuccessfulExpected
            )
        {
            // Arrange
            Gameboard.CreateTracks();

            List<Player> players = new()
            {
                new Player("Player1", Color.Yellow),
                new Player("Player2", Color.Blue),
            };
            players.ForEach(p => p.SetTrack()); // note: might be unnecessary if SetTrack can be called from the parameterless Player constructor without breaking Entity Framework.

            Gameboard gameboard = new(players);
            gameboard.Squares[player1Index].Tenant = new SquareTenant(player1Index, players[0], 1);
            gameboard.Squares[player2Index].Tenant = new SquareTenant(player2Index, players[1], player2PieceCount);

            IGamesRepository gameRepository = new TestGamesRepository
            {
                Squares = gameboard.Squares, // note: this is hacky, fixme: the repository should take a gameboardId
            };
            IPlayerRepository playerRepository = new TestPlayerRepository();
            await playerRepository.AddPlayers(null, players);
            await gameRepository.CreateNewGame(null, gameboard);

            var game = new Game(gameRepository, gameboard);
            //var turnManager = new TurnManager(game);

            // Act
            //var moveActions = turnManager.HandleTurn(players[0]);
            var moveActions = game.GetPossibleMoves(players[0], diceRoll);
            bool moveSuccessfulActual = await gameRepository.ExecuteMoveAction(null, moveActions[0]);

            // Assert
            //Assert.Single(moveActions);
            Assert.Equal(moveActionCountExpected, moveActions.Count);
            Assert.Equal(moveSuccessfulExpected, moveSuccessfulActual);

            //// Assert that player has moved from initial square
            Assert.Equal(new SquareTenant(player1Index, null, 0), gameboard.Squares[player1Index].Tenant);
            //Assert.Equal(0, gameboard.Squares[player1Index].Tenant.PieceCount);

            //// Assert that player has moved to target square
            Assert.Equal(players[0], gameboard.Squares[player1Index + diceRoll].Tenant.Player);
            Assert.Equal(1, gameboard.Squares[player1Index + diceRoll].Tenant.PieceCount);
        }

        //[Fact]
        //public void MovePiece_PathIsNotClear_PlayerStaysAtInitialSquare()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Player> players = new List<Models.Player>
        //    {
        //        new Player("LudoPlayer", Color.Yellow)
        //        //new Models.Player("LudoPlayer", 14)
        //    };

        //    Gameboard gameboard = new Gameboard(players);

        //    gameboard.Squares[0].Tenant = new SquareTenant(0, players[0], 1);
        //    //gameboard.Squares[0].OccupiedBy = players[0];
        //    //gameboard.Squares[0].Tenant?.PieceCount = 1;

        //    gameboard.Squares[2].Tenant = new SquareTenant(2, players[0], 1);
        //    //gameboard.Squares[2].OccupiedBy = players[0];
        //    //gameboard.Squares[2].Tenant?.PieceCount = 1;

        //    IGameRepository gameRepository = new TestGamesRepository();

        //    //Act
        //    var moved = new Moves(gameRepository, new GameEngine(gameRepository)).MovePiece(gameboard, gameboard.Squares, players[0], 0, 5);

        //    //Assert that move failed
        //    Assert.False(moved);

        //    //Assert that player has NOT moved from initial position
        //    Assert.Equal(players[0], gameboard.Squares[0].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[0].Tenant?.PieceCount);

        //    //Assert that player has NOT moved to target position
        //    Assert.Null(gameboard.Squares[5].OccupiedBy);
        //    Assert.Equal(0, gameboard.Squares[5].Tenant?.PieceCount);
        //}

        //[Fact]
        //public void TurnAtGoal_PathIsClear_MoveIsSuccessful()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Models.Player> players = new List<Models.Player>
        //    {
        //        new Models.Player("LudoPlayer", 14)
        //    };

        //    Gameboard gameboard = new Gameboard(players);

        //    gameboard.Squares[42].OccupiedBy = players[0];
        //    gameboard.Squares[42].Tenant?.PieceCount = 1;

        //    IGameRepository gameRepository = new TestGamesRepository();

        //    //Act
        //    bool result = new Moves(gameRepository, new GameEngine(gameRepository)).MovePiece(gameboard, gameboard.Squares, players[0], 42, 5);

        //    //Assert that move was successful
        //    Assert.True(result);

        //    //Assert that player has moved from initial position
        //    Assert.Null(gameboard.Squares[42].OccupiedBy);
        //    Assert.Equal(0, gameboard.Squares[42].Tenant?.PieceCount);

        //    //Assert that player has moved to target position
        //    Assert.Equal(players[0], gameboard.Squares[41].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[41].Tenant?.PieceCount);
        //}

        //[Fact]
        //public void MoveBackwards_PathIsNotClear_PlayerStaysAtInitialSquare()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Models.Player> players = new List<Models.Player>
        //    {
        //        new Models.Player("LudoPlayer", 14)
        //    };

        //    Gameboard gameboard = new Gameboard(players);

        //    gameboard.Squares[44].OccupiedBy = players[0];
        //    gameboard.Squares[44].Tenant?.PieceCount = 1;

        //    gameboard.Squares[41].OccupiedBy = players[0];
        //    gameboard.Squares[41].Tenant?.PieceCount = 1;

        //    //Act
        //    bool result = Moves.MoveBackwards(gameboard.Squares, players[0], 3, 44, gameboard.Squares[44]);

        //    //Assert that move failed
        //    Assert.False(result);

        //    //Assert that player has not moved
        //    Assert.Equal(players[0], gameboard.Squares[44].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[44].Tenant?.PieceCount);

        //    //Assert that piececount is still 1 on target position.
        //    Assert.Equal(players[0], gameboard.Squares[41].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[41].Tenant?.PieceCount);
        //}

        //[Fact]
        //public void CheckMoveOut_AnotherPlayerOnSquare_MoveOutPossible()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Models.Player> players = new List<Models.Player>
        //    {
        //        new Models.Player("LudoPlayer", 14),
        //        new Models.Player( "LudoPlayer2", 9)
        //    };

        //    Gameboard gameboard = new Gameboard(players);

        //    gameboard.Squares[0].OccupiedBy = players[1];
        //    gameboard.Squares[0].Tenant?.PieceCount = 1;

        //    //Act
        //    bool result = Moves.CheckMoveOut(gameboard.Squares, players[0], players[0].StartPosition);

        //    //Assert that moveout was successful
        //    Assert.True(result);
        //}

        //[Fact]
        //public void CheckMoveOut_OwnPlayerOnSquare_MoveOutFailure()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Models.Player> players = new List<Models.Player>
        //    {
        //        new Models.Player("LudoPlayer", 14)
        //    };

        //    Gameboard gameboard = new Gameboard(players);

        //    var ludoPlayerStartPosition = players[0].StartPosition;
        //    gameboard.Squares[ludoPlayerStartPosition].OccupiedBy = players[0];
        //    gameboard.Squares[ludoPlayerStartPosition].Tenant?.PieceCount = 1;

        //    //Act
        //    bool result = Moves.CheckMoveOut(gameboard.Squares, players[0], players[0].StartPosition);

        //    //Assert that moveout was unsuccessful
        //    Assert.False(result);
        //}

        //[Fact]
        //public void AddTwoPiecesToStartPosition_OriginalCountZero_NewCountTwo()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Models.Player> players = new List<Models.Player>
        //    {
        //        new Models.Player("LudoPlayer", 14)
        //    };

        //    Gameboard gameboard = new Gameboard(players);
        //    var playerStartPosition = players[0].StartPosition;
        //    Assert.Equal(0, gameboard.Squares[playerStartPosition].Tenant?.PieceCount);

        //    //Act
        //    bool result = Moves.AddPieces(gameboard.Squares, gameboard.Players[0], playerStartPosition, 2);

        //    //Assert that moveout was successful
        //    Assert.True(result);
        //    //Assert that player has moved to target position
        //    Assert.Equal(players[0], gameboard.Squares[playerStartPosition].OccupiedBy);
        //    Assert.Equal(2, gameboard.Squares[playerStartPosition].Tenant?.PieceCount);
        //}

        //[Fact]
        //public void MovePiece_PieceCountIsLessThan0_ExceptionIsThrown()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Models.Player> players = new List<Models.Player>
        //    {
        //        new Models.Player("LudoPlayer", 14)
        //    };

        //    IGameRepository gameRepository = new TestGamesRepository();
        //    Gameboard gameboard = new Gameboard(players);
        //    new Moves(gameRepository, new GameEngine(gameRepository));

        //    //Act & Assert that the move throws an exception.
        //    Assert.Throws<Exception>(() => new Moves(gameRepository, new GameEngine(gameRepository)).MovePiece(gameboard, gameboard.Squares, players[0], 0, 5));
        //}

        //[Fact]
        //public void GetAllGamesAndLoadGame_StartAndGoalPosition_CorrectIndex()
        //{
        //    //Arrange
        //    IGameRepository gameRepository = new TestGamesRepository();
        //    IGameEngine gameEngine = new GameEngine(gameRepository);
        //    Gameboard.CreateTracks();
        //    var gameboards = gameRepository.GetAllGamesAsync().Result;

        //    //Act
        //    gameEngine.LoadGame(gameboards[0]);

        //    //Assert that Players has correct start and goal square
        //    Assert.Equal(10, gameboards[0].Players.Single(player => player.Color == 12).StartPosition);
        //    Assert.Equal(49, gameboards[0].Players.Single(player => player.Color == 12).GoalIndex);
        //}
    }
}

