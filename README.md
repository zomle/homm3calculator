# HOMM3 Hota calculator

Purpose of the app is to help with guard calculations in Heroes of Might and Magic 3 Horn of the Abyss random templates. 
The calculations are based solely on user input, hard coded AI values and well-known formulas from The Heroes 3 wiki (no game memory access or any kind of *cheating* is used):
* [Template editor](https://heroes.thelazy.net//index.php/Template_Editor#About_Objects_and_Guards)
* [List of creatures (HotA)](https://heroes.thelazy.net/index.php/List_of_creatures_(HotA))

## Download

The program can be downloaded from here (Homm3Calculator_v1.0.0.zip): https://github.com/zomle/homm3calculator/releases 

## Getting Started

The program requires no installation whatsoever, it can be extracted and started, as long as the prerequisites are met.

### Prerequisites

The program uses .NET 8.0 and WPF, so the [.NET Desktup Runtime 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) 
is required: .NET Desktop Runtime 8.0 to run apps.

### How to use

After starting the program:
* Select *Monster Strength* used when setting up the random template before the game started.
* Enter the current *Week*.
* Select the *Monster Strength in Zone* of the guard (usually represented by the number of swords on the zone on template images).
* If a ZoneGuard was selected, enter the *Zone Guard Value* (often 3000, 6000, 9000, 12500, 45000, etc.), in this case the Map Objects are irrelevant.
* Otherwise select the *Map Objects* that are guarded.
* If a creature dwelling was selected, the total *Number of zones with towns* has to be entered, and *Number of xy zones*, where xy is the same faction as the creature dwelling. This is required, because the AI value of dwellings depends on these values. If the number of specific faction zones are unknown, then guess...
* Select the guard.
* The estimated number of guard will appear.

![Main screen](https://github.com/zomle/homm3calculator/raw/master/Resources/scr1_main.png)

**Filters:**

It is possible to type into some of the dropdown lists, to filter the values for easier setup:

![Filters in use](https://github.com/zomle/homm3calculator/raw/master/Resources/scr2_filter.png)

**Presets:**

The program contains a **Presets.json** file, which contains presets for your favourite templates, for easier setup. The file is read at startup, but can also be edited while the program is running, it can be reloaded with the **Reload Presets** button. 

Accidents happen, so worry not if the *Presets.json* file gets deleted or corrupted: the **Regenerate presets file** button will make a backup of the existing file if exists, and restores the original *Presets.json* file.

![Presets](https://github.com/zomle/homm3calculator/raw/master/Resources/scr3_preset.png)

**Guess the object:**

In some cases it's unambiguous what kind of object is guarded, for example in case of a Pandora's box. 

If you select a map object with a *(GUESS)* suffix, it's AI value will be ???. Go ahead and select the other 
guarded map objects, monster strengths, week, etc., and also select how many guards are guarding the objects. 
The possible unknown object types will be listed.

![Guess the object](https://github.com/zomle/homm3calculator/raw/master/Resources/scr4_guess.png)

Both a range (few, lots, horde, etc.) and an exact number can be entered. The exact number will be known either with 
visions or after the fight.

![Guess the object 2](https://github.com/zomle/homm3calculator/raw/master/Resources/scr5_guess2.png)

### Shortcomings

* **It can't handle multiple unknown objects.** There is no technical difficulty in guessing multiple objects, but the 
number of possible combinations are usually too big, and it would be practically pretty useless. Also the UI would 
have to be redesigned to be able to display a large number of combinations in a user-friendly way.

* **It can't guess Pandora's boxes, with monsters.** The reason is that with default random template settings there are too 
many variables that decide which faction the monster would belong to, which tier the monster would be, and how many 
monsters the box would contain. It would be practically close to impossible to guess a monster box properly.

* **It can't handle non-default AI values.** The reason is that it would complicate the UI too much to be able to set custom
AI values for any Map Object. This is a known issue for the Clash of Dragons template for example, where air control spells 
scrolls have non-default value.

* **Poor code quality.** Started small and later didn't bother to separate the logic from the UI, neither did I spend 
time using a proper desktop client architecture. Oh well :)

* ???

## Development/Contribution

This is a hobby project of mine, and as such I have limited time to work on it, hence the reason why it's made open-source.
Feel free to report issues, suggest features, create pull requests, etc. I'll try to deal with everything in a timely 
manner :)

### Development environment

* I used *Visual Studio 2022 Community Edition* for development and WPF on *.NET 8.0*. 
* I haven't used any fancy features, so it should work without any issues with older environments, like *Visual Studio 
2015 .NET Framework 4*, if the solution and WPF project files are (re)created.

## Authors

* **zomle** - *Initial work* - https://github.com/zomle

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Thanks for all the infor on the [Heroes 3 wiki](https://heroes.thelazy.net/). 
