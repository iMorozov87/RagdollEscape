using System.Collections.Generic;
using UnityEngine;

public class PathBilder : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;

    private const string LayerName = "Default";
    private Camera _camera;
    private LayerMask _filteredLayer;
    private List<Vector3> _pathPoint;

    public List<Vector3> PathPoint => _pathPoint;

    private void Awake()
    {
        _filteredLayer = LayerMask.GetMask(LayerName);
        _camera = Camera.main;
        _pathPoint = new List<Vector3>();
    }

    private void Update()
    {
        Vector3 target = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        CreatePath(_startPoint.position, target);
    }

    private void OnDisable()
    {
        _pathPoint = new List<Vector3>();
    }

    private void CreatePath(Vector3 firstPoint, Vector3 target)
    {
        _pathPoint = new List<Vector3>() { firstPoint };
        Vector2 firstDirection = target - firstPoint;
        GetNextPoint(firstPoint, firstDirection);
    }

    private void GetNextPoint(Vector3 startPoint, Vector2 direction)
    {
        float maxDistance = 1000;
        bool isHit = Physics.Raycast(startPoint, direction, out RaycastHit hit, maxDistance, _filteredLayer);
        if (isHit == false)
            return;
        Vector3 newPoint = CreateNewPoint(startPoint, hit.point, direction);
        if (hit.collider.gameObject.GetComponent<ReflectiveBlock>() == false)
            return;
        Vector2 reflectDirection = Vector2.Reflect(direction, hit.normal);
        GetNextPoint(newPoint, reflectDirection);
    }

    private Vector3 CreateNewPoint(Vector3 startPoint, Vector3 secondPoint, Vector3 direction)
    {
        float minDistance = 0.2f;
        float pointPositionOffset = 0.1f;

        Vector3 newPoint = secondPoint - direction.normalized * pointPositionOffset;
        if (Vector3.Distance(startPoint, newPoint) > minDistance)
        {
            _pathPoint.Add(newPoint);
        }
        return newPoint;
    }
}
