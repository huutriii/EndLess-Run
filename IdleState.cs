public class I_IdleSate : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.ani.SetBool("Idle", true);
    }

    public void ExitState(PlayerController player)
    {
        player.ani.SetBool("Idle", false);
    }

    public void UpdateState(PlayerController player)
    {
    }
}
