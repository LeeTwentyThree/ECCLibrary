using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace ECCLibrary.Data;
/// <summary>
/// Helps in the creation of a <see cref="TrailManager"/>. Call the <see cref="Complete"/> method to apply the changes.
/// </summary>
public class TrailManagerBuilder
{
    /// <summary>
    /// Helps in the creation of a <see cref="TrailManager"/>. Call the <see cref="Complete"/> method to apply the changes.
    /// </summary>
    /// <param name="creatureRoot">The root of the creature. Typically the object that holds the Creature component and all CreatureActions.</param>
    /// <param name="rootSegment"> The object that the TrailManager is added to. Is generally a part of the rig. This bone does NOT get animated, and should NOT be included in the list of trail bones.</param>
    /// <param name="segmentSnapSpeed">Controls rigidity. The higher this value, the faster the TrailManager can revert to its default (stiff) state. The lower this value, the more "floaty" it appears.</param>
    /// <param name="maxSegmentOffset">If -1 (default), there is no limit on how far each segment can go. Otherwise, this value forces each bone to remain within a certain distance of its starting point.</param>
    public TrailManagerBuilder(Transform creatureRoot, Transform rootSegment, float segmentSnapSpeed = 5f, float maxSegmentOffset = -1f)
    {
        CreatureRoot = creatureRoot;
        RootSegment = rootSegment;
        SegmentSnapSpeed = segmentSnapSpeed;
        MaxSegmentOffset = maxSegmentOffset;
    }

    /// <summary>
    /// The root of the creature. Typically the object that holds the Creature component and all CreatureActions.
    /// </summary>
    public Transform CreatureRoot { get; set; }

    /// <summary>
    /// The object that the TrailManager is added to. Is generally a part of the rig. This bone does NOT get animated, and should NOT be included in the list of trail bones.
    /// </summary>
    public Transform RootSegment { get; set; }

    /// <summary>
    /// The array that contains all of the transforms of the affected bones.
    /// </summary>
    public Transform[] Trails { get; set; } = new Transform[0];

    /// <summary>
    /// Controls rigidity. The higher this value, the faster the TrailManager can revert to its default (stiff) state. The lower this value, the more "floaty" it appears.
    /// </summary>
    public float SegmentSnapSpeed { get; set; } = 5f;

    /// <summary>
    /// If -1 (default), there is no limit on how far each segment can go. Otherwise, this value forces each bone to remain within a certain distance of its starting point.
    /// </summary>
    public float MaxSegmentOffset { get; set; } = -1f;

    /// <summary>
    /// Multiplier for the intensity of each bone's trail effect. The lowest time value (t=0) affects the first bone while the highest time value (t=1) affects the last bone. Default value is <see cref="TrailManagerUtilities.FlatMultiplierAnimationCurve"/>.
    /// </summary>
    public AnimationCurve PitchMultiplier { get; set; } = TrailManagerUtilities.FlatMultiplierAnimationCurve;

    /// <summary>
    /// Multiplier for the intensity of each bone's trail effect. The lowest time value (t=0) affects the first bone while the highest time value (t=1) affects the last bone. Default value is <see cref="TrailManagerUtilities.FlatMultiplierAnimationCurve"/>.
    /// </summary>
    public AnimationCurve RollMultiplier { get; set; } = TrailManagerUtilities.FlatMultiplierAnimationCurve;

    /// <summary>
    /// Multiplier for the intensity of each bone's trail effect. The lowest time value (t=0) affects the first bone while the highest time value (t=1) affects the last bone. Default value is <see cref="TrailManagerUtilities.FlatMultiplierAnimationCurve"/>.
    /// </summary>
    public AnimationCurve YawMultiplier { get; set; } = TrailManagerUtilities.FlatMultiplierAnimationCurve;

    /// <summary>
    /// Sets the multiplier in every direction to <paramref name="curve"/>.
    /// </summary>
    public void SetAllMultiplierAnimationCurves(AnimationCurve curve)
    {
        PitchMultiplier = curve;
        RollMultiplier = curve;
        YawMultiplier = curve;
    }

    /// <summary>
    /// Forces the Trails array to contain every single child of the <see cref="RootSegment"/>. Does not work well when the creature has fins or any sort of bone structure in the spine that isn't meant to be 100% animated.
    /// </summary>
    public void SetTrailArrayToAllChildren()
    {
        var transforms = new List<Transform>(RootSegment.GetComponentsInChildren<Transform>());
        transforms.Remove(RootSegment);
        Trails = transforms.ToArray();
    }

    /// <summary>
    /// Fills the Trails array with every child of the RootSegment (recursive) that contains <paramref name="keyword"/> in its name (case insensitive). Ordered from parent to child, top to bottom.
    /// </summary>
    public void SetTrailArrayToChildrenWithKeywords(string keyword)
    {
        var keywordLower = keyword.ToLower();
        var allTransforms = RootSegment.GetComponentsInChildren<Transform>();
        var trailTransforms = new List<Transform>();
        foreach (var transform in allTransforms)
        {
            if (transform != RootSegment && transform.name.ToLower().Contains(keywordLower))
            {
                trailTransforms.Add(transform);
            }
        }
        Trails = trailTransforms.ToArray();
    }

    /// <summary>
    /// Fills the Trails array with every child (recursive) of the <see cref="RootSegment"/> that contains 'phys' in its name (case insensitive). Ordered from parent to child, top to bottom.
    /// </summary>
    public void SetTrailArrayToPhysBones()
    {
        SetTrailArrayToChildrenWithKeywords("phys");
    }

    /// <summary>
    /// Finalizes creation of this TrailManager.
    /// </summary>
    public TrailManager Complete(CreatureComponents components)
    {
        return Complete(components.BehaviourLOD);
    }

    /// <summary>
    /// Finalizes creation of this TrailManager.
    /// </summary>
    public TrailManager Complete(BehaviourLOD lod)
    {
        var tm = RootSegment.gameObject.AddComponent<TrailManager>();
        tm.rootSegment = RootSegment;
        tm.rootTransform = CreatureRoot;
        tm.segmentSnapSpeed = SegmentSnapSpeed;
        tm.maxSegmentOffset = MaxSegmentOffset;
        tm.trails = Trails;
        tm.pitchMultiplier = PitchMultiplier;
        tm.rollMultiplier = RollMultiplier;
        tm.yawMultiplier = YawMultiplier;
        tm.levelOfDetail = lod;
        return tm;
    }
}