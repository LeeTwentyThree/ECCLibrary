﻿using UnityEngine.AddressableAssets;

namespace ECCLibrary.Data;

/// <summary>
/// Alternative to <see cref="AssetReferenceGameObject"/> that is valid for custom prefabs.
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

    /// <summary>
    /// Returns true as long as the RuntimeKey exists.
    /// </summary>
    /// <returns></returns>
    public override bool RuntimeKeyIsValid()
    {
        var key = RuntimeKey.ToString();
        return !string.IsNullOrEmpty(key);
    }
}