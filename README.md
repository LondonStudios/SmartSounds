# SmartSounds - London Studios
**SmartSounds** is a **FiveM** resource coded in **C#** allowing you to play custom sounds in-game to players nearby, featuring an easy to configure **"config.ini"** and the ability to just drop in any **mp3**, **wav** or **ogg** sound file.

**Example usage**: Create a doorbell sound, or a knock at a door!

This plugin is made by **LondonStudios**, we have created a variety of releases including TaserFramework, SearchHandler, ActivateAlarm and more!

Join our Discord [here](https://discord.gg/AtPt9ND).
You can now buy me a coffee [here](https://www.buymeacoffee.com/londonstudios).

![enter image description here](https://i.imgur.com/jdbOPKS.png)

## Usage
**/sound [name]** - Plays the selected sound to yourself and nearby players.
**/sounds** - Lists all available sounds

All sounds heard by nearby players reduce in volume the further away they are, the initial sound volume and the radius can all be configured in the "config.ini" file.
## Installation
Download the resource from GitHub [here](https://github.com/LondonStudios/SmartSounds).
 1.  Create a new **resource folder** on your server.
 2.  Add the contents of **"resource"** inside it. This includes:
"Client.net.dll", "Server.net.dll", "fxmanifest.lua", "index.html", "sounds", "SharpConfig.dll", "config.ini"
3. In **server.cfg**, "ensure" SmartSounds, to make it load with your server startup.

## Configuration - Adding custom sounds
This plugin can be fully configured in the "config.ini" file. If this is configured wrong, the plugin and/or certain sounds will not work and the plugin will alert you of this in-game.

    [SmartSounds]
    Commands = { doorbell, doorknock }
    SoundVolume = 0.6 # Maximum 1
    SoundRadius = 20.0 # 20.0 is recommended

1. Open up "config.ini".
2. On **line 2** you can add extra sounds here. They must be in the list otherwise they will be disabled. (See further steps required below for adding sounds).
3. On **line 3** you can adjust the sound volume, this must be lower than 1.0.
4. On **line 4** you can adjust the sound radius, we recommend keeping this at 20.0.

**Adding custom sounds:**
After you have added the command to line 2, you must specify the sound file name for each command, by creating a new section for each sound. This must be done like this:

    [doorbell]
    SoundFile = doorbell.mp3

The **[doorbell]** is the name of the command, this must match the command you entered on **line 2**.
The **SoundFile** = "doorbell.mp3" is the sound file name, the sound must be placed in "sounds" folder in the resource.
Please ensure you include the file type, this must be either "mp3", "wav", or "ogg".

This must be done for each extra command, in the example config.ini you'll see two commands created already.

## Source Code
Please find the source code in the **"src"** folder. Please ensure you follow the licence in **"LICENCE.md"**.

## Feedback
We appreciate feedback, bugs and suggestions related to SmartSounds and future plugins. We hope you enjoy using the resource and look forward to hearing from people!