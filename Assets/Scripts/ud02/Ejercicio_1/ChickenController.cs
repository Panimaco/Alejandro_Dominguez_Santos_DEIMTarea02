using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    //Zona de Variables Globales
    private Rigidbody _chickenRigidbody;
    [Header("Fuerzas")]
    [SerializeField]
    private float _speed,
                  _rotationSpeed,
                  _jumpSpeed;
    private float _vertical,
                  _horizontal;

    [Header("Animator")]
    [SerializeField]
    private Animator _anim;

    private void Awake() 
    {
        
        //Coge el "RigidBody" del objeto
        _chickenRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        Inputs();
        Move();
        Turn();
        Jump();

    }

    private void Inputs() 
    {
        //Recoge los inputs de "GetAxis" y los asigna a las variables
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
    
    }

    private void Move() 
    {

        //Si el personaje se mueve para adelante o atrás
        if (_vertical > 0) {
        
            //Lo desplaza
            transform.Translate(Vector3.forward * _speed * _vertical * Time.deltaTime);

            //Activa la animación de Andar
            _anim.SetInteger("Walk", 1);
        
        }
        else
        {

            //Desactiva la animación de Andar
            _anim.SetInteger("Walk", 0);
        
        }
    }
    private void Turn() 
    { 
    
        //Establece un float con la cantidad de rotación
        float chickenRotationAmount = _horizontal * _rotationSpeed * Time.deltaTime;
        
        //Crea un Quaternion al que le establece
        //la cantidad de rotación en "y"
        Quaternion chickenRotation = Quaternion.Euler(0, chickenRotationAmount, 0);

        //Lo rota
        _chickenRigidbody.MoveRotation(_chickenRigidbody.rotation * chickenRotation);

    }
    private void Jump() 
    {

        //Si pulsas la Barra Espaciadora
        if (Input.GetKeyDown(KeyCode.Space))
        { 
        
            //Le añade fuerza en el vector up
            _chickenRigidbody.AddForce(Vector3.up * _jumpSpeed);

            //Salta la animación de salto
            _anim.SetTrigger("jump");

        }
    }
}
