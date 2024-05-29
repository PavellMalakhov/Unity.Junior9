using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class Exploder : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _explosionForce = 300;
    [SerializeField] private float _explosionRadius = 4;

    public void Explode(List<Cube> cubes)
    {
        List<Rigidbody> RbCubes = new();

        foreach (var cube in RbCubes)
        {
            cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    public void ExplosionInRadius()
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

        foreach (var cube in cubes)
        {
            _explosionForce /= transform.localScale.x;

            _explosionRadius = _explosionRadius / transform.localScale.x / (cube.transform.position - transform.position).magnitude;

            cube.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
