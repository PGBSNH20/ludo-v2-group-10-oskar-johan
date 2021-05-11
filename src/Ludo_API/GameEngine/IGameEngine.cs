using Ludo_API.GameEngine.Game;

namespace Ludo_API.GameEngine
{
    public interface IGameEngine
    {
        void GetRules();
        void NewGame(GameOptions gameOptions);
        void LoadGame(int gameId);
        void RunGame(Game.Game game);
        void EndGame();
    }
}
