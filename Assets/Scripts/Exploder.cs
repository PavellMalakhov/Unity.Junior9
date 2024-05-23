using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Exploder : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _explosionForce = 300;
    [SerializeField] private float _explosionRadius = 1;

    public void Explode()
    {
        _rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}
