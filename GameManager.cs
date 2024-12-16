using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;
    public static GameManager Instance => _Instance;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            return;
        }
        if (Instance.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            Destroy(gameObject);
    }

    [SerializeField] PlayerController _player;
    public PlayerController player => _player;
}
