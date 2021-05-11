# Documentation

## Table of contents

- [Process](#Process)
- [Database](#Database)
  - [Models](#Models)
- [Endpoints](#Endpoints)
- [Credits & Sources](#Credits-&-Sources)

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

- api/Games
  - GET
  - PUT/PATCH
  - DELETE (admins only?)
  - POST

---

- api/Game/{gameId}/Player/{playerId}/MoveToken/{tokenId}
- api/Game/{gameId}/Token/{tokenId}/MoveTo/{squareId}

  - POST/PATCH/PUT

    ```csharp
    // request body:
    {
        int fromSquareId
        int toSquareId
    }

    // response:
    {
        Ok() // The token was successfully moved.
        BadRequest() // ??? If the supplied {gameId || tokenId} is not in the correct format. ??? If the request is malformed in any way.
        NotFound() // If there isn't a game || token with the supplied {gameId || tokenId}
    }
    ```

---

- api/Game/{gameId}/Token/{tokenId}/MoveTo/{squareId}
- api/Game/{gameId}/Token/{tokenId}/MoveTo/{squareId}

  - POST/PATCH/PUT

    ```csharp
    // request body:
    {
        int steps
    }

    // Possible responses:
    {
        Ok() // The token was successfully moved.
        BadRequest() // ??? If the supplied {gameId || tokenId || squareId} is not in the correct format. ??? If the request is malformed in any way.
        401 // Unauthorized (players in the game should be able to update?). Or stricter still (only the player who owns the token can move it?)
        NotFound() // If there isn't a game || token || square with the supplied {gameId || tokenId || squareId}
    }
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
    ```

---

- api/Game/{gameId}
  - POST
    ```csharp
    {
        // (player is sent a list of possible moves (each with an id))
        // POST the chosen move's id
        int gameMoveId
    }
    ```

---


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
    }

    // Possible responses:
    Ok {
      dieRoll: [1-6]
    }
    401 // Unauthorized; Necessary?
    ```

---

---

## Credits & Sources
