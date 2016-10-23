# COMP30022 IT Project - Team MasterMinds

This is a project of team MasterMinds (Chao Li, Jack Qian, Junwei Yang and Weixu Chen), for the subject *COMP30022 IT Project* in the University of Melbourne.

## Project Overview

### Background

Nowadays, more and more people play games on their smartphones. We are about to build a simple Android application to help people relax in their free time. 

### Users

Everyone who would like to play online game using their smartphones and play with others.

### Product

The application is a 3D third-person multiplayer online battle arena video game. It will be targeted at users with mobile devices running iOS or Android. It was inspired by Warcraft III: The Frozen Throne mod, Warlock Brawl.

The application requires internet access to connect the server. Player will be able to play with their friends or others via internet. Player can chat in game and watch the replay of the game.

## Features

### GamePlay

As a 3D arena game, the objective of a player is to defeat all other players in the game, and become the last survival. Each player controls a character in the stage, where the character can move and cast spells that have various effects.

In the beginning of the game, players are spawned in the corners of a large stage. The ground will gradually shrink over time up until a point. Characters outside of the ground will be damaged by lava, losing some HP over time, forcing players to move to the centre of stage.

There are two types of spells in the game, being `Fireball` and `FireNova`. A `Fireball` is a projectile spell that travels to the direction that the caster points at, dealing damage when collide with other characters. A `FireNova` is a spell instantiated at the position of the caster, and deals damage to all other characters in a certain area around the spell after several seconds.

Control of the game is simple and intuitive. Use the virtual joystick at the lower left corner of the screen to move, and tap on buttons at the lower right corner casts a spell.

### Multiplayer

The game supports online multiplayer game, players can play with each other either by matchmaking or continuation from previous saved game.

When the player is at the main menu, the player can tap on quick match to play with other players that have also chosen quick match. If the player would like to continue from previous game, the player can tap on Load button to load from save file so that other players can join the game.


### Replay

The game also provides a replay system, so that players can watch the game played later. When a game is finished, the player can choose to save the game as a replay.

The player can then go to the replay selection and choose a replay. In the replay scene, a player can pause the replay at any time, and move the camera view freely.

### Save/Load

The game supports Save/Load function. When player wants to save the current game state and play later, the player can choose to save the current state during game play. Afterwards the player can resume the game state at any time by tapping on Load button in the main menu.


### Profile and Account

Every player in the game has an account for identification, and has a profile to save the stats of the player.

When the game is started, the login page will firstly show up, where a player can type in the email and login. If the player doesn't have one, pressing on the register button will allow the player to register for a new account.

Profile of an account contains some stats including the number of games played, and the number of victory/defeats. The profile is stored and updated on the server, so that a player will not lose the profile when switching to another device or reinstalling the game.

## Known Issues and Possible Improvements

### Issues

- Sometimes spells are not displayed in replay scene. It seems to only happen to spells instantiated by other players in a game
- In rare conditions, the game switches to the main menu from gameplay before it finishes. This seems to be a network related issue
- In rare conditions, the kill number can be doubled due to slow network connection
- Due to the mechanism of Photon Network, two players joining in the match making at the same time may not be handled as supposed in a proper way

### Improvements

- A better visual representation overall
- Sound effects and background music
- More types of characters and spells


## Manual

### Registration and Login

