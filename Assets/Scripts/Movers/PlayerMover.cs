using UnityEngine;
using UnityEngine.Events;

public class PlayerMover : CharacterMover
{  
    private Vector3[] _path;
    private Vector3 _target;
    private Rigidbody[] _ragdollRigidbodys;
    private int _indexCurrentPoint;

    public int IndexCurrentPoint => _indexCurrentPoint;
    public Vector3 Position => Effector.position;
    public bool IsEnd = false;

    public event UnityAction PathPassed; 

    private void Awake()
    {
        _ragdollRigidbodys = GetComponentsInChildren<Rigidbody>();
    }

    public void SetPath(Vector3[] path)
    {
        _path = path;
        _indexCurrentPoint = 0;
    }

    private void FixedUpdate()
    {
        MoveTarget();
    }

    private void OnEnable()
    {
        IsEnd = false;
        StopEffectorVelocity();
        StopRagdollVelocity();        
    }

    private void MoveTarget()
    {        
        Vector2 direction = _path[_indexCurrentPoint] - Effector.position;
        MoveTowardsTheTarget(Effector, _path[_indexCurrentPoint]);
        if (Effector.position == _path[_indexCurrentPoint])
            SetTarget();        
    }

    private void SetTarget()
    {
        StopEffectorVelocity();       
        StopRagdollVelocity();        
        if (_indexCurrentPoint < _path.Length - 1)
        {
            _indexCurrentPoint++;
            return;
        }
        PathPassed?.Invoke();     
        IsEnd = true;
        enabled = false;
    }

    private void StopRagdollVelocity()
    {
        foreach (var ragdoll in _ragdollRigidbodys)
        {
            ragdoll.velocity = Vector3.zero;
            ragdoll.angularVelocity = Vector3.zero;
        }
    }
}
