Intended Player Experience
The game aims to immerse players in the thrill of surviving relentless waves of enemies. Quick reflexes, strategic combat, and skillful movement are essential to overcome increasingly difficult challenges and emerge as the ultimate survivor.

Game Environment Features
Grand Colosseum-style arena with towering walls and a central battlefield.
Surrounded by spectator seating with distinct areas for player spawn points and enemy entry points.
Basic architectural elements (pillars, arches, obstacles) for tactical gameplay.
Player Control Features
Movement: WASD keys for navigation.
Camera: Mouse controls for aiming and viewing direction.
Combat Actions:
Left-click for primary attacks (melee strikes or ranged shots).
Spacebar to jump.
Shift for sprinting.
Other Features
Enemies in Waves: Multiple waves of enemies with increasing difficulty.
Sword Combat Mechanics: Engage in close combat using a sword.
Hit Sound Effect: Sound feedback when hitting enemies or objects.
Collectible Experience: Experience collectibles dropped by defeated enemies.
Player Health System: Health bar indicating player damage.
Arena Navigation: Freedom of movement to avoid attacks and reposition.
External Assets
If you did not use any external assets, state this clearly here.
Scripts:

The EnemySpawner script is responsible for instantiating enemy characters in the game. It utilizes two designated spawn points to alternate enemy spawns. The spawning process begins after an initial delay and continues indefinitely at specified intervals. The script allows for dynamic gameplay by controlling the timing and location of enemy appearances in the Colosseum arena.
Key Features:
Enemy Prefab: Reference to the enemy character that will be spawned.
Spawn Points: Two defined locations where enemies can appear.
Initial Delay: Configurable time before spawning begins.
Spawn Interval: Adjustable time between successive enemy spawns.

The AIController script governs the behavior of enemy characters in the game. It enables enemies to track and engage the player, creating a dynamic combat experience. The script controls the movement speed, attack damage, and attack cooldown for each enemy, allowing them to approach the player when at a distance and initiate attacks when in close range.
Key Features:
Player Tracking: Enemies continuously face the player and move toward them if they are outside a specified minimum distance.
Attack Mechanism: Enemies can deal damage to the player when they are within a defined range, with an attack cooldown to regulate the frequency of attacks.
Damage Handling: Integrates with the player's health system, allowing for health reduction upon successful attacks.

Actor Script
The Actor script manages the health system for enemy entities. It tracks the current health of an enemy and handles damage intake, triggering a death sequence when health reaches zero.
Health Management:
maxHealth: Sets the maximum health for the enemy.
currentHealth: Tracks the enemy's current health.
TakeDamage(int amount): Reduces health based on damage taken and handles death if health drops to zero.
Death Handling: Logs a message and destroys the enemy object upon death.

The PlayerController script manages player movement, camera control, animations, and combat mechanics in the game.

Key Features:
Movement and Jumping:
Controls player movement using the CharacterController component, allowing smooth navigation around the arena.
Supports jumping mechanics with customizable jump height.
Camera Control:
Adjusts the camera view based on mouse input for a first-person perspective, enhancing the player's immersive experience.
Animation Handling:
Integrates animations for various states such as idle, walking, and attacking, using an Animator component to create a responsive and fluid animation system.
Combat Mechanics:
Implements attack functionality, allowing the player to perform melee attacks using a sword.
Features attack animations, sound effects, and damage calculation on enemy actors using raycasting to detect hits within a specified range.
Input Handling:
Utilizes the Unity Input System for responsive controls, enabling actions like movement, jumping, and attacking based on player input.

Asset: Fantasy Sword Model - 3D Model 
