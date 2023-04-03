namespace ECCLibrary.Data;

/// <summary>
/// Stores references to the basic components of a creature. Each field should not be expected to be assigned.
/// </summary>
public struct CreatureComponents
{
    public PrefabIdentifier prefabIdentifier;
    public TechTag techTag;
    public LargeWorldEntity largeWorldEntity;
    public EntityTag entityTag;
    public SkyApplier skyApplier;
    public Renderer renderer;
    public EcoTarget ecoTarget;
    public VFXSurface vfxSurface;
    public BehaviourLOD behaviourLOD;
    public Rigidbody rigidbody;
    public WorldForces worldForces;
    public Creature creature;
    public LiveMixin liveMixin;
    public LastTarget lastTarget;
    public SwimBehaviour swimBehaviour;
    public Locomotion locomotion;
    public SplineFollowing splineFollowing;
    public SwimRandom swimRandom;
    public InfectedMixin infectedMixin;
    public Pickupable pickupable;
    public AnimateByVelocity animateByVelocity;
    public CreatureDeath creatureDeath;
    public DeadAnimationOnEnable deadAnimationOnEnable;
}