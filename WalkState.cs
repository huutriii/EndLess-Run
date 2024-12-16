public class WalkState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.ani.SetBool("Walk", true);
    }

    public void ExitState(PlayerController player)
    {
        player.ani.SetBool("Walk", false);
    }

    public void UpdateState(PlayerController player)
    {
    }
}
