using UnityEngine;

public abstract class Character : MonoBehaviour, IInteractable
{
    [SerializeField] private RagdollActivator _ragdollActivator;

    public void Interact()
    {
        RagdollActivate();
        EnableStartingState();
    }

    protected abstract void EnableStartingState();

    private void RagdollActivate()
    {
        _ragdollActivator.Activate();
    }
}
