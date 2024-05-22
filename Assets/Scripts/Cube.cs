using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minValueClons;
    [SerializeField] private int _maxValueClons;
    [SerializeField] private int _chanceSplit;
    [SerializeField] private Cube _cube;

    private float _explosionForce = 300;
    private float _explosionRadius = 1;
    private List<GameObject> _cubes = new();

    private void OnMouseUpAsButton()
    {
        int multiplier = 2;
        int chanceSplitMaxValue = 101;
        int chanceSplitMinValue = 0;

        bool isSplit = Random.Range(chanceSplitMinValue, chanceSplitMaxValue) <= _chanceSplit;
        int cubeValue = Random.Range(_minValueClons, _maxValueClons);

        if (isSplit)
        {
            Init(multiplier, cubeValue);
        }

        Destroy(gameObject);
    }

    private void Init(int multiplier, int cubeValue)
    {
        _cube._chanceSplit /= multiplier;

        for (int i = 0; i < cubeValue; i++)
        {
            _cubes.Add(Instantiate(gameObject));
        }

        foreach (var cube in _cubes)
        {
            cube.transform.localScale /= multiplier;

            cube.GetComponent<MeshRenderer>().material.color = GetRandomColor();

            cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private Color GetRandomColor()
    {
        float colorChannelR = Random.value;
        float colorChannelG = Random.value;
        float colorChannelB = Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
