using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    
    private Camera playerCamera;
    private const float rayLenght = 3.5f;

    void Start()
    {
        playerCamera = GetComponent<Camera>();
    }
    void Update()
    {
        SearchForPickUp();
    }

    private void SearchForPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, rayLenght))
            {
                IInteractable hitObject = hit.collider.gameObject.GetComponent<IInteractable>();
                if (hitObject != null)
                {
                    hitObject.Interact();
                }
            }
        }
    }
}
