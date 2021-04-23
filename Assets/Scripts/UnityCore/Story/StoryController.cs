using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryController : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private Text book;
    [SerializeField] private MouseLook crosshair;
    [SerializeField] private GameObject bookCover;
    #pragma warning restore 0649

    private List<string> stories = new List<string>();

    private void Start()
    {
        stories.Add("Book 1: \n \n Thank you for participating in alpha-test!");
        stories.Add("Placeholder text #2");
        stories.Add("Placeholder text #3");
        stories.Add("Placeholder text #4");
        stories.Add("Placeholder text #5");
    }

    public void ShowText(int storyID)
    {
        bookCover.SetActive(true);
        book.text = stories[storyID];
        Time.timeScale = 0;
        crosshair.IsOn = false;
        CursorLocker.UnlockCursor();
    }

    public void HideText()
    {
        bookCover.SetActive(false);
        Time.timeScale = 1;
        crosshair.IsOn = true;
        CursorLocker.LockCursor();
    }
}
