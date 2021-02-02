using System.Collections;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private Vector3 velocity;
    private CharacterController playerController;
    private Transform groundCheck;
    private const float jumpHeight = 1f;
    private const float groundDistance = 0.2f;
    private const float gravity = -9.8f;
    private const float movementSpeedModifier = 3.0f;
    private const float crouchCameraHightModifier = 2.0f;
    private float movingSpeed = 5.0f;
    private float horizontalInput, verticalInput;
    private bool isGrounded, isSprinting, isCrouching;
    
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        groundCheck = GameObject.Find("GroundCheck").transform;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * movingSpeed;
        verticalInput = Input.GetAxis("Vertical") * movingSpeed;

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement = Vector3.ClampMagnitude(movement, movingSpeed) * Time.deltaTime;
        movement = transform.TransformDirection(movement);
        playerController.Move(movement);

        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();

        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isCrouching && !isSprinting)
            StartCoroutine(Sprint());

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
    }

    private void ChangeMovementSpeed(float modifier)
    {
        movingSpeed += modifier;
    }
    private void StartCrouch()
    {
        isCrouching = true;
        ChangeMovementSpeed(-movementSpeedModifier);
        playerController.height /= crouchCameraHightModifier;       
    }

    private void StopCrouch()
    {
        isCrouching = false;
        ChangeMovementSpeed(movementSpeedModifier);
        playerController.height *= crouchCameraHightModifier;
    }

    private IEnumerator Sprint()
    {
        isSprinting = true;
        ChangeMovementSpeed(movementSpeedModifier);
        yield return new WaitForSeconds(1.5f);
        isSprinting = false;
        ChangeMovementSpeed(-movementSpeedModifier);
    }
}
