using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _playerMoveSpeed,
                  _playerJumpSpeed;
    [SerializeField]
    private Rigidbody _rb;
    void Update()
    {

        GetMovement();
        GetJump();

    }

    private void GetMovement() 
    {

        

        float speed = _playerMoveSpeed;
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        Vector3 horizontalMove = transform.right * xMovement * _playerMoveSpeed * Time.deltaTime;
        Vector3 verticalMove = transform.forward * yMovement * _playerMoveSpeed * Time.deltaTime;

        transform.position = transform.position + horizontalMove + verticalMove;

    }
    private void GetJump() 
    {
    
        float jumpSpeed = _playerJumpSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) 
        {

            _rb.AddForce(Vector3.up * jumpSpeed);

        }

    }
}
