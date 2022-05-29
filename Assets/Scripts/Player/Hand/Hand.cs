using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultSize;
    [SerializeField] private Vector3 _movingSize;

    public void SetDefaultSize()
    {
        transform.localScale = _defaultSize;
    }

    public void SetMovingSize()
    {
        transform.localScale = _movingSize;
    }
}
