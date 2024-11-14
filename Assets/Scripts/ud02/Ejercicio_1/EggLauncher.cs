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
    
        if (Input.GetMouseButtonDown(0)) 
        {

            Debug.Log("Estoy pulsando");

            Vector3 eggPosition = transform.position;
            Quaternion eggRotation = transform.rotation;
            Rigidbody eggRigidBody;

            GameObject eggClone = Instantiate(_egg, eggPosition, eggRotation);

            eggRigidBody = eggClone.GetComponent<Rigidbody>();

            eggRigidBody.AddForce(-Vector3.forward * _eggSpeed);
            eggRigidBody.AddForce(Vector3.up * _eggForce);
            
            Destroy(eggClone, _eggTimer);
        }

    }
}
