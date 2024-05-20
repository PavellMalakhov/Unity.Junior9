using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]

public class Multiplier : MonoBehaviour
{
    [SerializeField] private int _minValueClons;
    [SerializeField] private int _maxValueClons;
    [SerializeField] private int _chanceSplit;

    private float _explosionForce = 300;
    private float _explosionRadius = 1;

    private void OnMouseUpAsButton()
    {
        int multiplier = 2;
        int chanceSplitMaxValue = 101;
        int chanceSplitMinValue = 0;

        bool isSplit = Random.Range(chanceSplitMinValue, chanceSplitMaxValue) <= _chanceSplit;
        int cubeValue = Random.Range(_minValueClons, _maxValueClons);

        List<GameObject> cubes = new();

        if (isSplit)
        {
            for (int i = 0; i < cubeValue; i++)
            {
                cubes.Add(Instantiate(gameObject));
            }
        }

        foreach (var cube in cubes)
        {
            Init(cube, multiplier);
        }

        Destroy(gameObject);
    }

    private void Init(GameObject cube, int multiplier)
    {
        cube.transform.localScale = transform.localScale / multiplier;

        cube.GetComponent<Multiplier>()._chanceSplit /= multiplier;

        cube.GetComponent<MeshRenderer>().material.color = GetColor();

        cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private Color GetColor()
    {
        float colorChannelR = Random.value;
        float colorChannelG = Random.value;
        float colorChannelB = Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
