using UnityEngine.AddressableAssets;

namespace ECCLibrary.Data;

/// <summary>
/// Alternative to <see cref="AssetReferenceGameObject"/> that is always marked as valid for custom prefabs.
/// </summary>
public class CustomGameObjectReference : AssetReferenceGameObject
{
    /// <summary>
    /// Creates a new <see cref="AssetReferenceGameObject"/>, but for a vanilla or custom prefab.
    /// </summary>
    /// <param name="guid">Can be an addressable key or ClassID.</param>
    public CustomGameObjectReference(string guid) : base(guid)
    {
    }
    
    /// <returns>Returns true as long as the RuntimeKey exists.</returns>
    public override bool RuntimeKeyIsValid()
    {
        var key = RuntimeKey.ToString();
        return !string.IsNullOrEmpty(key);
    }
}
