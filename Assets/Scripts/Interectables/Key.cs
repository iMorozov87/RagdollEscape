using UnityEngine;

public class Key : SelectedItem, IMoveble
{
    [SerializeField] private HingeJoint _hingeJoint;

    public void SetTarget(Rigidbody target)
    {
        _hingeJoint.connectedBody = target;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Lock gridLock))
        {
            gridLock.Open();
            gameObject.SetActive(false);
        }
    }
}
