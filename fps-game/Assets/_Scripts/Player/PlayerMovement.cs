using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController _characterController;

    //player basic movement
    [Header("General Locomotion Values")] 
    public bool isMoving;
    public float playerSpeed;
    public Vector3 _move;
    private Vector2 _moveIinput;
    
    //Jump
    [Header("Jump Settings")] 
    public bool isJump;
    public float  jumpHeight;
    
    [Header("Dash Settings")] 
    public bool isDashPressed;
    //public bool isDashable;
    public float  dashSpeed;
    public float dashTime;
    private float _dashCurrentTime;

    //player gravity
    [Header("Gravity Settings")]
    public bool isGrounded;
    private Vector3 _velocity;
    public float gravity;
    public float checkRadius;
    public LayerMask groundMask;
    public Transform groundCheck;

    private void Start()
    {
        _dashCurrentTime = 0;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
        PlayerGravity();
        Jump();
        Dash();
        
    }

    private void PlayerMove()
    {
        _move = transform.forward * _moveIinput.y + transform.right * _moveIinput.x;
        if (_move == Vector3.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        _characterController.Move(_move * playerSpeed * Time.deltaTime);
    }

    void PlayerGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    void Jump()
    {
        if (isJump && isGrounded)
        {
            _velocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravity);
            isJump = false;
        }
    }

    void Dash()
    {
        if (isDashPressed)
        {
            _characterController.Move(_move * dashSpeed * Time.deltaTime);
            _dashCurrentTime += Time.deltaTime;
            if (_dashCurrentTime >= dashTime)
            {
                _dashCurrentTime = 0;
                isDashPressed = false;
            }
        }
    }

    private void OnMove(InputValue moveInput)
    {
        _moveIinput = moveInput.Get<Vector2>();
    }
    
    private void OnJump()
    {
        if (_characterController.velocity.y == 0)
        {
            //can jump
            isJump = true;
        }
        else
        {
            //cant jump
            isJump = false;
        }
    }
    
    private void OnDash()
    {
        if (_dashCurrentTime <= 0)
        {
            isDashPressed = true;
        }
    }
}
