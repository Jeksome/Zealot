using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IPickable<GameObject>
{
    public void PickUp(GameObject item)
    {
        item.SetActive(false);

        LockedDoor lockedDoor = FindObjectOfType<LockedDoor>();
        lockedDoor.InsertKey();
    }
}
