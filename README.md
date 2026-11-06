# Auto Continue

A BepInEx mod for [Across the Obelisk](https://store.steampowered.com/app/1385380/Across_the_Obelisk/) that automatically presses the **Continue** button on event results.

## What it does

- Presses Continue a short, configurable time after an event result appears, using the game's own button.
- Waits while an alert, the settings screen, the card screen or the character window is open.
- In multiplayer, presses only once per event, because Continue is a ready toggle there. It doesn't press if you've already readied up, and it won't press again if you un-ready afterwards. You still wait for the other players.
- Never picks event options for you.

## Status

Tested and working in-game.

## Install

You need [BepInEx 5 for Across the Obelisk](https://thunderstore.io/c/across-the-obelisk/p/BepInEx/BepInExPack_AcrossTheObelisk/). You don't need to build anything.

1. Download [AutoContinue.dll](https://github.com/DorEydan/ato-auto-continue/releases/latest/download/AutoContinue.dll).
2. Drop it into your `BepInEx\plugins` folder.
   - **Manual BepInEx install:** the folder is inside your game folder.
   - **r2modman:** go to **Settings**, choose **Browse profile folder**, then open `BepInEx\plugins`. Launch the game with **Start modded**.
3. Start the game.

To uninstall, delete the DLL. If a game update stops the mod working, check the log at `BepInEx\LogOutput.log`.

## Build from source (optional)

Only needed if you want to change the code. This requires a .NET SDK:

```powershell
.\build.ps1 -GameDirectory "<path to Across the Obelisk>" -BepInExDirectory "<path to BepInEx folder>"
```

This writes `AutoContinue.dll` to `<BepInEx folder>\plugins\AutoContinue\`.

## Configuration

The config file `BepInEx\config\local.ato.autocontinue.cfg` is created on first launch. Restart the game after changing it.

| Setting | Default | Effect |
|---|---|---|
| `Enabled` | `true` | Turns the mod on or off |
| `DelaySeconds` | `1.5` | Seconds the result stays on screen before the press, from 0 to 30 |

## License

Free to use, modify and share under the [MIT License](LICENSE). This covers the mod's own code only, not Across the Obelisk or its files.
