using UnityEngine;
using System.Collections.Generic;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 300;
    [SerializeField] private float _explosionRadius = 4;

    public void Explode(List<Rigidbody> rigidbodyCubes)
    {
        foreach (var cube in rigidbodyCubes)
        {
            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    public void ExplodeInRadius()
    {
        Collider[] cubeshits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (var hit in cubeshits)
        {
            if (hit.attachedRigidbody != null)
            {
                cubes.Add(hit.attachedRigidbody);
            }
        }

        _explosionForce /= transform.localScale.x;

        foreach (var cube in cubes)
        {
            _explosionRadius = _explosionRadius / transform.localScale.x / (cube.transform.position - transform.position).magnitude;

            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
