using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour
{
    [SerializeField] private Rigidbody _movebleTarget;

    private PlayerBodySegment[] _bodySegments;

    private void Awake()
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        _bodySegments = new PlayerBodySegment[rigidbodies.Length];
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            _bodySegments[i] = rigidbodies[i].gameObject.AddComponent<PlayerBodySegment>();
        }
    }

    private void OnEnable()
    {
        foreach (var segment in _bodySegments)
        {
            segment.Collided += OnSegmentCollided;
        }
    }

    private void OnDisable()
    {
        foreach (var segment in _bodySegments)
        {
            segment.Collided -= OnSegmentCollided;
        }
    }

    private void OnSegmentCollided(IInteractable interactable)
    {
        interactable.Interact();
        if(interactable is IMoveble moveble)
        {
            moveble.SetTarget(_movebleTarget);
        }
    }
}
