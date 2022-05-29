using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _rigidbodys;
    [SerializeField] private Collider _collider;

    private void Awake()
    {
        _rigidbodys = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = true;
        }
    }

    public void Activate()
    {
        _collider.enabled = false;
        _animator.enabled = false;
        foreach (var rigidbody in _rigidbodys)
        {
            rigidbody.isKinematic = false;
        }
    }
}