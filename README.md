# Character System

Modular character capability system for Unity with C#. The system is built on top of Unity's `CharacterController`.

This system lets you add abilities (aim, move, jump, crouch, ...) to a character in the form of components. On their own, the components do nothing: each one waits to be called from the outside (player input, an FSM, an AI, ...). This way, every script can be reused across all the characters in a game, avoiding the need to write a separate script for each character or each type of caller.

## Examples

To show what the system can do, I created 2 simple examples.

### Player

First-person character that can aim, move on the ground (including ramps), jump, crouch and dash.

<img width="480" height="229" alt="Player-Demo" src="https://github.com/user-attachments/assets/66b38d95-cfd1-448b-89f4-1cb55e2fe1e3" />

### NPC

Non-player character that patrols between a series of predetermined points. It turns toward the direction it moves, moves on surfaces with up to 30 degrees of slope, and dies when it takes damage.

<img width="480" height="236" alt="NPC-Demo" src="https://github.com/user-attachments/assets/bb97686a-6e5b-4550-bf8e-dfc2da6b6c79" />

## Demo

The repository only includes the code for the system and the physics. I have also added a Unity Package with a demo that includes all the assets and a test scene.

The demo uses code from my repositories:

- [Weapon System](https://github.com/RoniaV/WeaponSystem)
- [Unity FSM](https://github.com/RoniaV/Unity-FSM)
- [Unity Utils](https://github.com/RoniaV/Unity-Utils)

The following third-party assets are also used:

- TextMeshPro, Unity
- Weapon Stylized Pack, nappin
- Cartoon FX Remaster, JMO Assets

The demo is built with Unity 2021.3.45f2 using the Built-in Render Pipeline.

## AI Disclaimer

AI assistance was used in this repository for:

- The code comments, which were written with AI assistance.
- A final cleanup pass over the code (formatting consistency and dead code removal).

The rest of the code was written by me.
