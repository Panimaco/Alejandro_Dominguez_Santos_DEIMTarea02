using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    //Zona de Variables Globales
    [SerializeField]
    private float _playerMoveSpeed,
                  _playerJumpForce,
                  _playerTurnSpeed;
    private float _horizontal,
                  _vertical;
    private bool _run;

    [SerializeField]
    private Rigidbody _rb;

    [Header("Animation")]
    [SerializeField]
    private Animator _anim;
    private bool _isOnAir,
                 _isCapableOfMove;
    [Header("Rayo")]
    private Ray _ray;
    [SerializeField]
    private float _rayDistance;

    void Update()
    {

        //Imputs del jugador
        ImputPlayer();
        //Movimiento del jugador
        GetMovement();
        //Salto del jugador
        GetJump();
        //Rotaci�n del jugador
        GetTurn();
        //Animaci�n de movimiento
        Move();
        //Animaci�n de saltar
        Jump();
        //Correr
        Run();

    }


    private void Move() 
    {

        //Si se est� moviendo en cualquier direcci�n
        if (_horizontal != 0 || _vertical != 0)
        {

            //Se activa la animaci�n de andar
            _anim.SetBool("IsMoving", true);

        }

        //Sin�
        else 
        {

            //Se desactiva la animaci�n de andar
            _anim.SetBool("IsMoving", false);
        
        }

    }
    private void Jump() {

        //Origen del ray
        _ray.origin = transform.position;
        //Direcci�n del ray
        _ray.direction = -transform.up;

        //Se dibuja el ray
        Debug.DrawRay(_ray.origin, _ray.direction * _rayDistance, Color.red);

        //Si el ray colisiona
        if (Physics.Raycast(_ray)) {

            //No est� en el aire
            _isOnAir = false;

        }

        //Sin�
        else 
        {

            //Est� en el aire
            _isOnAir = true;
        
        }

        //Si no est� en el aire
        if (!_isOnAir) {

            //Activa la animaci�n de salto
            _anim.SetBool("IsJumping", true);

        }
        else
        {
        
            //Desactiva la animaci�n de salto
            _anim.SetBool("IsJumping", false);
        
        }
    }
    private void Run() 
    {

        //Si est� corriendo y el movimiento es mayor a 0
        if (_run && _vertical > 0) 
        {

            //Activa la animaci�n de correr
            _anim.SetBool("IsRunning", true);
            //Aumenta la velocidad
            _playerMoveSpeed = 3f;
            //Mueve al personaje
            transform.Translate(Vector3.forward * _playerMoveSpeed * _vertical * Time.deltaTime);

        }
        //Sin�
        else 
        { 
        
            //Desactiva la animaci�n de correr
            _anim.SetBool("IsRunning", false);
            //Reestablece la velocidad normal
            _playerMoveSpeed = 2f;

        }
    
    }
    private void ImputPlayer() 
    {

        //Establece los Inputs de los "GetAxis" y del "Shift"
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        _run = Input.GetKey(KeyCode.LeftShift);

    }
    private void GetMovement() 
    {

        //Atribuye al "transform" del controlador del Mu�eco el movimiento en el eje "foward"
        //multiplicado por la velocidad puesta del jugador por el eje por el tiempo
        //haciendo as� que si no has pulsado los "axis" correctos no se mueva.

        transform.Translate(Vector3.forward * _playerMoveSpeed * _vertical * Time.deltaTime);
   
    }
    private void GetJump() 
    {

        //Si pulsas la barra espaciadora y estas en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && _isOnAir) 
        {

            //Se le atribuye una fuerza al salto ya definida
            _rb.AddForce(Vector3.up * _playerJumpForce);

        }
    }
    private void GetTurn() 
    {

        //Rotar el jugador por transform
        transform.Rotate(Vector3.up * _horizontal * _playerTurnSpeed * Time.deltaTime);
        
    }
}
