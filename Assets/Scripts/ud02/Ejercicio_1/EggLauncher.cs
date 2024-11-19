using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLauncher : MonoBehaviour
{

    [Header("Eggs")]
    [SerializeField]
    private GameObject _egg;
    [SerializeField]
    private float _eggSpeed,
                  _eggForce,
                  _eggTimer;

    // Update is called once per frame
    void Update()
    {

        Launching();
    
    }
    private void Launching() 
    { 
    
        //Si se pulsa el bot�n izquierdo del rat�n
        if (Input.GetMouseButtonDown(0)) 
        {

            Debug.Log("Estoy pulsando");

            //Establece la posici�n de los huevos
            Vector3 eggPosition = transform.position;
            //Establece la rotaci�n de los huevos
            Quaternion eggRotation = transform.rotation;
            //Crea una variable "RigidBody" para los clones
            Rigidbody eggRigidBody;

            //Crea un gameobject que clona con las propiedades correctas
            GameObject eggClone = Instantiate(_egg, eggPosition, eggRotation);

            //Establece el clon al "RigidBody" creado
            eggRigidBody = eggClone.GetComponent<Rigidbody>();

            //Lo lanza hacia atr�s y un poco hacia arriba
            eggRigidBody.AddForce(-Vector3.forward * _eggSpeed);
            eggRigidBody.AddForce(Vector3.up * _eggForce);
            
            //Destruye los clones cuando pasa la
            //cantidad de tiempo establecida
            Destroy(eggClone, _eggTimer);

        }
    }
}
