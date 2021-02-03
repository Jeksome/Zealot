using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool isOff;
    private Light glowingLight;
    private readonly float intensityMultiplier = 0.4f; 
    private readonly float rangeMultiplier = 12f;
    private PlayerCharacter player;

    void Start()
    {
        isOff = true;
        glowingLight = GetComponent<Light>();
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
    }

    public void TryToTurnOn()
    {
        if (isOff && player.currentHealth > 1)
        {
            StartCoroutine(FlashLight());
            player.Hurt();
        }
    }

    private IEnumerator FlashLight()
    {
        isOff = false;
        glowingLight.range *= rangeMultiplier;
        glowingLight.intensity += intensityMultiplier;
        yield return new WaitForSeconds(2f);
        glowingLight.range /= rangeMultiplier;
        glowingLight.intensity -= intensityMultiplier;
        isOff = true;
    }
}
