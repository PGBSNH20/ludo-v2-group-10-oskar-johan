# Game Flows

1. Load page (asp.net razor)
2. Select "New game"
    1. Input number of players, player names and select colors (and UI language?)
    1. Submit → ASP.NET razor → Rest POST request `PlayerDTO[]` → Ludo_API
    1. Ludo_API → error || redirect → (`localhost:port/ludo/{gameboardId}`)
    1. If redirected:
        1. Page with gameboard (razor page) informing that game is saved automatically.
        1. Ask what first player wants to do (roll dice)
        1. If "roll dice" is selected:
            1. "Roll Dice" button → ASP.NET Razor → API request → Ludo_API:
            1. POST localhost:port/api/Game/RollDie
            1. List of options
            1. Choose option →

## New Turn

1. Player is informed that it is their turn and is given options:
   1. Exit to menu (this informs of the `auto-save` feature)
   1. [Throw dice ↓](###-(1.2.)-Player-throws-dice:)


### (1.2.) Player throws dice:

1. Player selects `throw dice`-option → `Request` is sent to WebApp (with `<form POST>`?).
1. WebApp (server) forwards request to API → POST @ `api/Game/ThrowDice` with:
   > fixme: name `ThrowDice`?
   ```csharp
    HEADER { Authentication }
    
    BODY
    {
        int gameId,
        int playerId
    }
   ```
1. API receives POST request and checks DB for `gameId` and `playerId`.:
    1. API doesn't find a valid `Game|Player`:
        - `Error` handling
        - `Logging`?
1. API throws the `Dice` and generates a `List<MoveAction/TurnAction>`.
1. API saves `List<MoveAction/TurnAction>` to DB.
1. API responds with:
    1. > todo: error responses
    1. data: (`List<MoveAction/TurnAction>`) e.g.:
        ```csharp
        HEADER { Authentication }

        BODY
        [
            {MoveAction/TurnAction},
            {MoveAction/TurnAction},
            ...
        ]
        ```
1. The WebApp processes the data, and responds (with a `view/page`?).
1. Client receives the new `view/page` which presents the player with the `MoveAction`s`/TurnAction`s.
1. Player selects a `MoveAction/TurnAction` → `Request` is sent to WebApp (with `<form POST>`?).
    > If the selected `MoveAction/TurnAction` is invalid and there are additional `MoveAction`s`/TurnAction`s the player can try them all CLIENT-side.
1. WebApp (server) forwards request to API → POST @ `api/Game/ChooseAction` with:
    > fixme: name `ChooseAction`?
    ```csharp
    HEADER { Authentication }

    BODY
    {
        int moveActionId/turnActionId
        int gameId, // unnecessary?
        int playerId, // unnecessary?
    }
   ```
1. API receives POST request and checks DB for `moveActionId/turnActionId` (and maybe: `playerId` &| `playerId`?)
    1. API doesn't find a valid `moveActionId/turnActionId`:
        - `Error` handling
        - `Logging`?
        - Sends response to WebApp|Client, e.g.:
        ```csharp
        HEADER { Authentication }

        BODY
        {
            bool MoveActionSuccessful/TurnActionSuccessful: false,
            string errorMessage: "",
        }
        ```
    1. API find valid `MoveAction/TurnAction` → `continue`
1. API executes the `MoveAction/TurnAction`.
1. Check if `MoveAction/TurnAction` is valid.
    1. If opponent occupies the `DestinationSquare`, return message informing the players of this.
    1. If 
1. API deletes the `MoveAction/TurnAction`.
1. API return a message saying that the `MoveAction/TurnAction` was successful instructs the `WebApp|Client` to present the new gameboard state.
    - Animate movement of `Pieces`?
    - The response from the API also includes `Player`-data for the next player in the turn order.
        > If the current `Player` cast a 6 with the `Dice`, the `Player gets another turn.
1. API responds with:
    ```csharp
    HEADER { Authentication }

    BODY
    {
        bool MoveActionSuccessful/TurnActionSuccessful: true,
        string message,
        Player nextPlayer,
    }
    ```
1. [Go back (step 1.).](##-New-Turn)
