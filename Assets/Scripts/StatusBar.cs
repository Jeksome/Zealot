using UnityEngine;
using TMPro;

public abstract class StatusBar : MonoBehaviour
{
    protected TMP_Text bar;
    protected string emptyBarText;

    public void Display(int displayStat)
    {
        if (displayStat > 0)
        {
            bar.text = displayStat.ToString();
        }
        else
        {
            bar.text = emptyBarText;
        }
    }
}
