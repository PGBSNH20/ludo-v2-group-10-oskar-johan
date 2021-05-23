# Documentation

## Table of contents

- [Process](#Process)
- [Database](#Database)
  - [Models](#Models)
- [Endpoints](#Endpoints)
- [Credits & Sources](#Credits-&-Sources)

## TODO (document this)
- Validation attributes
- Why use DI for `Turnmanager : ITurnBased`?
  - Multiple rule sets?

## TODO (include in video presentation)
- Validation attributes

## Process

## Database

---

---

### Models

- Gameboard
- Player
- Square

---

---

## Endpoints

> {\*Id}/{id} could be an ID, GUI or name. E.g.:

- ID: https://localhost:5001/ludo/4/
- GUID: https://localhost:5001/ludo/1c1707cb-86fe-4cce-8ce2-06cd2d16f851/
- Name: https://localhost:5001/ludo/fiaspel2344/

---

### **_Ideas_**:

#### **Basic**

---

---

- api/Games

  - GET
    - Get all games
      // todo: update when authentication/authorization has been implemented.
  - DELETE (admins only?)
    - Delete all games
      <br>// note: do we want this?
      <br>// todo: update when authentication/authorization has been implemented.

---

- api/Games/{id}

  - GET
    - Get the game with the specified {id}
      // todo: update when authentication/authorization has been implemented.
  - PUT/PATCH

    - Update the game with the specified {id}
      <br>// note: do we want this?
      <br>// todo: update when authentication/authorization has been implemented.

  - DELETE (admins only?)
    - Delete the game with the specified {id}
      // todo: update when authentication/authorization has been implemented.

---

- api/Games/New

  - POST

    - Add a new game

      ```csharp
      // request:
      header: {
        // apikey or another form of authentication
      },
      body: {
        players: [
          {
            string name,
            ? color
          },
          {
            string name,
            ? color
          }
        ]
      }

      // Possible responses:
      header: {},
      body: {
        
      }
      ```

---

#### **Get/set Game Data/Options (min & max player count, available colors, AI-players available?, gameboard info?, rules?)**

- api/LudoGameInfo

  - GET

    ```csharp
    // request body:
    {
    }

    // Possible responses:
    Ok() {
      int minPlayerCount,
      int maxPlayerCount,
      aRgb[] availableColors,
      bool AIPlayerAvailable,
      ? gameboardInfo, // Number of squares? gameboard grid layout?
      string[] rules
    }
    BadRequest() // ???; Necessary?
    NotFound() // ???; Necessary?
    401 // Unauthorized; Necessary?
    ```

  - PUT || PATCH

    ```csharp
    // request body:
    {
      int minPlayerCount,
      int maxPlayerCount,
      aRgb[] availableColors,
      bool AIPlayerAvailable,
      ? gameboardInfo, // Number of squares? gameboard grid layout?
      string[] rules
    }

    // Possible responses:
    Ok() // The token was successfully moved.
    BadRequest() // ???
    NotFound() // ???
    401 // Unauthorized; Necessary?
    ```

---

#### **Move Token**

- api/Game/{gameId}/Player/{playerId}/MoveToken/{tokenId}
- api/Game/{gameId}/Token/{tokenId}/MoveTo/{squareId}

  - POST/PATCH/PUT

    ```csharp
    // request body:
    {
        int fromSquareId
        int toSquareId
    }

    // Possible responses:
    Ok() // The token was successfully moved.
    BadRequest() // ??? If the supplied {gameId || tokenId} is not in the correct format. ??? If the request is malformed in any way.
    NotFound() // If there isn't a game || token with the supplied {gameId || tokenId}
    401 // Unauthorized; Necessary?
    ```

---

#### **Move Token**

- api/Game/{gameId}/Token/{tokenId}/MoveTo/{squareId}
- api/Game/{gameId}/Token/{tokenId}/MoveTo/{squareId}

  - POST/PATCH/PUT

    ```csharp
    // request body:
    {
        int steps
    }

    // Possible responses:
    Ok() // The token was successfully moved.
    BadRequest() // ??? If the supplied {gameId || tokenId || squareId} is not in the correct format. ??? If the request is malformed in any way.
    401 // Unauthorized (players in the game should be able to update?). Or stricter still (only the player who owns the token can move it?)
    NotFound() // If there isn't a game || token || square with the supplied {gameId || tokenId || squareId}
    ```

---

- api/Game/{gameId/Token/{tokenId}/Move
- api/Token/{tokenId}/Move

  - POST/PATCH/PUT

    ```csharp
    // request body:
    {
        int steps
    }

    // Possible responses:
    Ok() // The token was successfully moved.
    BadRequest() // ??? If the supplied {gameId || tokenId} is not in the correct format. ??? If the request is malformed in any way.
    401 // Unauthorized (only admins and/or players in the game should be able to delete?)
    NotFound() // If there isn't a game with the supplied {gameId}
    ```

---

#### **Delete a game**

- api/Game/{gameId/Delete}
- api/Game/Delete/{gameId/}
- api/Game/{gameId/}

  - DELETE (admins only?)

    ```csharp
    // Possible responses:
    Ok() // The game was successfully deleted.
    BadRequest() // ??? If the supplied {gameId} is not in the correct format. ??? If the request is malformed in any way.
    401 // Unauthorized (only admins and/or players in the game should be able to delete?)
    NotFound() // If there isn't a game with the supplied {gameId}
    ```

---

- api/Game/{gameId}/Square/{squareId}

  - PATCH
    ```csharp
    // request body:
    {
        int playerId
        int pieceCount
    }
    ```
  - GET
    ```csharp
    // possible responses:
    Ok {
        int id // squareId
        int playerId // or Player object?
        int pieceCount
        int gameBoardId // or gameId? necessary?
    }
    401 // Unauthorized; Necessary?
    ```

---

- api/Game/{gameId}

  - POST

    ```csharp
    // request body:
    {
        // (player is sent a list of possible moves (each with an id))
        // POST the chosen move's id
        int gameMoveId
    }

    // possible responses:
    Ok {
        int id // squareId
        int playerId // or Player object?
        int pieceCount
        int gameBoardId // or gameId? necessary?
    }
    401 // Unauthorized; Necessary?
    ```

---

#### **Die Roll**

- api/Game/{gameId}/RollDie

  - POST

    ```csharp
    // request body:
    {
    }

    // Possible responses:
    Ok {
      dieRoll: [1-6]
    }
    401 // Unauthorized; Unnecessary?
    BadRequest() // Unnecessary?; If the supplied {gameId || tokenId} is not in the correct format. ??? If the request is malformed in any way.
    ```

- api/Game/{gameId}/RollDie/{playerId}

  - POST

    ```csharp
    // request body:
    {
    }

    // Possible responses:
    Ok {
      dieRoll: [1-6]
    }
    401 // Unauthorized; Necessary?
    BadRequest() // Unnecessary?; If the supplied {gameId || tokenId} is not in the correct format. ??? If the request is malformed in any way.
    NotFound() // Unnecessary?; If there isn't a game || player with the supplied {gameId || playerId}
    ```

> There is probably no need to couple a die roll to a specific player or game, so these either of these should be enough.

- api/Die/Roll
- api/RollDie
- api/Game/RollDie

  - POST

    ```csharp
    // request body:
    {
      int/string gameId,
      int playerId
    }

    // Possible responses:
    Ok {
      gameId: {int}/{Guid},
      playerId: {int},
      dieRoll: [1-6],
      possibleMoves: [
        {
          int moveId,
          string "Insert 1 pieces/tokens to square {index}"
        },
        {
          int moveId,
          string "Insert 2 pieces/tokens to square {index}"
        },
        {
          int moveId,
          string "Move piece/token on Square {index}"
        }
      ]
    }
    401 // Unauthorized; Necessary?
    ```

---

#### **Add Players** // **Create new game**

>

- api/Players/Add

  - POST

    ```csharp
    // request body:
    {
      int gameId,
      Player[] players [
        {
          string playerName,
          aRgb playerColor,
        },
        {
          string playerName,
          aRgb playerColor,
        }
      ],
    }

    // Possible responses:
    Ok {
      // {player}-object with more data?
    }
    401 // Unauthorized; Necessary?
    BadRequest() // ???; If the supplied data ({player}[]) is invalid in some way. Maybe there's another error code better suited for this?
    NotFound() // If there isn't a game with the supplied {gameId}
    ```

- api/Players/Add?gameId={gameId}
- api/Game/{gameId}/Players/Add

  - POST

    ```csharp
    // request body:
    [
      {
        string playerName,
        aRgb playerColor,
      },
      {
        string playerName,
        aRgb playerColor,
      }
    ]

    // Possible responses:
    Ok {
      // {player}-object with more data?
    }
    401 // Unauthorized; Necessary?
    BadRequest() // ???; If the supplied data ({player}[]) is invalid in some way. Maybe there's another error code better suited for this?
    NotFound() // If there isn't a game with the supplied {gameId}
    ```

- api/Game/New

  - POST

    ```csharp
    // request body:
    {
      int GameId,
      Player[] players [
        {
          string playerName,
          aRgb playerColor,
        },
        {
          string playerName,
          aRgb playerColor,
        }
      ],
      (optional) int AiCount,
      (optional) string[] emailInvitesTo [
        string emailAddress,
        string emailAddress
      ]
    }

    // Possible responses:
    Ok {
      // {game}-object with more data?
    }
    401 // Unauthorized; Necessary?
    BadRequest() // ???; If the supplied data ({player}[]) is invalid in some way. Maybe there's another error code better suited for this?
    NotFound() // If there isn't a game with the supplied {gameId}
    ```

---

---

## Credits & Sources
- Lazy loading **singleton** https://csharpindepth.com/Articles/Singleton#performance
- https://github.com/microsoft/referencesource/blob/5697c29004a34d80acdaf5742d7e699022c64ecd/System.Web/Routing/RouteValueDictionary.cs#L61
  - Used this to figure out how to convert an anonymous object (of query-parameters) to a dictionary. This is used in the method GetAsync<T> in [Fetch.cs](../src/Ludo_WebApp/Ludo_API/Fetch.cs) (in the WebApp-project).
