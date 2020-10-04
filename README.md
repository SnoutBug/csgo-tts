# Counter-Strike: Global Offensive TTS
Enables TTS for Counter-Strike: Global Offensive in-game chat

## Setup:
```python
 pip install halo
 pip install pyttsx3
 ```
 
 **-condebug** in CSGO launch options
 
 Start the Script with
 ```python
 python main.py
  ```
## Notes:
 Use the config.txt file to adjust the settings as you like!
 
 Variable | Description
 ------------ | -------------
 CS:GO Folder Path | (str) Path to your **Counter-Strike Global Offensive** folder
 Spam Timeout      | (int) Sets, how much time needs to pass for the same message to be read again in seconds
 Read Names        | (bol) **PlayerOne** wrote @T-Start: Hello Team!
 Player X "wrote"  | (bol) PlayerOne **wrote** @T-Start: Hello Team!
 Read Spots        | (bol) PlayerOne wrote **@T-Start:** Hello Team!
 \*Combine Messages | (bol) **PlayerOne wrote @T-Start:** gets skipped if the same player writes a new message in a specific time window
 ~~Debug~~             | ~~(bol) Prints some useful variables for debugging~~ (no longer supported in release version 0.82 and above)
 \*Region           | (str) Set your Region, if the needed voice pack is installed (eg. en-US, ru-RU, de-DE)
 \*Gender           | (str) Defines the gender of the synthesised voice (eg. male, female)
 
 \* only available in the release version 0.82 and above, since I had to rewrite the code in C# and I didn't bother updating the Python version
 
Download the latest release version [here](https://github.com/SnoutBug/csgotts/releases)!

## License
[MIT](https://choosealicense.com/licenses/mit/)
