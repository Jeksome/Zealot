using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private Transform playerBody;
    #pragma warning restore 0649

    private Camera playerCamera;
    private float xRotation = 0f; 
    private float mouseSens = 300f;  
    
    private void OnGUI()
    {
        int size = 12;
        float posX = playerCamera.pixelWidth / 2 - size / 4;
        float posY = playerCamera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+");
    }

    private void Start()
    {
        playerCamera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -63f, 63f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
