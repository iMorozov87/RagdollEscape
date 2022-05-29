using UnityEngine;

public class PrincessSelector : MonoBehaviour
{
    [SerializeField] private PlayerLeftArmMover _playerLeftArmMover;

    private State _state = State.KeyReceived;

    private void OnTriggerEnter(Collider other)
    {
        TrySetNextState(other);      
    }

    private void TrySetNextState(Collider collider)
    {
        switch (_state)
        {
            case State.KeyReceived:
                TryUseKey(collider);
                break;
            case State.KeyUsed:
                TrySelectPrincess(collider);
                break;
        }
    }

    private void TryUseKey(Collider collider)
    {
        if (collider.TryGetComponent<Lock>(out Lock gridlock))
        {
            _playerLeftArmMover.SetTarget(gridlock.Rigidbody);
            _playerLeftArmMover.enabled = true;
            _state = State.KeyUsed;
        }
    }

    private void TrySelectPrincess(Collider collider)
    {
        if (collider.TryGetComponent<Princess>(out Princess princess))
        {
            CharacterMover mover = princess.GetComponent<CharacterMover>();     
            _playerLeftArmMover.SetTarget(mover.Effector);
            _playerLeftArmMover.enabled = true;
            _state = State.PrincessSelected;
        }
    }

    enum State
    {
        Start,
        KeyReceived,
        KeyUsed,
        PrincessSelected
    }
}
