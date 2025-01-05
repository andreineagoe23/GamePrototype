Welcome to Colosseum Clash!

The game immerses players in the thrill of surviving relentless waves of enemies, where quick reflexes, strategic combat, and skillful movement are essential to overcome increasingly difficult challenges and emerge as the ultimate survivor.

Game Environment Features:

Grand Colosseum-style Arena: Towering walls and a central battlefield designed for tactical combat.
Spectator Seating: Surrounds the arena, creating an immersive and intense atmosphere.
Tactical Elements: Includes pillars, arches, and obstacles to enhance strategic gameplay.

Player Control Features:

Movement: WASD keys for navigation.
Camera Control: Mouse for aiming and adjusting the viewing direction.

Combat Actions:

Left-click: Primary attacks (melee strikes).
Shift: Dash.

Additional Controls:

Weapon Switching: Press 1, 2, or 3 to switch weapons.
Spear Throw: Press E to throw a spear.
Tutorial: Press T to toggle the tutorial.
Menu: Press Esc to open the menu.

Gameplay Features:

Enemies in Waves: Multiple waves of enemies with increasing difficulty.
Sword Combat Mechanics: Engage in close combat using a sword.
Hit Sound Effects: Provides feedback upon hitting enemies or objects.
Collectible Experience: Experience orbs dropped by defeated enemies.
Player Health System: A health bar to indicate player damage.
Arena Navigation: Freedom of movement to avoid attacks and reposition strategically.

External Assets

Fantasy Sword Model: High-quality 3D model for melee combat.
Audience Crowd: Creates a lively and immersive environment.
Sun Temple: Used as a backdrop to enhance visuals.
Version Control: Ensures smooth collaboration and version management.
ProBuilder: Enables custom level design.
NavMesh AI Navigation: Implements advanced enemy pathfinding.
Shader Graph: For creating custom shaders and enhancing visual effects.
2D Sprite: Used in UI elements and effects.
Unity UI: For creating in-game menus and HUD.
Enemies: Custom enemy models and animations.
Player Model: Fully animated and rigged for seamless gameplay.

Key Scripts

EnemySpawner.cs
Handles enemy spawning at designated spawn points.
Features adjustable spawn delay and interval for dynamic gameplay.

AIController.cs
Manages enemy behavior, enabling them to track and attack the player.
Features customizable attack damage, movement speed, and attack cooldown.

Actor.cs
Manages health and damage for enemies.
Triggers death mechanics when health reaches zero.

PlayerController.cs
Controls player movement, camera, and combat mechanics.
Integrates animations, raycasting for melee attacks, and weapon switching.

WaveSpawner.cs
Coordinates enemy wave spawning with increasing difficulty.
Tracks wave progress and triggers subsequent waves.

Trigger.cs
Activates enemy spawning or events upon player interaction with specific triggers.

Enemy.cs
Controls individual enemy AI behavior.
Manages movement using NavMesh and player tracking.

TutorialPopup.cs
Displays an interactive tutorial panel in front of the player.
Adjusts dynamically based on player position and facing direction.

Colosseum Clash brings fast-paced action and strategic combat to life in a vibrant arena setting. Prepare for an epic survival challenge!
