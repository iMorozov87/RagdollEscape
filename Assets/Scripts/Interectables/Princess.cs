using UnityEngine;

public class Princess : Character, IMoveble
{
    [SerializeField] private CharacterMover _princesseMover; 

    public void SetTarget(Rigidbody target)
    {
        _princesseMover.SetTarget(target);
    }

    protected override void EnableStartingState()
    {
        EnableMoving();
    }

    private void EnableMoving()
    {      
        _princesseMover.enabled = true;
    }
}
