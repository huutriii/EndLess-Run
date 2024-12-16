public class SitDownState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.ani.SetBool("SitDown", true);
    }

    public void ExitState(PlayerController player)
    {
        player.ani.SetBool("SitDown", false);
    }

    public void UpdateState(PlayerController player)
    {
    }
}
