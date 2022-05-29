using UnityEngine;

public abstract class Mover : MonoBehaviour, IMoveble
{
    [SerializeField] private float _speed;

    public abstract void SetTarget(Rigidbody target);
    
    protected void MoveTargetPosition(Rigidbody effector, Vector3 target)
    {
        Vector3 position  = Vector3.MoveTowards(effector.position, target, _speed * Time.fixedDeltaTime);
        effector.MovePosition(position);
    }    
}
