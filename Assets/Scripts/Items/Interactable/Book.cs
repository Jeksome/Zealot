using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour, IReadable
{
    #pragma warning disable 0649
    [Range(0, 10)] [SerializeField] private int storyID;
    [SerializeField] private StoryController storyLibrary;
    #pragma warning restore 0649

    public void Interact() => ReadBook();

    public void ReadBook() => storyLibrary.ShowText(storyID);

    public void CloseBook() => storyLibrary.HideText();  
}
