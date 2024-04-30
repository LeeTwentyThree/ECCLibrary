# ECCLibrary 2.0

ECC Library is a modding API for Subnautica and Subnautica: Below Zero that allows you to add fleshed-out creatures to the game without needing to reinvent the wheel each time, saving you hundreds of lines of code and hours of research.

ECC Library 2.0 is a complete rewrite of the original library, created in response to the "modpocalypse" update.
The [old version of ECCLibrary](https://github.com/LeeTwentyThree/ECCLibrary-Legacy) is no longer supported.
ECC 2.0 converts the inheritance-forcing creature data system to a more robust object-oriented system called [Creature Templates](https://github.com/LeeTwentyThree/ECCLibrary/blob/main/ECCLibrary/ECCLibrary/Data/CreatureTemplate.cs).

### Custom creature examples

An example of a creature asset class in ECC 2.0 can be found [here](https://github.com/LeeTwentyThree/ECCLibrary/blob/main/ECCLibrary/ECCLibrary/Examples/ExampleCreature.cs). Note that this example is much more complicated than it needs to be because I construct the prefab entirely through code instead of exporting a model out of the Unity Editor.

Creatures can also be created [without using inheritance](https://github.com/LeeTwentyThree/ECCLibrary/blob/main/ECCLibrary/ECCLibrary/Examples/ExamplePatcher.cs#L35-L55). This example is cleaner than the previous one because the prefab is defined in Unity.

[De-Extinction for SN1 and Below Zero](https://github.com/LeeTwentyThree/SubnauticaMods/tree/main/DeExtinction) is also a great example with many fish ranging from small edible creatures to leviathans.
- [Filtorb](https://github.com/LeeTwentyThree/SubnauticaMods/blob/main/DeExtinction/Prefabs/Creatures/FiltorbPrefab.cs)
- [Gulper Leviathan](https://github.com/LeeTwentyThree/SubnauticaMods/blob/main/DeExtinction/Prefabs/Creatures/GulperLeviathanPrefab.cs)

The XML documentation within the library is very elaborate and should provide sufficient information for setting up basic creature behaviours.

## Requirements

[Nautilus](https://github.com/SubnauticaModding/Nautilus), the new Subnautica Modding API.

## Download

Please check the releases tab on the right side of this page to find the latest version.

## Contributing to ECCLibrary

ECCLibrary is an open-source mod and contributions are always welcome. If you are interested in contributing to the project, there are several ways to do so.

One way to contribute is to submit bug reports and feature requests through the issue tracker on GitHub. This helps the ECCLibrary team understand what features, changes and/or fixes are needed.

If you would like to build the project, follow the steps below to set up the environment:
1. Fork or clone the repository.
2. ~~Open the `ECCLibrary.csproj` file with an appropriate IDE (Visual Studio, Rider, etc.,).~~
3. ~~Locate the `GameDir.targets` file, open it, and replace the GameDir path with the correct path to your Subnautica folder.~~
4. ~~Ensure the NuGet packages are loaded correctly. You may need to right click the Solution at the top of the Solution Explorer and choose `Restore NuGet Packages`.~~
5. ~~Build the solution. A folder named ECCLibrary should be automatically placed in your BepInEx `plugins` folder.~~
   - ~~If that does not work, double check the Nautilus reference path in the csproj file.~~
  
The project structure is being changed so currently you have to manually copy the file. Sorry for the confusion!
