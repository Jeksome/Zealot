using UnityEngine;
using TMPro;

public class Book : MonoBehaviour, IReadable
{
    #pragma warning disable 0649
    [Range(1, 10)] [SerializeField] private int storyID;
    [SerializeField] private TMP_Text book;    
    [SerializeField] private Story storyLibrary;
    #pragma warning restore 0649

    public void Interact() => ReadBook();

    public void ReadBook()
    {
        book.gameObject.SetActive(true);
        book.text = storyLibrary.Stories[storyID];
        Time.timeScale = 0;
        CursorLocker.UnlockCursor();   
    }

    public void CloseBook()
    {
        book.gameObject.SetActive(false);
        Time.timeScale = 1;
        CursorLocker.LockCursor();       
    }
}
