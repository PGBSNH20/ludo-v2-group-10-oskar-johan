1. Code (API)
   1. (Bug) "move your piece" wrong destination square if blocked by own piece
   1. (Bug) red starts at 0
   1. (bug) when blocked by own piece, user it asked to roll die again when it should remove the moveaction and prompt them to try again
   1. (bug) 
1. Code (Web App)
    1. [x] Send Selection MoveAction
        ```csharp
        IndexModel.OnPostChooseMoveActionAsync()
        ```
    1. Validate selected MoveAction
        ```csharp
        GameplayController.PostChooseAction()
        ```
        1. error -> retry
        1. Success -> draw gameboard
    1. [x] Figure our who's next.
        1. [x] If player rolled 6 -> same player
        1. [x] Else -> get next player and
    1. [x] Set next player
    1. Victory Screen
    1. SignalR to tell clients to reload when another player has completed their turn
    1. Status messages for moves
       1. "X won"
       1. "X moved/added piece [from] to square"
   1. List of players in game with colors, in "color order" and indication if it's a player's turn.
1. Code (API)
1. Code (Web App)
1. Documentation
1. Tester
   1. Test all possible moves.
   1. Manual testing of web app
1. Extra:
   1. SignalR
   1. Authentication
   1. l10n & i18n
1. *Extra Extra*:
   1. Animate piece movement
   1. Drag & drop pieces to move
   1. History of moves (hover to show pieces position)
