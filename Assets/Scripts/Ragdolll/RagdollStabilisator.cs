using UnityEngine;

public class RagdollStabilisator : MonoBehaviour
{
    private Rigidbody[] _rigidbodys;
    private float _elapseDalay;
    private float _timer = 0.7F;

    private void Awake()
    {
        _rigidbodys = GetComponentsInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _elapseDalay += Time.fixedDeltaTime;

        if(_elapseDalay > _timer)
        {
            _elapseDalay = 0;
            foreach (var rigidbody in _rigidbodys)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
}
