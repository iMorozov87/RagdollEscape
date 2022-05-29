using UnityEngine;

public class HandMover : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Hand _hand;
    [SerializeField] private PathRenderer _pathRenderer;

    private Vector3[] _path;

    private void Update()
    {
        transform.position = _pathRenderer.CurrentPosition;
        transform.LookAt(_path[_pathRenderer.IndexTargetPosition]);
    }

    public void SetDirection(Vector3[] path)
    {
        int minPathLenght = 1;
        if (path == null || path.Length <= minPathLenght)
        {
            _hand.gameObject.SetActive(false);
            return;
        }

        if (path.Length > minPathLenght)
        {
            int firstPointIndex = 0;
            int secondPointIndex = 1;
            _path = path;
            _hand.gameObject.SetActive(true);
            transform.position = path[firstPointIndex];
            transform.LookAt(path[secondPointIndex]);
        }
    }

    public void StartMove(PathRenderer hookRenderer)
    {
        _pathRenderer = hookRenderer;
        enabled = true;
    }
}
