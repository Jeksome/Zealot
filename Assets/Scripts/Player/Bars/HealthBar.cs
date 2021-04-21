using UnityEngine.UI;

public class HealthBar : StatusBar
{
    void Start()
    {
        bar = GetComponent<Text>();
        emptyBarText = "Dead";
    }
}
