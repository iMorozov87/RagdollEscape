using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private Rigidbody[] _blocks;

    private void Awake()
    {
        _blocks = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        Close();
    }

    private void Close()
    {
        SetKinematicState(true);
    }

    public void Open()
    {
        _collider.gameObject.SetActive(false);
        SetKinematicState(false);

    }

    private void SetKinematicState(bool isActive)
    {
        foreach (var block in _blocks)
        {
            block.isKinematic = isActive;
        }
    }
}
