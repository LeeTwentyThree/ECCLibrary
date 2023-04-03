﻿using System.Reflection;
using ECCLibrary.Examples;

namespace ECCLibrary;
[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
internal class ECCPlugin : BaseUnityPlugin
{
    public static ECCConfig config = OptionsPanelHandler.RegisterModOptions<ECCConfig>();

    public static ManualLogSource logger;

    private void Awake()
    {
        logger = this.Logger;

        Harmony harmony = new Harmony(PluginInfo.GUID);
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        logger.Log(LogLevel.Info, "ECCLibrary loaded!");
        
        var exampleCreature = new ExampleCreature();
        exampleCreature.Register();
    }
}