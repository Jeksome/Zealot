﻿using UnityEngine;

public class MouseInteraction : MonoBehaviour
{
    [SerializeField] private GameObject staff;
    private Camera playerCamera;
    private PlayerCharacter player;
    private float rayLenght = 3.5f;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
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
                TryPickUp(hit.collider.gameObject, hit.collider.tag);               
        }
    }

    private void TryPickUp(GameObject item, string itemName)
    {
        switch (itemName)
        {
            case "Health":
                item.SetActive(false);
                player.Heal();
                break;
            case "Door":
                WoodenDoor woodenDoor = item.GetComponent<WoodenDoor>();
                woodenDoor.OpenDoor();
                break;
            case "LockedDoor":
                LockedDoor lockedDoor = item.GetComponent<LockedDoor>();
                lockedDoor.OpenDoor();
                break;
            case "Key":
                Key key = item.GetComponent<Key>();
                key.PickUp(item);
                break;
            case "Book":
                Book book = item.GetComponent<Book>();
                book.ReadBook();
                break;
            case "Staff":
                staff.SetActive(true);
                item.SetActive(false);
                break;
        }
    }

}
