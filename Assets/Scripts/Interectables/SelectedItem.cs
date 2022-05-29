using UnityEngine;

public class SelectedItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody _rigidbody;

    public void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        _rigidbody.isKinematic = false;        
    }
}
