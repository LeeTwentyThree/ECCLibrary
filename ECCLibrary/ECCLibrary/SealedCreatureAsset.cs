using ECCLibrary.Data;
using System;

namespace ECCLibrary;

/// <summary>
/// An alternative to <see cref="CreatureAsset"/> that does not use inheritance. Supply a template and any needed prefab code.
/// </summary>
public sealed class SealedCreatureAsset : CreatureAsset
{
    private CreatureTemplate template;
    private Func<GameObject, CreatureComponents, IEnumerator> modifyPrefab;
    private Action<GameObject> applyMaterials;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefabInfo"></param>
    /// <param name="template"></param>
    /// <param name="modifyPrefab"></param>
    /// <param name="applyMaterials"></param>
    public SealedCreatureAsset(PrefabInfo prefabInfo, CreatureTemplate template, Func<GameObject, CreatureComponents, IEnumerator> modifyPrefab = null, Action<GameObject> applyMaterials = null) : base(prefabInfo)
    {
        this.template = template;
        this.modifyPrefab = modifyPrefab;
        this.applyMaterials = applyMaterials;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override CreatureTemplate CreateTemplate()
    {
        return template;
    }

    /// <summary>
    /// 
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
    /// 
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
