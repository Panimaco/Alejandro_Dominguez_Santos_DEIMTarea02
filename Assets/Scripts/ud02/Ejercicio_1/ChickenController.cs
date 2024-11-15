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
    private bool _isGrounded,
                 _isMoving,
                 _isJumping;

    private void Awake() 
    {
    
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

        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");
    
    }

    private void Move() 
    {

        if (_vertical > 0) {
        
            transform.Translate(Vector3.forward * _speed * _vertical * Time.deltaTime);
            _anim.SetInteger("Walk", 1);
        
        }
        else
        {

            _anim.SetInteger("Walk", 0);
        
        }
    }
    private void Turn() 
    { 
    
        float chickenRotationAmount = _horizontal * _rotationSpeed * Time.deltaTime;
        
        Quaternion chickenRotation = Quaternion.Euler(0, chickenRotationAmount, 0);

        _chickenRigidbody.MoveRotation(_chickenRigidbody.rotation * chickenRotation);

    }
    private void Jump() 
    {

        if (Input.GetKeyDown(KeyCode.Space))
        { 
        
            _chickenRigidbody.AddForce(Vector3.up * _jumpSpeed);

            _anim.SetTrigger("jump");
        }
    
    }
}
