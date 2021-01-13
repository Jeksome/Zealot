using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Book : MonoBehaviour
{
    public TMP_Text text;
    public void ReadBook()
    {
        Debug.Log("Reading...");
        text.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            text.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void StopReading()
    {
        //TODO
    }
}
