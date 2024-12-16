using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 offset;
    private void Update()
    {
        Vector3 pos = GameManager.Instance.player.transform.position;
        pos.z = transform.position.z;

        transform.position = pos + (Vector3)offset;
    }
}
