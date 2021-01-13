using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable<T>
{
    void PickUp(T item);
}

