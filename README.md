# Auto Continue

A BepInEx mod for [Across the Obelisk](https://store.steampowered.com/app/1385380/Across_the_Obelisk/) that automatically presses the **Continue** button on event results.

## What it does

- Presses Continue a short, configurable time after an event result appears, using the game's own button.
- Waits while an alert, the settings screen, the card screen or the character window is open.
- In multiplayer, presses only once per event, because Continue is a ready toggle there. It doesn't press if you've already readied up, and it won't press again if you un-ready afterwards. You still wait for the other players.
- Never picks event options for you.

## Status

Compiles against the game's assemblies, but has not yet been confirmed in-game.

## Requirements

- [BepInEx 5](https://thunderstore.io/c/across-the-obelisk/p/BepInEx/BepInExPack_AcrossTheObelisk/) for Across the Obelisk
- A .NET SDK, to build from source

## Build and install

No compiled DLL is included, so build it against your own copy of the game:

```powershell
.\build.ps1 -GameDirectory "<path to Across the Obelisk>" -BepInExDirectory "<path to BepInEx folder>"
```

This writes `AutoContinue.dll` to `<BepInEx folder>\plugins\AutoContinue\`. If you use r2modman, the BepInEx folder is inside your profile; launch the game with **Start modded**.

## Configuration

The config file `BepInEx\config\local.ato.autocontinue.cfg` is created on first launch. Restart the game after changing it.

| Setting | Default | Effect |
|---|---|---|
| `Enabled` | `true` | Turns the mod on or off |
| `DelaySeconds` | `1.5` | Seconds the result stays on screen before the press, from 0 to 30 |
