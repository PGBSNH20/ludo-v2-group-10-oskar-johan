1. Load page (asp.net razor)
2. Select "New game"
    1. Input number of players, player names and select colors (and UI language?)
    1. Submit -> ASP.NET razor -> Rest POST request `PlayerDTO[]` -> Ludo_API
    1. Ludo_API -> error || redirect -> (`localhost:port/ludo/{gameboardId}`)
    1. If redirected:
        1. Page with gameboard (razor page) informing that "Spelet sparas automatiskt"
        1. Ask what first player wants to do (roll dice)
        1. If "roll dice" is selected:
            1. "Roll Dice" button -> ASP.NET Razor -> API request -> Ludo_API:
            1. POST localhost:port/api/Game/RollDie
                1. List of options
                1. Choose option -> 
