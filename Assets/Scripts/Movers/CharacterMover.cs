using UnityEngine;

public class CharacterMover : Mover
{
    [SerializeField] private Rigidbody _effector;
    
    private Rigidbody _target;

    public Rigidbody Effector => _effector;
    public Rigidbody Target => _target;

    public override void SetTarget(Rigidbody target)
    {
        _target = target; 
    }

    private void FixedUpdate()
    {
        MoveTowardsTheTarget(_effector, _target.position);
    }

    protected void MoveTowardsTheTarget(Rigidbody effector, Vector3 position)
    {
        StopEffectorVelocity();
        MoveTargetPosition(effector, position);       
    }

    protected void StopEffectorVelocity()
    {
        _effector.velocity = Vector3.zero;
        _effector.angularVelocity = Vector3.zero;
    }
}
