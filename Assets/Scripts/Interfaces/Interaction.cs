public interface IInteractable
{
    void Interact();
}

public interface IOpenable : IInteractable
{
    void Open();
    void Close();
}

public interface IReadable : IInteractable
{
    void ReadBook();
    void CloseBook();
    void TranslateToEng();
    void TranslateToRus();
}