using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] private PlayerCharacter player;
    #pragma warning restore 0649

    private Light glowingLight;
    private bool isOn;
    private readonly float intensityMultiplier = 1f; 
    private readonly float rangeMultiplier = 5f; 
    public bool IsFound { get { return isFound; } }
    private bool isFound;

    void Start()
    {
        glowingLight = GetComponent<Light>();
        isOn = false;       
    }

    private void OnEnable() => isFound = true;

    public void TryToTurnOn()
    {
        if (!isOn && player.CanCast)
            StartCoroutine(TurnedOn());
    }

    public void TryToTurnOff()
    {
        if (isOn)
        {
            isOn = false;
            glowingLight.range /= rangeMultiplier;
            glowingLight.intensity -= intensityMultiplier;
        }
    }

    private IEnumerator TurnedOn()
    {
        glowingLight.range *= rangeMultiplier;
        glowingLight.intensity += intensityMultiplier;
        isOn = true;

        while (isOn)
        {
            player.GetHurt();
            yield return new WaitForSeconds(2f);           
        }     
    }
}
