# Endpoints
Data is returned in JSON format.

Endpoints:
* Games - Endpoint for managing games and letting players start or join games.
* Gameplay - Endpoint for handling gameplay once a game has started.

## GAMES
<br/>

**GET** /api/Games  
Get all game rows/entities from the database.
#### Rest Client example:  
```
GET localhost:44376/api/Games/
```
<br/>

**GET** /api/Games/{id}  
Path parameter: id (int)  
Find and return a {game} entity with the specified {id} from the database.
#### Rest Client example:  
```
GET https://localhost:44360/api/Games/1
```
<br/>

**GET** /api/LudoData  
Get Ludo data such as gameboard layout, player colors and their gameboard track indices.
#### Rest Client example:  
```
GET localhost:44376/api/Games/LudoData
```
<br/>

**POST** /api/New  
Create a new game and returns ID of the new gameboard whichs represents a game.  
Body parameter: A NewPlayerDTO object containing the name and chosen id of the player creating the game.
#### Rest Client example:  
```
POST https://localhost:44360/api/New
Content-Type: application/json

{
    "ID": 1,
    "PlayerName":"Player1",
    "PlayerColor":"Yellow"
}
```
<br/>

**DELETE** /api/Games/{id}  
Path parameter: id (int)  
Create a new game and returns ID of the new gameboard whichs represents a game.  
#### Rest Client example:  
```
DELETE https://localhost:44360/Games/1
Content-Type: application/json

```
<br/>

**POST** /api/Games/AddPlayer  
Add a new player to a game.  
Body parameter: A NewPlayerDTO object containing the name and color the player.
#### Rest Client example:  
```
POST localhost:44376/api/Games/AddPlayer
Content-Type: application/json
//apikey: apikey1234_visitor

{
    "GameId": 1,
    "PlayerName": "Player2",
    "PlayerColor": "Red"
}

```
<br/>

**POST** /api/Games/StartGame  
Starts a game.  
Body parameter: Game ID (int) to be started.
#### Rest Client example:  
```
POST localhost:44376/api/Games/StartGame
Content-Type: application/json

1

```
<br/>

## Gameplay
<br/>

**POST** /api/Gameplay/RollDie  
Rolls a die for the player and returns a list of actions availible for the player.  
Body parameter: A PostRollDieDTO object containing Game ID and Player ID.
#### Rest Client example:  
```
POST localhost:44376/api/Gameplay/RollDie
Content-Type: application/json

{
    "GameId": 1,
    "PlayerId": "1"
}
```
<br/>

**POST** /api/Gameplay/ChoseAction  
Send the ID of the chosen action by the player.  
Body parameter: ID (int) of the action the player has chosen.
#### Rest Client example:  
```
POST localhost:44376/api/Gameplay/ChoseAction
Content-Type: application/json

1
```
<br/>

**POST** /api/Gameplay/GetMoveActions  
Get a list of all availible actions for a die roll.  
Body parameter: A PostRollDieDTO object containing Game ID and Player ID.
#### Rest Client example:  
```
POST localhost:44376/api/Gameplay/GetMoveActions
Content-Type: application/json

{
    "GameId": 1,
    "PlayerId": "1"
}
```
<br/>