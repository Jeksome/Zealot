using UnityEngine;
using TMPro;

public class ArmorBar : StatusBar
{
   private void Start()
    {
        bar = GetComponent<TMP_Text>();
        bar.color = Color.yellow;
        emptyBarText = "";
    }
}
