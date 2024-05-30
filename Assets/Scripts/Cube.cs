using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minValueClons;
    [SerializeField] private int _maxValueClons;
    [SerializeField] private int _chanceSplit;

    private Exploder _exploder;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
    }

    private void OnMouseUpAsButton()
    {
        int multiplier = 2;
        int chanceSplitMaxValue = 101;
        int chanceSplitMinValue = 0;

        bool isSplit = Random.Range(chanceSplitMinValue, chanceSplitMaxValue) <= _chanceSplit;
        int cubeValue = Random.Range(_minValueClons, _maxValueClons);

        if (isSplit)
        {
            List<Cube> cubes = new();
            List<Rigidbody> rigidbodyCubes = new();

            for (int i = 0; i < cubeValue; i++)
            {
                cubes.Add(Instantiate(this));
            }

            foreach (var cube in cubes)
            {
                cube.Init(multiplier);
                rigidbodyCubes.Add(cube.GetComponent<Rigidbody>());
            }

            _exploder.Explode(rigidbodyCubes);
        }
        else
        {
            _exploder.ExplodeInRadius();
        }

        Destroy(gameObject);
    }

    private void Init(int multiplier)
    {
        _chanceSplit /= multiplier;

        transform.localScale /= multiplier;

        GetComponent<MeshRenderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        float colorChannelR = Random.value;
        float colorChannelG = Random.value;
        float colorChannelB = Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
