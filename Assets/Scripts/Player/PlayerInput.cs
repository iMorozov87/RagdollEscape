using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _player.BuildTrajectoryMovement();
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            _player.ThrowHook();
        }
    }
}
