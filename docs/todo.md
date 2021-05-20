# TODO

## Getting started
- [x] Create folder structure, solution & API project.
- [x] Create `user stories`.
- [x] Get database up and running with Docker Compose.
- [x] Create basic API for saving to database.
- [x] Create an initial API design draft.
- [x] Create an initial database structure.

## When up and running (reorder as necessary)
- API and database:
    - [ ] Finalize the API.
    - [ ] Finalize the database structure.
        - [ ] New Game created by column?
    - [ ] ??? - ORM for reading game/config data from a (e.g. json) file containing things such as gameboard layout and information about squares(e.g. color, index ...)?
- WebApp
    - [x] Create a webapp project.
    - [ ] Create Ludo GUI.
        - [ ] ??? - History page (old games)?
        - [ ] ??? - Profile page?
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
