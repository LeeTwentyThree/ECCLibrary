namespace ECCLibrary.Mono;

internal class SwimInSchoolFieldSetter : MonoBehaviour
{
    public SwimInSchool behaviour;
    public float breakDistance;
    public float percentFindLeaderRespond;
    public float chanceLoseLeader;

    private void Start()
    {
        if (behaviour != null)
        {
            behaviour.kBreakDistance = breakDistance;
            behaviour.percentFindLeaderRespond = percentFindLeaderRespond;
            behaviour.chanceLoseLeader = chanceLoseLeader;
        }
    }
}
