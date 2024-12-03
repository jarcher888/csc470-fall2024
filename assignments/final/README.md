## Description
My idea for a final project revolves around the player navigating through different areas/levels without being “seen” by security guards, or some other patrolling unit. The very loose story is that you are a thief who broke into this building and you are attempting to escape. The goal will be to get from one end of a hall/room without the guards seeing you, eventually getting to a front door. If they reach the front door they win. Each room will be different, some may have obstacles to hide behind or a large number of guards for the player to navigate. My “core mechanic” will be moving and getting caught by the guards.

The security guards will have a set path that they will move back and forth on. Ideally, their field of view would be a cone out in front of them, however, I’m unsure of how difficult that would be to implement, so I will be starting with just a Raycast a few units in front of them.

I want my audio style to be stealthy, if that makes sense. In other words, minimal sound because of the environment the player is in. I will probably add a sound for the game starting, the player winning, and the player getting caught.

My visual style I wish to be dark. However, this depends on how well my progress is. I think it would be cool to implement some sort of flashlight system. If time does not permit me to create that, then I will make the game environment lighter. Maybe the player robbed the house during the day, who knows. For other elements of the visual style, I will be utilizing similar themes as the rest of the assignments, i.e., very simple and blocky with perhaps some decoration thrown in. 

The camera will be positioned above the room to give a top-down view at a slight angle. To add some pizzazz to the project, I’m going to implement a “hide” mechanic. In this universe the thief is able to turn invisible, for some reason. While invisible, the player cannot be detected by the guards. However, the invisibility only lasts for so long and there is a cooldown for it. 

A large portion of the screen will be taken up by the gameplay. The player will move across the screen from left to right, from one doorway to another. In one corner of the screen there will be a meter or timer to track the use of the invisibility ability.

## Low/Medium/High Bars
My low bar is to create a game that has 1 level. The environment will be light, however other elements of the visual and audio style will be the same as higher bars. The patrolling units will follow a fixed path and will “see” the player if the player intercepts a raycast a few units in front of the guard.

My medium bar is everything my low bar has plus some. A game that has at least two levels/”rooms”. I will implement the hiding mechanic. I will put more thought into the level design to try and create something actually fun and/or challenging. Something that lies on the border of my medium and high bar is the flashlight mechanic, though it is not my priority for the medium bar.

My high bar is a game that has 2+ levels. The visual style is darker, to emulate it being nighttime and the player has a flashlight that allows them to view an area in front of them. The guard’s field of view will also be emulated by a flashlight and if the player goes into the flashlight, they are caught. This is in my high bar because I do not know how to make the raycasting in a cone shape to cover more area than just a line. 

I think I have some decent challenges at each level.

## Timeline
### 12/3: have a player that can move and an outline of the first level
### 12/5: implement a patrol unit and enable the player to be caught
### 12/6: Core mechanic playtesting
### 12/9: have another level complete and have the invisibility implemented
### 12/11: have the game polished and tested for bugs. Implement audio and other outstanding UI elements

Any remaining time will be used to reach my high bar.
