using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] private Collider _door;
    [SerializeField] private Exit _exit;
    [SerializeField] private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;
    public void Open()
    {
        _door.gameObject.SetActive(false);
        _exit.Open();
    }
}
