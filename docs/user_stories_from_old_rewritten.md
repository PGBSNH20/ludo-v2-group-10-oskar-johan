# User stories

## As a user I want/should to be able to:
- Start a `new game`
    - Choose between 2 and 4 `player`s
        - (optional) `Human` or `AI`
    - Choose a color and name for each `player`.
- See a `gameboard` with all `player`'s tokens.
- See the `score`.
- Play a `round`.
    - Roll the `die`.
    - Make a decision based on the `die`-roll.
        - Choose to enter new (a) `token`(s) into player (applicable if the result of the `die` roll is 1 or 6).
            - 1 `token` to my starting `square` (applicable if I roll a **1** and have exactly 1 `token` in my `yard`).
            - 1 `token` to my the sixth `square` from my starting `square` (applicable if I roll a **6** and have exactly 1 `token` in my `yard`).
            - 2 `token`s to my starting `square` (applicable if a roll a **6** and have at least 2 `token`s in my `yard`)
        - Move a `token` (applicable if the `player` has a `token` in play and isn't blocked by one of their other `token`s).
            - **I cannot** pass one of my own `token`s.
            - Move to the "finishing `square`" and take a `token` out of play (applicable if I roll the exact number required to reach the "finish").
        - Knock another `player`'s `token` out of `player` (back to said `player`'s "`yard`" (applicable if I move my `token` to a `square` occupied by another `player`).
        - Roll a bonus `die` as long as I roll a **6**.
- See some kind of "Victory UI" when I win.
- (optional) continue playing after a `player` has won?
- Resume a `Game`.
- Save a `game`.
    - `Game`s are automatically saved at the end of each turn.
    - (optional) Save a `game` to disc.
- Read the `rules`.
    - [https://www.spelregler.org/fia-med-knuff/](https://www.spelregler.org/fia-med-knuff/)
