using UnityEngine.UI;

public class ArmorBar : StatusBar
{
   private void Start()
    {
        bar = GetComponent<Text>();
        emptyBarText = "";
    }
}