As the player entered the game, the player will be prompted to enter a registered email to continue (Figure 1). If the player would like to register an account, the player can tap on register button to create an account. In the registration page, the player can enter the desired user name and email address and then submit (Figure 2), after which the player will be taken into the main menu.
![Figure 1](https://i.imgur.com/pNjtlTy.png)
(Figure 1)
![Figure 2](https://i.imgur.com/XgabmPA.png)
(Figure 2)

If the player owns an account already, the player can simply enter the email and tap on login button to log in, however logging in with invalid email address will be rejected (Figure 3).
![Figure 3](https://i.imgur.com/OHMfnDM.jpg)
(Figure 3)


### Quick Match

When the player has logged in, the player can choose to start a quick match by tapping on quick match button (Figure 4). After which, the game will try to connect to the server with entered user name to look for other players that are also looking for players to play (Figure 5). Once there are at least two players joined the quick match, a countdown will start so that players have time to exit if they entered quick match by accident (Figure 6). After countdown finishes the game will start.
![Figure 4](https://i.imgur.com/2XFzF6r.png)
(Figure 4)
![Figure 5](https://i.imgur.com/jW5raEz.jpg)
(Figure 5)
![Figure 6](https://i.imgur.com/tLkF3Qn.jpg)
(Figure 6)


### Control

When the player is in the game, the player can control the movement of character by holding the joystick towards desired direction, so the character will start to move in the direction (Figure 7). To cast a `fireball`, the player can hold the joystick towards the direction and then tap on the spell icon for fireball (Figure 8), then a fire ball will be flying towards the direction. To cast a `fire nova`, the player can just simply tap on the spell icon, then the spell will be generated and explode in a few seconds (Figure 9).
![Figure 7](https://i.imgur.com/IVCshld.png)
(Figure 7)
![Figure 8](https://i.imgur.com/NtDoV5i.jpg)
(Figure 8)
![Figure 9](https://i.imgur.com/4FfqR5N.png)
(Figure 9)


### Voice Chat

When the player is in the game, the player can utilise voice chat feature to chat with other players. The player can tap on the green microphone icon in the upper left corner, when the player can see a green speaker icon above the character, it indicates the player can talk with other players. When the player wants to stop talking, the player can tap on the icon again to stop (Figure 10).
![Figure 10](https://i.imgur.com/tLSgQ4s.png)
(Figure 10)


### Pause Game

When the game is in the game, the player can pause the game. When the player would like to pause the game, the player can tap on the menu button on the upper right corner first and then tap on the pause button (Figure 11), then all players will be told the game has paused and the game can be resumed when any player taps on the pause button (Figure 12).
![Figure 11](https://i.imgur.com/hXvQm0z.png)
(Figure 11)
![Figure 12](https://i.imgur.com/6zUM1mz.png)
(Figure 12)


### Replay

The game supports replay function so that players can watch saved replay. When the game ends and comes to the result page, the player can tap on `Save Replay` button to save the replay for the game (Figure 13). Afterwards, the player can watch the replay at anytime.
![Figure 13](https://i.imgur.com/buZXYq5.jpg)
(Figure 13)

In the main menu, when the player taps on `Replay` button, the player will see a list of saved replay named by time (Figure 14). Then the player can tap on the button with desired time to watch replay. After the replay has started, the player can move around with one finger,  zoom in/out with two fingers, and can pause/continue the replay by tapping on corresponding button (Figure 15). 
![Figure 14](https://i.imgur.com/XdVpqEA.jpg)
(Figure 14)
![Figure 15](https://i.imgur.com/2YDBhSe.jpg)
(Figure 15)

If the player would like to exit the player can tap on the menu button on upper left corner, otherwise the player has to wait for replay to finish, then a message will tell the player that the replay has finished.

### Save/Load

The game supports Save/Load function, so that the player can choose to continue a game from saved file. When the player is in the game, the player can save the current state into a file by tapping on the `S`(save) button (Figure 16), so that the game state will be saved (only one game state can be saved, later game saved  will replace the previous one). After the game state has been saved, the player can continue from the saved game. When the player is in the main menu, the player can continue from saved game by tapping on Load button. After the player has loaded from save file, when other players in the previous game joined, the countdown will start and game will continue from previous state (Figure 17).
![Figure 16](https://i.imgur.com/rLvwgaM.png)
(Figure 16)
![Figure 17](https://i.imgur.com/VRuC6k7.jpg)
(Figure 17)


### Profile

For different accounts, all stats such as number of victory, killed player will be stored on the server. When the player would like to see the detailed stats, the player can enter the profile page by tapping on `Profile` button in the main menu (Figure 18).
![Figure 18](https://i.imgur.com/XAVffqg.jpg)
(Figure 18)


## Testing

Some unit tests are included in the project. To run the tests: 
1. You need to switch to `UnitTests` branch. 
2. Open the project by open `/IT Project/Gameplay.unity` with Unity.
3. Bring up the test runner by selecting `Window -- Editor Test Runner`.
4. Then click on the "run all" button, and all the test will run.

Although there are no automatic test for some parts of the project, it can be performed manually. A suggested procedure to check all the main features is:

1. Build two instances of the game, preferably on two different platforms
2. Run the two instances, log in on one device with existing account, and register a new account on the other device
3. Perform a quick match between the two instances, inspect whether spells, character movement and ground shrinking behaves normally
4. When the game finishes, save the replay and go back to the main menu
5. Go to the replay selection page, open the saved replay, and check whether the replay is correct. Pausing/Continuing and camera movement/zooming can also be checked in this stage
6. Create another quick match between the instances, try the voice chat and pausing functions
7. Save the game and load it again, check whether the game is recovered to the previous stage

Please note that, due to the lack of devices, the game is only tested on iOS platform.

## Build Instructions

1. Open `/IT Project/Gameplay.unity` with Unity
2. (Optional) In Unity, go to `File -- Build Settings` if you want to adjust the build settings, although it cannot be guaranteed that the game works in settings different to the original.
3. Select `File -- Build & Run` to build and run the project
