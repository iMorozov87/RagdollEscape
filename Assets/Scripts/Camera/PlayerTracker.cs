using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private float _speed = 10;
  
    private void LateUpdate()
    {
        Vector3 target = Vector3.Lerp(transform.position, _mover.Position, _speed * Time.deltaTime);
        transform.position = new Vector3(target.x, transform.position.y, transform.position.z);
    }
}
