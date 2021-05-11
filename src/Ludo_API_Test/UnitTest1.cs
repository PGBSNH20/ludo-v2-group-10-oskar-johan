using System;
using System.Collections.Generic;
using Xunit;
using Ludo_API;
using Ludo_API.Database;
using Ludo_API.Models;
using System.Linq;
using Ludo_API.Tests;
using Ludo_API.Repositories;
using Ludo_API.GameEngine.Game;
using System.Drawing;

namespace Ludo_API.Tests
{
    public class Ludo_API_Test
    {
        [Fact]
        public void MovePiece_PathIsClear_MoveIsSuccessful()
        {
            //Arrange
            Gameboard.CreateTracks();

            List<Player> players = new List<Models.Player>
            {
                new Player("LudoPlayer", Color.Yellow)
            };

            Gameboard gameboard = new Gameboard(players);

            gameboard.Squares[0].OccupiedBy = players[0];
            gameboard.Squares[0].PieceCount = 1;
            IGameRepository gameRepository = new GameRepositoryTest();
            IPlayerRepository playerRepository = new PlayerRepository();
            var game = new Game(gameRepository, gameboard);
            bool canMove = game.CanMoveToken(players[0], gameboard.Squares[players[0].StartPosition], 5);
            game.MoveToken(players[0], gameboard.Squares[0], gameboard.Squares[5]);

            // Assert that move was successful
            Assert.True(canMove);

            // Assert that player has moved from initial position
            Assert.Null(gameboard.Squares[0].OccupiedBy);
            Assert.Equal(0, gameboard.Squares[0].PieceCount);

            // Assert that player has moved to target position
            Assert.Equal(players[0], gameboard.Squares[5].OccupiedBy);
            Assert.Equal(1, gameboard.Squares[5].PieceCount);
        }

        //[Fact]
        //public void MovePiece_PathIsNotClear_PlayerStaysAtInitialSquare()
        //{
        //    //Arrange
        //    Gameboard.CreateTracks();

        //    List<Models.Player> players = new List<Models.Player>
        //    {
        //        new Models.Player("LudoPlayer", 14)
        //    };

        //    Gameboard gameboard = new Gameboard(players);

        //    gameboard.Squares[0].OccupiedBy = players[0];
        //    gameboard.Squares[0].PieceCount = 1;

        //    gameboard.Squares[2].OccupiedBy = players[0];
        //    gameboard.Squares[2].PieceCount = 1;

        //    IGameRepository gameRepository = new GameRepositoryTest();

        //    //Act
        //    var moved = new Moves(gameRepository, new GameEngine(gameRepository)).MovePiece(gameboard, gameboard.Squares, players[0], 0, 5);

        //    //Assert that move failed
        //    Assert.False(moved);

        //    //Assert that player has NOT moved from initial position
        //    Assert.Equal(players[0], gameboard.Squares[0].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[0].PieceCount);

        //    //Assert that player has NOT moved to target position
        //    Assert.Null(gameboard.Squares[5].OccupiedBy);
        //    Assert.Equal(0, gameboard.Squares[5].PieceCount);
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
        //    gameboard.Squares[42].PieceCount = 1;

        //    IGameRepository gameRepository = new GameRepositoryTest();

        //    //Act
        //    bool result = new Moves(gameRepository, new GameEngine(gameRepository)).MovePiece(gameboard, gameboard.Squares, players[0], 42, 5);

        //    //Assert that move was successful
        //    Assert.True(result);

        //    //Assert that player has moved from initial position
        //    Assert.Null(gameboard.Squares[42].OccupiedBy);
        //    Assert.Equal(0, gameboard.Squares[42].PieceCount);

        //    //Assert that player has moved to target position
        //    Assert.Equal(players[0], gameboard.Squares[41].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[41].PieceCount);
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
        //    gameboard.Squares[44].PieceCount = 1;

        //    gameboard.Squares[41].OccupiedBy = players[0];
        //    gameboard.Squares[41].PieceCount = 1;

        //    //Act
        //    bool result = Moves.MoveBackwards(gameboard.Squares, players[0], 3, 44, gameboard.Squares[44]);

        //    //Assert that move failed
        //    Assert.False(result);

        //    //Assert that player has not moved
        //    Assert.Equal(players[0], gameboard.Squares[44].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[44].PieceCount);

        //    //Assert that piececount is still 1 on target position.
        //    Assert.Equal(players[0], gameboard.Squares[41].OccupiedBy);
        //    Assert.Equal(1, gameboard.Squares[41].PieceCount);
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
        //    gameboard.Squares[0].PieceCount = 1;

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
        //    gameboard.Squares[ludoPlayerStartPosition].PieceCount = 1;

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
        //    Assert.Equal(0, gameboard.Squares[playerStartPosition].PieceCount);

        //    //Act
        //    bool result = Moves.AddPieces(gameboard.Squares, gameboard.Players[0], playerStartPosition, 2);

        //    //Assert that moveout was successful
        //    Assert.True(result);
        //    //Assert that player has moved to target position
        //    Assert.Equal(players[0], gameboard.Squares[playerStartPosition].OccupiedBy);
        //    Assert.Equal(2, gameboard.Squares[playerStartPosition].PieceCount);
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

        //    IGameRepository gameRepository = new GameRepositoryTest();
        //    Gameboard gameboard = new Gameboard(players);
        //    new Moves(gameRepository, new GameEngine(gameRepository));

        //    //Act & Assert that the move throws an exception.
        //    Assert.Throws<Exception>(() => new Moves(gameRepository, new GameEngine(gameRepository)).MovePiece(gameboard, gameboard.Squares, players[0], 0, 5));
        //}

        //[Fact]
        //public void GetAllGamesAndLoadGame_StartAndGoalPosition_CorrectIndex()
        //{
        //    //Arrange
        //    IGameRepository gameRepository = new GameRepositoryTest();
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

