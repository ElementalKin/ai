# ai

My plan for this project is to make a tower defense game, at the start of the game the player will choose where to put their main tower on a grid 
I will be using A* to have the AI find a path to the players main tower from the four walls of the grid (if any are available) one wall must have an opening
I will be using a decision tree that will take in account all the possible paths(and their length) to the main tower and try and choose the best spots to spawn their units 
(I.E. say that the player has walled in one side of the grid the AI will not spawn any units on that side)
it will also use what towers the player has to choose what units it will spawn
(I.E. say that player has a bunch of fast shooting towers they will send slower armoured units instead of faster weaker units)
The player will be able to choose from three towers turret (fast but low dmg), sniper(slow but high dmg), and cannon(AOE).
The AI will be able to choose between three units tank(slow but with high hp), peasant(medium speed with low hp but will spawn in groups of three), speeder(fast but medium).