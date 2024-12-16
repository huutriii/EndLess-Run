using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float InputX { get; private set; }
    public bool JumpPress { get; private set; }
    public bool SlidePress { get; private set; }
    public bool SneakingPress { get; private set; }

    private void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        JumpPress = Input.GetKeyDown(KeyCode.Space);
        SlidePress = Input.GetKeyDown(KeyCode.S);
        SneakingPress = Input.GetKeyDown(KeyCode.X);
    }
}
