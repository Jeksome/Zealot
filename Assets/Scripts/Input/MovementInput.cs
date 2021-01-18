using System.Collections;
using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    private CharacterController playerController;
    private Transform groundCheck;
    private const float jumpHeight = 1f;
    private const float groundDistance = 0.2f;
    private const float gravity = -9.8f;
    private float movingSpeed = 5.0f;
    private float horizontalInput, verticalInput;
    private bool isGrounded, isSprinting;

    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 normalScale;
    private Vector3 velocity;

    void Start()
    {
        normalScale = transform.localScale;
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprinting)
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

    private void StartCrouch()
    {
        movingSpeed /= 2;
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
    }

    private void StopCrouch()
    {
        movingSpeed *= 2;
        transform.localScale = normalScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private IEnumerator Sprint()
    {
        isSprinting = true;
        movingSpeed *= 2;
        yield return new WaitForSeconds(1.5f);
        isSprinting = false;
        movingSpeed /= 2;
    }

}
