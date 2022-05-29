using UnityEngine;

public class Bomb : MonoBehaviour, IInteractable
{
    [SerializeField] private ParticleSystem _explosionEffect;

    public void Interact()
    {
        Explode();
    }

    private void Explode()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Collider[] explodeResault = Physics.OverlapSphere(transform.position, 1.5f);
        foreach (var collider in explodeResault)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
