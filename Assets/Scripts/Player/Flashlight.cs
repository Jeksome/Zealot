using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private PlayerCharacter player;   
    private Light glowingLight;
    private bool isOn;
    private readonly float intensityMultiplier = 0.8f; 
    private readonly float rangeMultiplier = 4f; 
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
        if (!isOn && player.IsAlive)
            StartCoroutine(TurnedOn());
    }

    public void TryToTurnOff()
    {
        if (isOn && player.IsAlive)
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
            player.Hurt();
            yield return new WaitForSeconds(2f);           
        }     
    }
}
