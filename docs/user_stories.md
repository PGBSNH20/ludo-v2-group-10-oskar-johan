# User Stories
note: Should we use a [Github Project](https://github.com/PGBSNH20/ludo-game-group-6/projects/1) for user stories?

## As a user I can...
* chose what I want to do when the game starts with a menu
    1. (_userstory_)(**menuitem**) start a new game
        * choose the number of `Player`s (at least: 2, max: 4)
           * (**voluntary**) choose to play against `AI` and/or other `Player`s
        * pick a `ConsoleColor` (not optional)
           * (**optional**) automatically assign a number and print that too
        * see the gameboard with the `Player`s' `Token`s and `Score`s.
        * see a victory UI/message when I or another player is victorious
        * ~~chose whether to continue when a player has won~~
        * roll the `Die`
        * make a decision based on the roll
            * if the roll == 6 OR 1 they can enter a new token into play (if)
        * enter a `Token` into play
        * move a `Token` already in play
            * **cannot** move a `Token` past one of my other `Token`s.
            * move my `Token`(s) in the `Home` column
        * ~~**have to** move backwards if the `Die` ends up on the floor~~
        * knock another `Player`s `Token` out of play (back to the `Yard`) if mine lands on theirs
        * chose whether to enter 1 `Token` (placed a the `Position` 6), 2 `Token`s (placed at `Position` 1), or move a `Token` already on the board
        * roll a bonus `Die` if I roll a _6_
        * **have to** roll the exact number  to enter the `Home` "triangle"
    1. (_userstory_)(**menuitem**) resume a `Game` ~~and see the out come of previous games~~
    1. (_userstory_)(**menuitem**) save/leave a `Game`
        * (**option 2**) leave (upon which the `Game` saves automatically)
    1. (_userstory_)(**menuitem**) read the rules
        * rule-sets:
            1. (**rule-set**) [spelregler.se](https://www.spelregler.org/fia-med-knuff/)
            
