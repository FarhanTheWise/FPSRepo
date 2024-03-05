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
    public float playerSpeed;
    public float dashSpeed;
    public Vector3 _move;
    private Vector2 _moveIinput;

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
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
        PlayerGravity();
    }

    private void PlayerMove()
    {

        //Dash


        _move = transform.forward * _moveIinput.y + transform.right * _moveIinput.x;
        _characterController.Move(_move * playerSpeed * Time.deltaTime);
    }

    void PlayerGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnMove(InputValue moveInput)
    {
        _moveIinput = moveInput.Get<Vector2>();
    }
}
