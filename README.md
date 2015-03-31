Wardraft: Tactics - Readme
========================

## Contents
* **Introduction**

* **Installation**

* **About The Game**

* **File Architecture**

## Introduction
Wardraft: Tactics is a tactical turn-based strategy game inspired by Fire Emblem and Advance Wars. Part speed chess, part go and part poker, this revolutionary take on the genre of tile-based warfare will capture your attention and imagination. Outplay, outwit and outmanuever your enemies with a deadlier army and your superior strategy.

## Installation
1. Get the Repo with `git clone https://github.com/ericyliu/wardraft-tactics.git`
2. Download Unity 5 `http://unity3d.com/get-unity/download`
3. Install Unity and open up `samplemap.unity` in the `client/Assets` folder

## About The Game
1. Personalized Army
  - Draft against your opponents
  - Do you go for the unit that bolsters your own army? Or take away an essential piece of theirs?
2. Quick-time Events
  - Minigames when attacking
  - No more random, luck based attack values.
  - Depends on your skill and your skill only.
3. Fast paced gameplay
  - Short turn timers.
  - No one has all day to play! Think fast and be rewarded.
4. Dynamic Terrain
  - Use the land to your advantage.
  - Cut down trees, scale cliffs, burrow underground, take to the skys to outplay your enemy.

## File Architecture
- `Assets/Scripts/` All game logic
  - `Constants/` Enums and Constant settings used by the app and game
  - `Controllers/` controllers that handle player and server input and handles communication between game objects
  - `Data/` data objects storing game attributes
  - `Debug/` testing code
  - `Models/` data objects storing game state
  - `Utility/` libraries and helpers
  - `ViewModels/` scripts that interact with unity gameobjects
- `Assets/Resources` Resources to be loaded during runtime
  - `Maps/` map files in json format made using Tiled (http://www.mapeditor.org/)

