# Counter-Strike: Global Offensive TTS + Translator
Enables TTS for Counter-Strike: Global Offensive in-game chat

## Download the latest release version [here](https://github.com/SnoutBug/csgotts/releases/latest)!
 Watch [this video](https://www.youtube.com/watch?v=gQUS7W-mtOg) for a showcase!
 
## Setup:
 **-condebug** in CSGO launch options
 Start Counter-Strike Global Offensive.exe
 Browse to your **Counter-Strike Global Offensive** folder location
 Press the "Start" button
 
## Notes:

 Variable | Description
 ------------ | -------------
 Browse            | Sets the path to your **Counter-Strike Global Offensive** folder.
 Spam Timeout      | Sets, how much time needs to pass for the same message to be read again in seconds.
 Read Names        | **PlayerOne** wrote @T-Start: Hello Team!
 Use Filler        | PlayerOne **wrote** @T-Start: Hello Team!
 Read Spots        | PlayerOne wrote **@T-Start:** Hello Team!
 Combine Messages  | **PlayerOne wrote @T-Start:** gets skipped if the same player writes a new message in a specific time window.
 Region            | Set your Region, if the needed voice pack is installed (eg. en-US, ru-RU, de-DE).
 Gender            | Defines the gender of the synthesised voice (eg. male, female).
 Player Name       | Select a name from the list.
 Alias             | Select the color this player is associated with or choose "None" to keep the original name (if Use Alias and Read Names is checked).
 Mute              | Check this box to mute the selected player.
 Use Alias         | Enable whether aliases should be used or not.
 Delete            | CS:GO log files can get quite bit after some time, so if CS:GO is not running feel free to press this button.
 Refresh           | Enter **status** in the CS:GO console and press "OK" to add all players to the drop-down list.
 Auto-Translate    | Translate the message to your language (Region)
 Skip voice message| **bind "any_button_here" "!tts_skip"** inside of the CSGO console.
 
## [Donate](https://www.paypal.com/paypalme/snoutie)

## License
[MIT](https://choosealicense.com/licenses/mit/)
