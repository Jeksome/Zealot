using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/Jump")]

public class Jump : MonoBehaviour
{
    public float gravity = -9.8f;
    public float jumpHeight = 1f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private bool _isGrounded;
    private Vector3 _velocity;
    private CharacterController _playerController;

    void Start()
    {
        _playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        _velocity.y += gravity * Time.deltaTime;
        _playerController.Move(_velocity * Time.deltaTime);
    }
}