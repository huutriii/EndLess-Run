public class SlideState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        player.ani.SetBool("Slide", true);
    }

    public void ExitState(PlayerController player)
    {
        player.ani.SetBool("Slide", false);
    }

    public void UpdateState(PlayerController player)
    {
    }
}
