using UnityEngine;

public class LoopImage : MonoBehaviour
{
    Texture _texture;
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _pixelPerUnit;
    float inGameWidth;
    private void Awake()
    {
        _texture = GetComponent<SpriteRenderer>().sprite.texture;
        inGameWidth = _texture.width / _pixelPerUnit;
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) - _playerTransform.position.x >= inGameWidth)
        {
            Vector2 pos = _playerTransform.position;
            transform.position = pos;
        }
    }
}
