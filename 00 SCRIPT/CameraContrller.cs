using UnityEngine;

public class CameraContrller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;
    [SerializeField] Vector2 offset;

    void Update()
    {
        Vector3 pos = player.transform.position + (Vector3)offset;
        pos.z = Camera.main.transform.localPosition.z;
        pos.y = Camera.main.transform.localPosition.y;

        Camera.main.transform.position = pos;
    }
}
