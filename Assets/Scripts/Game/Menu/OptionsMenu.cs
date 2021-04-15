using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject controlsMenu;
    #pragma warning restore 0649

    public void OpenSoundMenu()
    {
        soundMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void OpenControls()
    {
        controlsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
