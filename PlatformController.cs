using System.Collections.Generic;
using UnityEngine;

public class PlatformControlelr : MonoBehaviour
{
    [SerializeField] float speed;

    List<Vector3> _path = new();
    Vector2 _target;
    int index = 0;
    private void Start()
    {
        _path = GetComponent<PathCreator>().GetPoints();
        _target = _path[index];
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _target) <= speed * Time.deltaTime)
        {
            index = (index + 1) % _path.Count;
            _target = _path[index];
        }
    }
}
