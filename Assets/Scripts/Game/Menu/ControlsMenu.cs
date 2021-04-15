using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private GameObject optionsMenu;
    #pragma warning restore 0649

    public void ReturnToOptionsMenu()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
