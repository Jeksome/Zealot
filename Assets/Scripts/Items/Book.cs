using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Book : MonoBehaviour, IRead
{
    public TMP_Text text;
    public TMP_Text textRus;

    public void ReadBook()
    {
        text.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseBook()
    {
        text.gameObject.SetActive(false);
        textRus.gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TranslateToRus()
    {
        text.gameObject.SetActive(false);
        textRus.gameObject.SetActive(true);
    }

    public void TranslateToEng()
    {
        textRus.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
    }
}
