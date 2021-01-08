using System.Collections;
using UnityEngine;

[RequireComponent (typeof (CharacterController))]
[AddComponentMenu("Control Script/Movement Input")]

public class MovementInput : MonoBehaviour
{
    public float runningSpeed = 6.0f;
    private CharacterController _playerController;
    
    void Start()
    {
        _playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * runningSpeed;
        float deltaZ = Input.GetAxis("Vertical") * runningSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, runningSpeed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _playerController.Move(movement);
    }
}
