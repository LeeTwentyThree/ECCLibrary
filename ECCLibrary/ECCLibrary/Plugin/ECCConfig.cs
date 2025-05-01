using Nautilus.Json;
using Nautilus.Options.Attributes;

namespace ECCLibrary;

/// <summary>
/// Main and only config file for ECCLibrary.
/// </summary>
[Menu("ECCLibrary")]
public class ECCConfig : ConfigFile
{
    /// <summary>
    /// Toggle for the example shark spawn location.
    /// </summary>
    /// <remarks>
    /// Due to poor code design in the past, this is public, but please DO NOT change it.
    /// </remarks>
    [Toggle("Example shark can spawn",
        Tooltip = "If checked, the shark creature from the ECC Library example mod will spawn. Restart required.")]
    public bool EnableExampleShark;
}