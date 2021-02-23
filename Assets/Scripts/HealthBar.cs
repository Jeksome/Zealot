using TMPro;

public class HealthBar : StatusBar
{
    void Start()
    {
        bar = GetComponent<TMP_Text>();
        emptyBarText = "Dead";
    }
}
