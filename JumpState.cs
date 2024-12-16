public class JumpState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.ani.SetBool("Jump", true);
    }

    public void ExitState(PlayerController player)
    {
        player.ani.SetBool("Jump", false);
    }

    public void UpdateState(PlayerController player)
    {
    }
}
