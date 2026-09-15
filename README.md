# Vehicle Tracker

Vehicle Tracker is a lightweight client mod for **Valheim** that makes sure you never lose your ride again. Aim your crosshair at any ship or cart to automatically place dynamic minimap pins that follow you as you travel. When nighttime falls or heavy ocean fog rolls in, press a single key to activate an intense glow light directly on your vehicle.

---

## Features

* **Dynamic Map Tracking** Look at any raft, karve, longship, drakkar, or cart to generate a tracking pin on your minimap.
* **Realtime Movement** Pins update position as your vehicle moves across the open seas or rough terrain.
* **Safe Cleanup** Pins automatically clear out if your boat or cart gets smashed into raw materials.
* **Full Map Control** Right click to remove pins on your map at any time. Simply hover your crosshair over the vehicle again to recreate the marker.
* **Persistence Without Duplicates** Intelligently binds back to your existing saved world pins across restarts so your map never gets cluttered.
* **Nighttime Vehicle Glow** Press the toggle hotkey (`H` by default) to cast a vibrant light source on all nearby vehicles.
* **Deep Configuration** Easily customize pin display names, toggle keybinds, and exact hex glow colors.

---

## Installation

1. Install **[BepInExPack Valheim](https://thunderstore.io/c/valheim/p/denikson/BepInExPack_Valheim/)** if you have not already.
2. Download the latest release from the **Releases** tab.
3. Extract and place `VehicleTracker.dll` inside your game directory under:
```text
Valheim/BepInEx/plugins/

```


4. Start Valheim once to let BepInEx generate your default configuration file.

---

## Configuration

Run the game once to automatically create your settings file at:

```text
Valheim/BepInEx/config/net.pinnacle.valheim.vehicletracker.cfg

```

Open the file in any text editor to adjust your settings:

```ini
[General]

## Key used to toggle vehicle glow
# Setting type: KeyboardShortcut
# Default value: H
ToggleGlowKey = H

[Names]

## Pin name for carts
# Setting type: String
# Default value: Cart
CartName = Cart

## Pin name for rafts
# Setting type: String
# Default value: Raft
RaftName = Raft

## Pin name for the karve
# Setting type: String
# Default value: Karve
KarveName = Karve

## Pin name for the longship
# Setting type: String
# Default value: Longship
LongshipName = Longship

## Pin name for the drakkar
# Setting type: String
# Default value: Drakkar
DrakkarName = Drakkar

[Colors]

## Hex color code for ship glow
# Setting type: String
# Default value: #00FFFF
ShipGlowColor = #00FFFF

## Hex color code for cart glow
# Setting type: String
# Default value: #FFD700
CartGlowColor = #FFD700

```

---

## Requirements

* **BepInExPack Valheim**

---

## Building from Source

1. Clone this repository to your machine.
2. Open the solution file in **Visual Studio**.
3. Ensure your project references `assembly_valheim.dll`, `UnityEngine.dll`, `UnityEngine.CoreModule.dll`, `Splatform.dll`, and `0Harmony20.dll` from your local game directory.
4. Target `.NET Framework 4.8` with language version `8.0`.
5. Build using the `Release` configuration to generate `VehicleTracker.dll`.
