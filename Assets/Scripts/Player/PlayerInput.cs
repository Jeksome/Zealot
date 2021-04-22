using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;
    [SerializeField] private GroundCheck groundCheckDisplay;
    #pragma warning restore 0649

    private Vector3 velocity;
    private CharacterController playerController;
    private PlayerCharacter playerCharacter;
    private PlayerAudio playerAudio;
    private AudioSource playerAudioSource;

    private const float jumpHeight = 1.2f;
    private const float groundDistance = 0.2f;
    private const float gravity = -9.8f;
    private const float movementSpeedModifier = 3.0f;
    private const float crouchCameraHightModifier = 2.0f;
    private float playerHight;
    private float movingSpeed;
    private float defaultSpeed;
    private float horizontalInput, verticalInput;
    private bool isGrounded, isSprinting, isCrouching, isTouchingCeiling, hasStoppedCrouching, isWalkingBackwards;
    
    private void Start()
    {       
        playerController = GetComponent<CharacterController>();
        playerCharacter = GetComponent<PlayerCharacter>();
        playerAudio = GetComponent<PlayerAudio>();
        playerAudioSource = playerAudio.GetComponent<AudioSource>();
        playerHight = playerController.height;
        defaultSpeed = 5.0f;
        movingSpeed = defaultSpeed;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") * movingSpeed;
        verticalInput = Input.GetAxis("Vertical") * movingSpeed;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isTouchingCeiling = Physics.CheckSphere(ceilingCheck.position, groundDistance, groundMask);       

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement = Vector3.ClampMagnitude(movement, movingSpeed) * Time.deltaTime;
        movement = transform.TransformDirection(movement);
        playerController.Move(movement);

        if (isGrounded && playerController.velocity.magnitude > 2f && playerAudioSource.isPlaying == false)
        {
            playerAudio.ToggleFootstep(isSprinting, isWalkingBackwards);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentGameState  != GameManager.GameState.PREGAME)
        {
            GameManager.Instance.TogglePause();
        }

        if (flashlight.IsFound)
        {
            if (Input.GetKeyDown(KeyCode.F))
                flashlight.TryToTurnOn();
            else if (Input.GetKeyUp(KeyCode.F))
                flashlight.TryToTurnOff();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
            StartCrouch();

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (!isTouchingCeiling)
                StopCrouch();
            else
                hasStoppedCrouching = false;
        }

        if (!isTouchingCeiling && Input.GetKey(KeyCode.LeftControl) == false && !hasStoppedCrouching)
            NormalizePlayer();

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && Input.GetKey(KeyCode.W) && !isCrouching && !isSprinting && playerCharacter.CanCast && playerCharacter.IsBurdened == false)
            StartSprint();
        
        if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.W)) && isSprinting)
            StopSprint();

        if (Input.GetKeyDown(KeyCode.S))
        {
            movingSpeed = movementSpeedModifier;
            isWalkingBackwards = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            movingSpeed = defaultSpeed;
            isWalkingBackwards = false;
        }
                 
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);

        groundCheckDisplay.GetStatus(isGrounded);
    }   

    private void NormalizePlayer()
    {
        playerController.height = playerHight;
        movingSpeed = defaultSpeed;
        hasStoppedCrouching = true;
        isCrouching = false;
    }

    private void ChangeMovementSpeed(float modifier) => movingSpeed += modifier;

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

    private void StartSprint()
    {
        isSprinting = true;
        ChangeMovementSpeed(movementSpeedModifier);
        StartCoroutine(DamageHealthWhileSprinting());
    }

    private void StopSprint()
    {
        isSprinting = false;
        ChangeMovementSpeed(-movementSpeedModifier);
    }

    private IEnumerator DamageHealthWhileSprinting()
    {                
        while (isSprinting)
        {
            playerCharacter.GetHurt();
            yield return new WaitForSeconds(2f);            
        }
    }
}
