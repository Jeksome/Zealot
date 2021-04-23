using UnityCore.Menu;
using UnityEngine;

public class MenuButtonActions : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private PageController pageController;
    #pragma warning restore 0649

    public void StartGame() => GameManager.Instance.TryToLoadLevel("level1");
    public void GameOptions() => pageController.TurnPageOff(PageType.MainMenu, PageType.Options);
    public void Continue() => GameManager.Instance.TogglePause();
    public void Restart() => GameManager.Instance.RestartGame();
    public void ExitGame() => Application.Quit();
    public void ReturnFromSoundToOptionsMenu()  => pageController.TurnPageOff(PageType.Sound, PageType.Options);
    public void ReturnFromControlsToOptionsMenu() => pageController.TurnPageOff(PageType.Controls, PageType.Options);
    public void OpenSoundMenu() => pageController.TurnPageOff(PageType.Options, PageType.Sound);
    public void OpenControls() => pageController.TurnPageOff(PageType.Options, PageType.Controls);
    public void ReturnToMainMenu() => pageController.TurnPageOff(PageType.Options, PageType.MainMenu);
}
