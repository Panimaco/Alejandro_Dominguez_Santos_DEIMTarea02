using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    //Zona de Variables Globales
    [SerializeField]
    private float _playerMoveSpeed,     //Velocidad de Movimiento del Muñeco
                  _playerJumpForce,     //Fuerza de Salto del Muñeco
                  _playerTurnSpeed;     //Velocidad de Rotación del Muñeco
    private float _horizontal,          //Imput de eje Horizontal
                  _vertical;            //Imput de eje Vertical

    [SerializeField]
    private Rigidbody _rb;              //Rigidbody del Muñeco para el salto

    [Header("Animation")]
    [SerializeField]
    private Animator _anim;
    private bool _isPlayerJumping,
                 _isOnGround,
                 _isCapableOfMove;
    private Ray _ray;

    void Update()
    {

        ImputPlayer();                              //Imputs del jugador
        GetMovement();                              //Movimiento del jugador
        GetJump();                                  //Salto del jugador
        GetTurn();                                  //Rotación del jugador

        if (_isCapableOfMove) 
        {
            Move();                                 //Animación de movimiento
        }

        Jump();                                     //Animación de saltar

    }


    private void Move() 
    {

        if (_horizontal != 0 || _vertical != 0) {

            _anim.SetTrigger("IsMoving");

        }

    }
    private void Jump() {

        _ray.origin = transform.position;
        _ray.direction = -transform.up;

        Debug.DrawRay(_ray.origin, _ray.direction * 0.01f, Color.red);

        if (Physics.Raycast(_ray)) {

            _isOnGround = true;

        }
        else {

            _isOnGround = false;

        }
        if (_isPlayerJumping && _isOnGround) {

            _anim.SetTrigger("IsJumping");
            _isCapableOfMove = false;

        }

        else 
        {

            _isCapableOfMove = true;

        }

    }
    private void ImputPlayer() 
    {

        _horizontal = Input.GetAxis("Horizontal");  //Atribución de teclas del eje Horizontal
        _vertical = Input.GetAxis("Vertical");      //Atribución de teclas del eje Vertical

    }
    private void GetMovement() 
    {

        //Atribuye al "transform" del controlador del Muñeco el movimiento en el eje "foward"
        //multiplicado por la velocidad puesta del jugador por el eje por el tiempo
        //haciendo así que si no has pulsado los "axis" correctos no se mueva.

        transform.Translate(Vector3.forward * _playerMoveSpeed * _vertical * Time.deltaTime);
   
    }
    private void GetJump() 
    {

        //Si pulsas la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Se le atribuye una fuerza al salto ya definida
            _rb.AddForce(Vector3.up * _playerJumpForce);

            _isPlayerJumping = true;
        }

        else 
        {
            _isPlayerJumping = false;
        }

    }
    private void GetTurn() 
    {

        //Cantidad de "rotatción" a atribuir
        float _rotationAmount = _horizontal * _playerTurnSpeed * Time.deltaTime;

        // Crea la rotación alrededor del eje Y del propio objeto (su pivote)
        Quaternion _deltaRotation = Quaternion.Euler(0, _rotationAmount, 0);

        // Aplica la rotación al Rigidbody, asegurando que gire sobre su propio pivote
        _rb.MoveRotation(_rb.rotation * _deltaRotation);
        
    }
}
