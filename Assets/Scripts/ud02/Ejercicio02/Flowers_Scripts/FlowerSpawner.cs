using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private GameObject _flowerLauncher;
    [SerializeField]
    private GameObject[] _flowers = new GameObject[7];
    [SerializeField]
    private float _frontForce,
                  _upForce,
                  _timeFlower = 3.0f;
    [SerializeField]
    private int _flowerCadence = 0,
                _flowerDamage = 1;

    // Update is called once per frame
    void Update()
    {

        CreateFlowers();

    }

    private void CreateFlowers() 
    {
        if (Input.GetMouseButtonDown(0) && _flowerCadence < _flowers.Length) {

            Vector3 _flowerPosition = _flowerLauncher.transform.position;
            Quaternion _flowerRotation = _flowerLauncher.transform.rotation;
            GameObject cloneFlower = Instantiate(_flowers[_flowerCadence], _flowerPosition, _flowerRotation);

            Rigidbody rbFlowers = cloneFlower.GetComponent<Rigidbody>();

            rbFlowers.AddForce(Vector3.up * _upForce);
            rbFlowers.AddForce(transform.forward * _frontForce);

            _flowerCadence += 1;

            Destroy(cloneFlower, _timeFlower);

        }
        if (_flowerCadence >= _flowers.Length) {

            _flowerCadence = 0;

        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        
        ZombieData zombie = other.GetComponent<ZombieData>();

        if (zombie != null) 
        {

            zombie.TakeDamage(_flowerDamage);

            Destroy(gameObject);
        
        }
    }
}
