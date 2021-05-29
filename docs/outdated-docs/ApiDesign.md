# Api Design

## Endpoints

### `api/Games` (GamesController)

- `api/Games`
    - GET
        > Get all games/gameboards.
- `api/Games/{id}`
    - GET
        > Get the game/gameboard with the specified id.
        - {id} = id of gameboard in database
    - PUT
        > Update the game/gameboard with the specified id.
        - {id} = id of gameboard in database
    - DELETE
        > Delete the game/gameboard with the specified id.
        - {id} = id of gameboard in database
- `api/Games/New`
    - POST
        ```
        body:
        {
            string PlayerName,
            string PlayerColor
        }
        ```
- `api/Games/AddPlayer`
    - POST
        > Add player to an existing but not yet started game.
        ```
        body:
        {
            int gameId
            string PlayerName,
            string PlayerColor
        }
        ```
- `api/Games/StartGame`
    - POST
        > Start an existing but not yet started game.
        ```
        body: int gameId
        ```

### `api/Gameplay` (GameplayController)
- `api/Gameplay/RollDie`
    - POST
        > Rolls the die and returns a `List<MoveAction>` (i.e. all possibles moves).
        ```
        body:
        {
            int gameId,
            int playerId
        }
        ```
- `api/Gameplay/ChoseAction`
    - POST
        > Executes a `MoveAction` (i.e. a possible move) and returns a bool indicating wether the action was successful.
        ```csharp
        body:
        int moveActionId // id of the chosen m
        ```
