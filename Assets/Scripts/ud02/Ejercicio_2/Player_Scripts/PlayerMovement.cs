using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    //Zona de Variables Globales
    [SerializeField]
    private float _playerMoveSpeed,     //Velocidad de Movimiento del Mu�eco
                  _playerJumpForce,     //Fuerza de Salto del Mu�eco
                  _playerTurnSpeed;     //Velocidad de Rotaci�n del Mu�eco
    private float _horizontal,          //Imput de eje Horizontal
                  _vertical;            //Imput de eje Vertical
    private bool _run;

    [SerializeField]
    private Rigidbody _rb;              //Rigidbody del Mu�eco para el salto

    [Header("Animation")]
    [SerializeField]
    private Animator _anim;
    private bool _isOnGround,
                 _isCapableOfMove;
    [Header("Rayo")]
    private Ray _ray;
    [SerializeField]
    private float _rayDistance;

    void Update()
    {

        ImputPlayer();                              //Imputs del jugador
        GetMovement();                              //Movimiento del jugador
        GetJump();                                  //Salto del jugador
        GetTurn();                                  //Rotaci�n del jugador
        Move();                                     //Animaci�n de movimiento
        Jump();                                     //Animaci�n de saltar
        Run();                                      //Correr

        Debug.Log(_run);
    }


    private void Move() 
    {

        if (_horizontal != 0 || _vertical != 0)
        {

            _anim.SetBool("IsMoving", true);

        }

        else 
        { 
        
            _anim.SetBool("IsMoving", false);
        
        }

    }
    private void Jump() {

        _ray.origin = transform.position;
        _ray.direction = -transform.up;

        Debug.DrawRay(_ray.origin, _ray.direction * _rayDistance, Color.red);

        if (Physics.Raycast(_ray)) {

            _isOnGround = false;

        }
        else 
        {

            _isOnGround = true;
        
        }
        if (!_isOnGround) {

            _anim.SetBool("IsJumping", true);

        }
        else
        {
        
            _anim.SetBool("IsJumping", false);
        
        }
    }
    private void Run() 
    {

        if (_run && _vertical > 0) 
        {

            _anim.SetBool("IsRunning", true);
            _playerMoveSpeed = 3f;
            transform.Translate(Vector3.forward * _playerMoveSpeed * _vertical * Time.deltaTime);

        }
        else 
        { 
        
            _anim.SetBool("IsRunning", false);
            _playerMoveSpeed = 2f;

        }
    
    }
    private void ImputPlayer() 
    {

        _horizontal = Input.GetAxis("Horizontal");  //Atribuci�n de teclas del eje Horizontal
        _vertical = Input.GetAxis("Vertical");      //Atribuci�n de teclas del eje Vertical
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
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround) 
        {

            //Se le atribuye una fuerza al salto ya definida
            _rb.AddForce(Vector3.up * _playerJumpForce);

        }
    }
    private void GetTurn() 
    {

        //Cantidad de "rotatci�n" a atribuir
        float _rotationAmount = _horizontal * _playerTurnSpeed * Time.deltaTime;

        // Crea la rotaci�n alrededor del eje Y del propio objeto (su pivote)
        Quaternion _deltaRotation = Quaternion.Euler(0, _rotationAmount, 0);

        // Aplica la rotaci�n al Rigidbody, asegurando que gire sobre su propio pivote
        _rb.MoveRotation(_rb.rotation * _deltaRotation);
        
    }
}
