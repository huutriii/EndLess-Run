public class RunState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.ani.SetBool("Run", true);
    }

    public void ExitState(PlayerController player)
    {
        player.ani.SetBool("Run", false);
    }

    public void UpdateState(PlayerController player)
    {
    }
}
