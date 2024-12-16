public class SneakingState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.ani.SetBool("Sneaking", true);
    }

    public void ExitState(PlayerController player)
    {
        player.ani.SetBool("Sneaking", false);
    }

    public void UpdateState(PlayerController player)
    {
    }
}
