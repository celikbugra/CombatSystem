# CombatSystem

A console-based combat system written in C# and .NET.

The project implements object-oriented game logic through characters, weapons,
damage handling, character states, companions and item systems.

## Features

- Character hierarchy using abstract base classes and inheritance
- Player and enemy combat
- Weapon system with different weapon types
- Health and damage handling
- Character and health states
- Companion healing and transformation logic
- Player and enemy attack streak mechanics
- Attack history tracking
- Item and potion classes
- Self-attack protection
- Custom test runner for validating core gameplay logic

## Project Structure

```text
CombatSystem/
├── Characters/
│   ├── Character.cs
│   ├── Player/
│   │   ├── Player.cs
│   │   └── Geralt.cs
│   ├── Enemy/
│   │   ├── Enemy.cs
│   │   └── Vampire.cs
│   └── Companion/
│       ├── Companion.cs
│       └── Helper.cs
├── Combat/
│   └── Weapon.cs
├── Core/
│   ├── Config.cs
│   ├── Interfaces.cs
│   └── States.cs
├── Items/
│   └── Item.cs
├── test/
│   └── TestRunner.cs
├── Program.cs
└── CombatSystem.csproj
```

## Architecture

The project is organized into several namespaces:

- `CombatSystem.Characters` - characters, players, enemies and companions
- `CombatSystem.Combat` - weapons and combat-related types
- `CombatSystem.Core` - shared interfaces, states and combat history
- `CombatSystem.Items` - items and potion types
- `CombatSystem.Tests` - custom test runner

The combat system uses interfaces and abstract base classes to keep common
behavior reusable while allowing individual characters and weapons to provide
their own implementations.

## Running the Project

Requires the .NET 9 SDK.

Build the project:

```bash
dotnet build
```

Run the combat demo:

```bash
dotnet run
```

Run the custom test suite:

```bash
dotnet run -- --test
```

## Technologies

- C#
- .NET 9
- Object-oriented programming