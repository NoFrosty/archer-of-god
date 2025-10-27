# Archer Of God

## Overview

Archer Of God is a Unity-based action game where players control an archer character with various skills, status effects, and combat mechanics. The game features a modular architecture with separate assemblies for different gameplay systems.

## Requirements

- Unity 6000.2.9 or later
- Universal Render Pipeline

## Getting Started

1. Clone the repository
2. Open the project in Unity
3. Ensure all required packages are installed via Package Manager
4. Open the `Game` scene from `Assets/Scenes`
5. Press Play to start

## Videos
Video after Day 1 of Development: 
- https://youtu.be/nWHzvxsguC4

Video after Day 2 of Development (Final Version):
- https://youtu.be/dpY4HoswlS0

## Features

### Combat System
- Arrow shooting mechanics with object pooling
- Enemy AI and behavior
- Skill system
- Shield mechanics
- Status System
- Faction system

### Status Effects
- **Burn Effect** - Fire-based damage over time
- **Cold Effect** - Slow and freeze mechanics
- **Push Back Effect** - Knockback mechanics

### Switch AI Behavior
The AI ​​has several difficulty levels (Aggressive, Balanced, Defensive), which can be changed in the prefab.
> "P_Character_Enemy" > "EnemyAIController" > "AI Config".

This setting only affects skill cooldowns, execution order, and timers.

### Skills
- Modular skill system for abilities and special attacks

## Assets Used

The project incorporates several Unity Asset Store packages:

- [GUI Parts](https://assetstore.unity.com/packages/p/gui-parts-159068)
- [Miniature Army 2D V.1 [Medieval Style]](https://assetstore.unity.com/packages/2d/characters/miniature-army-2d-v-1-medieval-style-72935)
- [Pixel Art Platformer - Village Props](https://assetstore.unity.com/packages/2d/environments/pixel-art-platformer-village-props-166114)
- [Pixel Art RPG VFX Lite](https://assetstore.unity.com/packages/vfx/particles/pixel-art-rpg-vfx-lite-311145)
- [Pixel Cursors](https://assetstore.unity.com/packages/2d/gui/icons/pixel-cursors-109256)

## License

Please ensure compliance with all Unity Asset Store licenses for included third-party assets.
