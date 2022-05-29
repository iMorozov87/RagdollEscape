using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private Material _deadMaterial;
    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;

    protected override void EnableStartingState()
    {
        Kill();
    }

    private void Kill()
    {
        _skinnedMesh.material = _deadMaterial;
    }
}
