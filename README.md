Wardraft: Tactics - Readme
========================

## Contents
* **Introduction**

* **About The Game**

* **File Architecture**

## Introduction
Wardraft: Tactics is a tactical turn-based strategy game inspired by Fire Emblem and Advance Wars. Part speed chess, part go and part poker, this revolutionary take on the genre of tile-based warfare will capture your attention and imagination. Outplay, outwit and outmanuever your enemies with a deadlier army and your superior strategy.

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
- `Assets/Scripts`
  - `/Constants` Enums and Constant settings used by the app and game
  - `/Controllers` part of MVC, controllers that handle player and server input
  - `/Debug` testing code
  - `/Managers` managers that dictate the flow of the app
  - `/Models` part of MVC
    - `/Abilities` abilities used by units and buildings
    - `/Actors` any entity inside the game
      - `/Buildings` building types
      - `/Doodads` doodad types
      - `/Units` unit types
    - `/Application` models describing the app
    - `/Attributes` unit and building stats
    - `/Game` models describing the game
    - `/Map` models describing the map and terrain
      - `/Terrain` terrain types
  - `Utility` libraries and helpers
  - `Views` part of MVC, what the player sees
- `Assets/Resources`
  - `/Maps` map files in json format made using Tiled (http://www.mapeditor.org/)
  - `/Models` 3d models of game objects
    - `/Terrain`
    - `/Units`
    - `/Buildings`
    - `/FX`
  - `/Textures` textures for game objects
    - `/Terrain`
    - `/Units`
    - `/Buildings`
    - `/FX`
    - `/UI`
      - `/Menus`
      - `/Icons`
  - `/Sounds` sounds of game objects
    - `/Units`
    - `/Buildings`
    - `/FX`

