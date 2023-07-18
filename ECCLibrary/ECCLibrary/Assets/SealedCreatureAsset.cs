using ECCLibrary.Data;
using System;

namespace ECCLibrary;

/// <summary>
/// An alternative to <see cref="CreatureAsset"/> that does not use inheritance. Supply a template and any needed prefab code.
/// </summary>
public sealed class SealedCreatureAsset : CreatureAsset
{
    private readonly CreatureTemplate template;
    private readonly Func<GameObject, CreatureComponents, IEnumerator> modifyPrefab;
    private readonly Action<GameObject> applyMaterials;

    /// <summary>
    /// Creates a basic Creature Asset that does not use inheritance. Supply a template and any needed prefab code. Call the Register method to add the creature to the game.
    /// </summary>
    /// <param name="prefabInfo">The unique PrefabInfo of this creature instance.</param>
    /// <param name="template">The template that contains all of the creature's data.</param>
    /// <param name="modifyPrefab">An optional call that allows for modification of the prefab after creation.</param>
    /// <param name="applyMaterials">An optional call that, if overriden, no longer applies the MarmosetUBER shader automatically and allows for full control of the materials. Called AFTER <paramref name="modifyPrefab"/>!</param>
    public SealedCreatureAsset(PrefabInfo prefabInfo, CreatureTemplate template, Func<GameObject, CreatureComponents, IEnumerator> modifyPrefab = null, Action<GameObject> applyMaterials = null) : base(prefabInfo)
    {
        this.template = template;
        this.modifyPrefab = modifyPrefab;
        this.applyMaterials = applyMaterials;
    }

    /// <summary>
    /// Handled by the class.
    /// </summary>
    /// <returns></returns>
    protected override CreatureTemplate CreateTemplate()
    {
        return template;
    }

    /// <summary>
    /// Handled by the class.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="components"></param>
    /// <returns></returns>
    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        if (modifyPrefab == null) yield break;
        yield return modifyPrefab.Invoke(prefab, components);
    }

    /// <summary>
    /// Handled by the class.
    /// </summary>
    /// <param name="prefab"></param>
    protected override void ApplyMaterials(GameObject prefab)
    {
        if (applyMaterials != null)
        {
            applyMaterials.Invoke(prefab);
        }
        else
        {
            MaterialUtils.ApplySNShaders(prefab);
        }
    }
}
