using UnityEngine;

public class Multiplier : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private int _minValueClons;
    [SerializeField] private int _maxValueClons;

    private float _explosionRadius = 1;
    private float _explosionForce = 300;

    private void OnMouseUpAsButton()
    {
        int multiplier = 2;
        Vector3 size = gameObject.transform.localScale / multiplier;

        for (int i = 0; i < Random.Range(_minValueClons, _maxValueClons + 1); i++)
        {
            _cubePrefab.transform.localScale = size;

            Instantiate(_cubePrefab);
        }

        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, size.x / multiplier);

        foreach (var item in hits)
        {
            item.GetComponent<Multiplier>()._maxValueClons /= multiplier;
            item.GetComponent<Multiplier>()._minValueClons /= multiplier;

            item.gameObject.GetComponent<MeshRenderer>().material.color = GetColor();

            item.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Destroy(gameObject);
    }

    private Color GetColor()
    {
        float colorChannelR = Random.value;
        float colorChannelG = Random.value;
        float colorChannelB = Random.value;

        return new Color(colorChannelR, colorChannelG, colorChannelB);
    }
}
