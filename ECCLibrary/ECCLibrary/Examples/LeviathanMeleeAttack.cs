namespace ECCLibrary.Examples;

internal class LeviathanMeleeAttack : MeleeAttack
{
    public override void OnTouch(Collider collider)
    {
        base.OnTouch(collider);
        ErrorMessage.AddMessage("Chomp!");
    }
}