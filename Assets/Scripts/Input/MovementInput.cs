using System.Collections;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    public float runningSpeed = 6.0f;
    private CharacterController playerController;
    
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * runningSpeed;
        float deltaZ = Input.GetAxis("Vertical") * runningSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, runningSpeed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        playerController.Move(movement);
    }
}
