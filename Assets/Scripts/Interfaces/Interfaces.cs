public interface IInteractable 
{
    void Interact();
}

public interface IOpenable : IInteractable
{
    void OpenDoor();
    void CloseDoor();
}

public interface IReadable : IInteractable
{
    void ReadBook();
    void CloseBook();
    void TranslateToEng();
    void TranslateToRus();
}