# Alarm Against Thief

**Educational Project | Unity 6.5.7f1 | C# | SOLID, OOP**

---

## Description

This project demonstrates a **burglar alarm system** using **`Mathf.MoveTowards`** for smooth audio transitions.

**Scenario:**
- A thief named **Lesha** enters a house using a predefined waypoint path.
- Crossing the door trigger activates the **alarm sound**.
- Volume and distortion level increase smoothly to maximum.
- Lesha gets scared, pauses for 2 seconds in the center, then runs away.
- Alarm sound smoothly fades to zero.

---

## Architecture Principles

- ✅ **No `Manager` or `Controller` class names** — each class has a single, clear responsibility.
- ✅ **No magic strings or numbers** — all configurable values exposed via `[SerializeField]`. Tags replaced with `Thief` marker component.
- ✅ **Event-driven model (`event Action`)** — loose coupling between modules.
- ✅ **Single Responsibility Principle (S from SOLID)** — each class does one thing.
- ✅ **Dependency Inversion (D from SOLID)** — dependencies injected via Inspector.
- ✅ **Compile-time safety** — thief detection via `TryGetComponent<Thief>()`, not string tags.

---

## Project Structure

### Scripts (`Assets/Scripts/`)

| Script | Responsibility |
|--------|----------------|
| `Thief` | Empty marker component for identifying the thief |
| `ThiefNavigator` | Moves toward a target using `NavMeshAgent`. Fires `OnTargetReached` event |
| `ThiefJourney` | Manages waypoints, pauses, and target switching (no FSM or switch statements) |
| `HouseTrigger` | Door trigger. Detects thief entry/exit via `TryGetComponent<Thief>()`. Fires `OnThiefPresenceChanged` |
| `AlarmSound` | Controls `volume` and `distortionLevel` using `Mathf.MoveTowards` |
| `AlarmBinder` | Connects trigger and sound via event subscriptions |

---

### Scene Hierarchy

AlarmAgainstThief (Scene)
│
├── Ground (Plane, GroundMaterial)
├── Directional Light
│
├── Point_Start (empty, Z = -12)
├── Point_Center (empty, center of house)
├── Point_Exit (empty, Z = +14)
│
├── Lesha (Capsule + Thief + ThiefNavigator + ThiefJourney + NavMeshAgent)
│
├── AlarmBinder (AlarmBinder, references to trigger and sound)
│
└── House (empty container)
├── Floor
├── Wall_Back
├── Wall_Front_Left
├── Wall_Front_Right
├── Wall_Left
├── Wall_Right
├── Roof
├── AlarmTrigger (Cube, Is Trigger = true, HouseTrigger)
└── AlarmSource (AudioSource + AudioDistortionFilter + AlarmSound)

---

### Materials (`Assets/Materials/`)

| Material | Color (Albedo) | Smoothness | Use |
|----------|----------------|------------|-----|
| `HouseMaterial` | R=0.35 G=0.20 B=0.10 | 0.15 | Walls and floor |
| `RoofMaterial` | R=0.55 G=0.25 B=0.15 | 0.1 | Roof |
| `GroundMaterial` | R=0.25 G=0.55 B=0.12 | 0.0 | Grass |
| `ThiefMaterial` | R=0.85 G=0.10 B=0.10 | 0.3 | Lesha (thief) |

---

### Object Dimensions

| Object | Position | Scale |
|--------|----------|-------|
| **Ground** | `(0, -0.5, 0)` | `(3, 1, 3)` |
| **Floor** | `(0, -0.5, 0)` | `(10, 1, 10)` |
| **Wall_Back** | `(0, 1.5, -5)` | `(10, 3, 0.5)` |
| **Wall_Front_Left** | `(-1.75, 1.5, 5)` | `(3.5, 3, 0.5)` |
| **Wall_Front_Right** | `(1.75, 1.5, 5)` | `(3.5, 3, 0.5)` |
| **Wall_Left** | `(-5, 1.5, 0)` | `(0.5, 3, 10)` |
| **Wall_Right** | `(5, 1.5, 0)` | `(0.5, 3, 10)` |
| **Roof** | `(0, 3.5, 0)` | `(10.8, 0.3, 10.8)` |
| **AlarmTrigger** | `(0, 1.5, 4.8)` | `(3.5, 3, 0.5)` |
| **AlarmSource** | `(0, 2.5, -4)` | — |

---

## How It Works (Logic Flow)

```mermaid
graph TD
    A[Start: Lesha at Point_Start] --> B[Move to Point_Center]
    B --> C{Crosses trigger?}
    C -->|Yes| D[AlarmSound: SetAlarmState true]
    D --> E[Mathf.MoveTowards: volume ↑, distortion ↑]
    E --> F[Lesha reaches center]
    F --> G[Pause 2 seconds]
    G --> H[Move to Point_Exit]
    H --> I{Crosses trigger?}
    I -->|Yes| J[AlarmSound: SetAlarmState false]
    J --> K[Mathf.MoveTowards: volume ↓, distortion ↓]
    K --> L[End: Lesha stands outside]

Technologies & Requirements

| Component | Version / Description |
|-----------|-----------------------|
| Unity     | 6.5.7f1 (CoreCLR)    |
| Language  | C# (.NET Standard 2.1)|
| Build     | IL2CPP / Mono         |
| Navigation| NavMesh (Unity AI)    |
| Audio     | `AudioSource` + `AudioDistortionFilter` |
| Math      | `Mathf.MoveTowards` (guaranteed target reaching) |

Setup & Run
Clone the repository:

bash
git clone https://github.com/demonstalker86/AlarmAgainstThief.git
Open the project in Unity Hub (select Unity 6.5.7f1).

Open scene Assets/Scenes/SampleScene.unity (or create one following the instructions above).

Press Play and watch Lesha.

Future Improvements (Optional)
□ Add flashing light (Point Light with animation)
□ Add UI indicator "ALARM!" on screen
□ Add multiple houses with independent alarms
□ Implement random waypoints for Lesha
□ Replace primitives with Low Poly 3D models

Author
demonstalker86

License
Created for educational purposes. Free to use and modify.

P.S.
Lesha is waiting for you at home! 🕵️‍♂️🔊🏠

