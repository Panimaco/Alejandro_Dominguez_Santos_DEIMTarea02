using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{

    //Zona de Variables Globales
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

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private bool _isFlowerLaunching;

    [SerializeField]
    private Animator _anim;

    // Update is called once per frame
    void Update()
    {

        CreateFlowers();
        LauchFlowers();

    }

    private void LauchFlowers() 
    {

        if (_isFlowerLaunching) {

            _anim.SetTrigger("IsAttacking");

        }
    
    }
    private void CreateFlowers() 
    {

        //Si pulsamos el bot�n izquierdo del rat�n y la cadencia de flores es m�s peque�o que la
        //cantidad de flores en el array.
        if (Input.GetMouseButtonDown(0) && _flowerCadence < _flowers.Length) {

            //Se est� lanzando la flor
            _isFlowerLaunching = true;

            //Creamos la posici�n de las flores en el punto del objeto de control
            Vector3 _flowerPosition = _flowerLauncher.transform.position;

            //Creamos la rotaci�n de las flores en el punto de rotaci�n del objeto de control
            Quaternion _flowerRotation = _flowerLauncher.transform.rotation;

            //Creamos una variable "GameObject" que ser�n los clones y le decimos que cree la flor
            //que pertenezca a la lista de flores con ese n�mero de cadencia, en la posici�n y rotaci�n
            //del objeto de control.
            GameObject _cloneFlower = Instantiate(_flowers[_flowerCadence], _flowerPosition, _flowerRotation);

            //De cada flor obtenemos el "RigidBody"
            Rigidbody _rbFlowers = _cloneFlower.GetComponent<Rigidbody>();

            //Le a�adimos fuerza en el eje de las "y" de forma global y en el de la "z" de forma local
            _rbFlowers.AddForce(Vector3.up * _upForce);
            _rbFlowers.AddForce(transform.forward * _frontForce);

            //Aumentamos el n�mero de la cadencia para que avancen las flores de la lista
            _flowerCadence += 1;

            //Destruimos las flores que no impacten cuando pase el tiempo del temporizador
            Destroy(_cloneFlower, _timeFlower);

        }
        else 
        { 
        
            _isFlowerLaunching = false;
        
        }

        //Si la cadencia llega a la cantidad m�xima de flores en el lanzador, esta se "resetea"
        if (_flowerCadence >= _flowers.Length) {

            _flowerCadence = 0;

        }
    }

    private void OnTriggerEnter(Collider clon) 
    {
        
        //Una variable para el script de "Zombiedata" que consigue los componentes de "ZombieData"
        ZombieData _zombie = clon.GetComponent<ZombieData>();

        //Si el zombie existe porque no lo hemos eliminado
        if (_zombie != null) 
        {

            //Llama a la funci�n de "TakeDamage" y le "env�a" el valor de el da�o de las flores
            _zombie.TakeDamage(_flowerDamage);

            //Si la flor choca con el zombie, se destruye al instante despues de hacer da�o.
            Destroy(gameObject);
        
        }
    }
}
