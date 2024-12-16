using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField] bool isLoop = false;
    [SerializeField]
    List<Vector3> List_Points;
    [SerializeField]
    public GameObject circlePrefab;

    protected Vector3 _originalTransformPosition;
    protected Vector3 OriginalTransformPosition => _originalTransformPosition;
    protected bool _originalTransformPositionStatus = false;
    protected bool OriginalTransformPositionStatus => _originalTransformPosition != null;

    protected virtual void Start()
    {
        Initialization();
        InstancetiateCircles();
    }


    protected virtual void Initialization()
    {
        if (List_Points == null || List_Points.Count < 1)
            return;
        if (!_originalTransformPositionStatus)
        {
            _originalTransformPositionStatus = true;
            _originalTransformPosition = transform.position;
        }

        transform.position = _originalTransformPosition;
    }

    public List<Vector3> GetPoints()
    {
        List<Vector3> tmp = new();
        for (int i = 0; i < List_Points.Count; i++)
        {
            tmp.Add(List_Points[i] + transform.position);
        }
        return tmp;
    }

    public void InstancetiateCircles()
    {
        if (circlePrefab == null) return;
        foreach (var point in List_Points)
        {
            Vector3 position = _originalTransformPosition + point;

            Instantiate(circlePrefab, position, Quaternion.identity);
        }
    }
#if UNITY_EDITOR

    protected virtual void OnDrawGizmos()
    {
        if (List_Points == null || List_Points.Count == 0)
            return;

        if (!_originalTransformPositionStatus)
        {
            _originalTransformPosition = transform.position;
            _originalTransformPositionStatus = true;
        }
        if (!Application.isPlaying && transform.hasChanged)
        {
            _originalTransformPosition = transform.position;
        }

        for (int i = 0; i < List_Points.Count; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_originalTransformPosition + List_Points[i], 0.2f);

            if (i + 1 < List_Points.Count)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(_originalTransformPosition + List_Points[i], _originalTransformPosition + List_Points[i + 1]);
            }

        }

        if (isLoop && List_Points.Count > 2)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(_originalTransformPosition + List_Points[List_Points.Count - 1], _originalTransformPosition + List_Points
[0]);
        }
    }
#endif
}
