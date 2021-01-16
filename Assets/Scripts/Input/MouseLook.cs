using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;

    private float xRotation = 0f; 
    private float mouseSens = 300f;
    private Camera playerCamera;
    public GameObject elvenstaff;
    private int rayLenght = 3;

    void Start()
    {
        playerCamera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
}

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -75f, 75f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        AttemptPickUp();
    }

    private void AttemptPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLenght))
            {
                if (hit.collider.tag == "Staff")
                {
                    GameObject itemGrabbed = hit.collider.gameObject;
                    Debug.Log($"You picked up {itemGrabbed}");
                    elvenstaff.SetActive(true);
                    itemGrabbed.SetActive(false);
                }
                else if (hit.collider.tag == "Ammo")
                {
                    GameObject itemGrabbed = hit.collider.gameObject;
                    Debug.Log($"You picked up {itemGrabbed}");
                    itemGrabbed.SetActive(false);
                    ElvenStaff elvenStaff = FindObjectOfType<ElvenStaff>();
                    elvenStaff.IncreasePlayerHealth(Random.Range(10, 15));

                    //FIX THIS
                    
                    //MAKE ELVENSTAFF SINGLETON OR MAKE ANOTHER CLASS FOR AMMO
                }
                else if (hit.collider.tag == "Door")
                {
                    Debug.Log($"You staring at {hit.collider.gameObject}");
                    WoodenDoor woodenDoor = hit.collider.gameObject.GetComponent<WoodenDoor>();
                    woodenDoor.OpenDoor();
                }
                else if (hit.collider.tag == "LockedDoor")
                {
                    Debug.Log($"You staring at {hit.collider.gameObject.name}");
                    LockedDoor lockedDoor = hit.collider.gameObject.GetComponent<LockedDoor>();
                    lockedDoor.OpenDoor();
                }
                else if (hit.collider.tag == "Key")
                {
                    Key key = hit.collider.gameObject.GetComponent<Key>();
                    key.PickUp(hit.collider.gameObject);
                }
                else if (hit.collider.tag == "Book")
                {
                    Debug.Log($"You are staring at {hit.collider.gameObject.name}");
                    Book book = hit.collider.gameObject.GetComponent<Book>();
                    book.ReadBook();
                }
            }
        }
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = playerCamera.pixelWidth / 2 - size / 4;
        float posY = playerCamera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
