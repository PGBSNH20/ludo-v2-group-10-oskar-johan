# TODO

## Getting started
- [x] Create folder structure, solution & API project.
- [x] Create `user stories`.
- [x] Get database up and running with Docker Compose.
- [x] Create basic API for saving to database.
- [x] Create an initial API design draft.
- [x] Create an initial database structure.

## When up and running (reorder as necessary)
- [ ] Documentation
    - [ ] Api design
    - [ ] Database
    - [ ] UML/flowchart
    - [ ] Web App
    - [ ] Optional parts
        - [ ] Deploy a web server.
        - [ ] Login in functionality (authentication & authorization).
        - [ ] Invite players via email.
        - [ ] Update GUI asynchronously (e.g. position of `token`s) (live).
        - [ ] Responsive "mobile friendly" UI (html).
- API and database:
    - [ ] Finalize the API.
    - [ ] Finalize the database structure.
        - [ ] New Game created by column?
    - [ ] ??? - ORM for reading game/config data from a (e.g. json) file containing things such as gameboard layout and information about squares(e.g. color, index ...)?
- WebApp
    - [x] Create a webapp project.
    - [ ] Create Ludo GUI.
        - [ ] Load game:
            - If a game is active, show gameboard and allow the player whose' turn it is to play their turn thus resuming the game.
        - [ ] Couple the data we fetch from the API to the html razor page ("pages/ludo/index.html")
        - [ ] Show when a player wins the game.
        - [ ] (optional) - Victory Screen.
        - [ ] (optional) - A list of active games and the ability to load them.
        - [ ] (optional) - History page (old games)?
        - [ ] (optional) - Profile page (if we add login functionality)?
    - [ ] ??? - Login/Create account page/component/?
    - [ ] Create unique URL for each game.
        - [ ] Uses gameboard id for, switch to something that's harder to guess (e.g. [`class ShortGuid` url friendly base64 encoded guid](../src/Ludo_API/Utils/ShortGuid.cs)?
- API or/and WebApp?:
    - [ ] Basic authentication?
    - [ ] (in progress) Validate input.

## Optional elements ("VG")
- [ ] Deploy a web server.
    - How?
- [ ] Login in functionality (authentication & authorization).
- [ ] Invite players via email.
- [ ] Update GUI asynchronously (e.g. position of `token`s) (live).
    - SignalR?
- [ ] i18n - localization of GUI.
- [ ] Responsive "mobile friendly" UI (html).

## Unit tests
- [ ] Web API
- [ ] Web App
