# Card Matching Game Prototype

## Overview
A memory card matching game prototype built using **Unity 2021 LTS**.  
The implementation focuses on gameplay mechanics, requirement fulfillment, and clean code structure.

## Run Instructions
1. Clone the repository.
2. Open the project using **Unity 2021 LTS**.
3. Open the scene located at: **"Assets/Scenes/SampleScene.unity"**
4. Press **Play** in the Unity Editor to start the game.

## Features

- Card matching gameplay
- Smooth card flip animation
- Continuous card flipping without blocking input during comparisons
- Multiple board layouts (2x2, 2x3, 4x4, 5x6)
- Automatic card grid scaling within the game panel
- Turn-based scoring system
- Save and load game progress
- Game over detection when all pairs are matched
- Sound effects for:
  - Card flip
  - Match
  - Mismatch
  - Game over
- Clean modular code structure suitable for team development

## Save System
Game state is stored using **PlayerPrefs with JSON serialization**, allowing players to resume their last session.
