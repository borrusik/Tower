# Tower Defense Game

![Status](https://img.shields.io/badge/Status-Completed-success)
![Language](https://img.shields.io/badge/Language-C%23-green)
![Framework](https://img.shields.io/badge/Framework-.NET%20%2F%20Windows%20Forms-purple)

This repository contains the source code for a **Tower Defense** strategy game built with C# and Windows Forms. The project was developed as a course assignment to demonstrate Object-Oriented Design (OOD) principles and **GoF Design Patterns**.

## 📋 Project Overview

The goal of the game is to defend the base from waves of incoming enemies by strategically placing defensive towers on the map.

The solution allows for easy scalability due to a clean architecture separating the Core logic from the UI:
* **TowerDefense.Core**: Contains all game logic, entities, and algorithms.
* **TowerDefense (UI)**: Handles rendering and user interaction (Windows Forms).
* **TowerDefense.Tests**: Unit tests ensuring logic stability.

## 🎮 Gameplay & Features

1.  **Enemy Types:**
    * **Fast Enemy:** Moves quickly but has low health.
    * **Tank Enemy:** Slow movement but high armor/health.
2.  **Tower Types:**
    * **Cannon Tower:** Deals direct damage using `DamageAttack` strategy.
    * **Slow Tower:** Slows down enemies within range using `SlowAttack` strategy.
3.  **Game Cycle:**
    * **Building Phase:** Player places towers on the grid.
    * **Wave Phase:** Enemies spawn and move towards the base; towers attack automatically.
    * **Game Over:** Triggered when the base health reaches zero.

## 🏗 Architecture & Design Patterns

This project heavily utilizes design patterns to manage complexity:

* **State Pattern:**
    Manages the game loop. The `GameContext` switches between `BuildState` (placement), `WaveState` (combat), and `GameOverState`.
    
* **Strategy Pattern:**
    Decouples attack algorithms from tower objects. `IAttackStrategy` allows different towers to have different behaviors (Damage vs. Slow) without altering the tower class itself.

* **Factory Method:**
    Encapsulates object creation. `EnemyFactory` and `TowerFactory` handle the instantiation of specific game entities.

* **Template Method:**
    Base classes like `GameObject` and `Tower` define the skeleton of operations, while subclasses implement specific steps.

## 🛠 Tech Stack

* **Language:** C#
* **Framework:** .NET (Windows Forms)
* **IDE:** Visual Studio 2022 / JetBrains Rider
* **Graphics:** GDI+ (with Double Buffering to prevent flickering).

## 💻 Installation & Setup

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/borrusik/tower.git](https://github.com/borrusik/tower.git)
    ```
2.  **Open the project:**
    Open `TowerDefense.slnx` in Visual Studio.
3.  **Build:**
    Press `Ctrl + Shift + B` to build the solution.
4.  **Run:**
    Set the `TowerDefense` project as the **Startup Project** and press `F5`.

## 👤 Author

**borrusik**
* GitHub: [borrusik](https://github.com/borrusik)

---
*Developed for the Object-Oriented Programming course (2nd Semester).*
