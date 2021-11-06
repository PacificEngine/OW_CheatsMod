# Using Mod
Default value for commands

## Player Data
| Key | Description |
| :-: | --- |
`C` + `J` | Fill Fuel and Health
`C` + `H` | Toggle Helmet
`C` + `G` | Toggle Spacesuit
`C` + `1` | Toggle Training Suit
`C` + `I` | Toggle Invinciblity
`C` + `T` | Toggle Unlimited Boost
`C` + `Y` | Toggle Unlimited Fuel
`C` + `O` | Toggle Unlimited Oxygen
`C` + `U` | Toggle Unlimited Health
`C` + `T` | Toggle Unlimited Boost
`C` + `N` | Toggle Player Collision
`C` + `M` | Toggle Ship Collision

## Player Acceleration
| Key | Description |
| :-: | --- |
`P` + `-` | Decrease JetPack Acceleration
`P` + `+` | Increase JetPack Acceleration
`O` + `-` | Decrease Ship Acceleration
`O` + `+` | Increase Ship Acceleration

## Save Data
| Key | Description |
| :-: | --- |
`C` + `L` | Toggle Launch Codes
`C` + `E` | Toggle Eye Coordinates
`C` + `F` | Toggle All Frequencies (Toggles between: no signals nor frequencies, all frequencies, and all signals and frequencies)
`C` + `R` | Toggle Rumors (Toggles between: no rumors nor facts, all rumors, and rumors and facts)

## Game State
| Key | Description |
| :-: | --- |
`V` + `I` | Toggle Anglerfish AI
`V` + `O` | Toggle Inhabitants AI
`V` + `H` | Toggle Inhabitants Hostility
`V` + `0` | Toggle Supernova Timer
`V` + `-` | Decrease Supernova Timer
`V` + `+` | Increase Supernova Timer
`F` + `O` + `G` | Toggle Fog
`Q` + `0` | Change Quantum Moon Position

## Teleportation
| Key | Description |
| :-: | --- |
`T` + `1` | Teleport to 5000m above the sun
`T` + `2` | Teleport to Sun Station
`T` + `3` | Teleport to Ember Twin's North Pole
`T` + `4` | Teleport to Ash Twin's North Pole
`T` + Numpad `8` | Teleport to Ash Twin Project
`T` + `5` | Teleport to Timber Hearth Near 
`T` + Numpad `1` | Teleport to Timber Hearth Probe
`T` + `6` | Teleport to Attlerock's Observation Platform
`T` + `7` | Teleport to Brittle Hollow's Blackhole Forge
`T` + `8` | Teleport to Holow Lantern's Project Platform
`T` + `9` | Teleport to Giant's Deep Quantum Tower
`T` + Numpad `2` | Teleport to Giant's Deep Probe Cannon
`T` + Numpad `-` | Teleport to Nomai Probe
`T` + `0` | Teleport to Dark Bramble's Most Northern Point
`T` + Numpad `+` | Teleport to Vessel
`T` + Numpad `0` | Teleport to Interloper
`T` + Numpad `3` | Teleport to White Hole
`T` + Numpad `4` | Teleport to White Hole Station
`T` + Numpad `5` | Teleport to Stranger's Front Dock Area
`T` + Numpad `6` | Teleport to the Dreamworld
`T` + Numpad `7` | Teleport to the Quantum Moon
`T` + Numpad `9` | Teleport to Ship
`T` + Numpad `/` | Teleport Ship To Player
`T` + Numpad `*` | Teleport Ship To Probe

## Giving Items
| Key | Description |
| :-: | --- |
`G` + `W` | Give Player Warp Core

## Debugging
| Key | Description |
| :-: | --- |
`D` + `P` | Toggle Display of Coordinates

# Customizing Inputs
You can add multiple key input combos using `|`.
You can add multiple keys to the combo inputs using `,`.

The following options are available for customizing inputs
* Space
* Enter
* Tab
* Backquote
* Quote
* Semicolon
* Comma
* Period
* Slash
* Backslash
* LeftBracket
* RightBracket
* Minus
* Equals
* A
* B
* C
* D
* E
* F
* G
* H
* I
* J
* K
* L
* M
* N
* O
* P
* Q
* R
* S
* T
* U
* V
* W
* X
* Y
* Z
* Digit1
* Digit2
* Digit3
* Digit4
* Digit5
* Digit6
* Digit7
* Digit8
* Digit9
* Digit0
* LeftShift
* RightShift
* LeftAlt
* RightAlt
* AltGr
* LeftCtrl
* RightCtrl
* LeftMeta
* LeftWindows
* LeftApple
* LeftCommand
* RightMeta
* RightWindows
* RightApple
* RightCommand
* ContextMenu
* Escape
* LeftArrow
* RightArrow
* UpArrow
* DownArrow
* Backspace
* PageDown
* PageUp
* Home
* End
* Insert
* Delete
* CapsLock
* NumLock
* PrintScreen
* ScrollLock
* Pause
* NumpadEnter
* NumpadDivide
* NumpadMultiply
* NumpadPlus
* NumpadMinus
* NumpadPeriod
* NumpadEquals
* Numpad0
* Numpad1
* Numpad2
* Numpad3
* Numpad4
* Numpad5
* Numpad6
* Numpad7
* Numpad8
* Numpad9
* F1
* F2
* F3
* F4
* F5
* F6
* F7
* F8
* F9
* F10
* F11
* F12
* OEM1
* OEM2
* OEM3
* OEM4
* OEM5
* IMESelected111

# Creating Code
Create a new file called `PacificEngine.OW_CheatsMod.csproj.user`
```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <OuterWildsRootDirectory>$(OuterWildsDir)\Outer Wilds</OuterWildsRootDirectory>
    <OuterWildsModsDirectory>%AppData%\OuterWildsModManager\OWML\Mods</OuterWildsModsDirectory>
  </PropertyGroup>
</Project>
```
