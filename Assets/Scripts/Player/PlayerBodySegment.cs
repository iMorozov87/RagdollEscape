using UnityEngine;
using UnityEngine.Events;

public class PlayerBodySegment : MonoBehaviour
{
    public event UnityAction<IInteractable> Collided; 

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            Collided?.Invoke(interactable);
        }
    }
}
