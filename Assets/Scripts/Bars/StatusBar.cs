using UnityEngine;
using UnityEngine.UI;

public abstract class StatusBar : MonoBehaviour
{
    protected Text bar;
    protected string emptyBarText;

    public void Display(int stat)
    {
        if (stat > 0)
        {
            bar.text = stat.ToString();
        }
        else
        {
            bar.text = emptyBarText;
        }
    }
}
