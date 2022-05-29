using UnityEngine;

public class HookRetainer : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public void Fix(Vector3 targetPoint)
    {
        _rigidbody.gameObject.SetActive(true);
        _rigidbody.position = new Vector3 (targetPoint.x, targetPoint.y,0);
    }

    public void Unfix()
    {
        _rigidbody.gameObject.SetActive(false);
    }
}
