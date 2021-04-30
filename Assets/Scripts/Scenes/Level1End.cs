using UnityEngine;

public class Level1End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject == FindObjectOfType<PlayerCharacter>())
        {
            GameManager.Instance.GameOver();
        }        
    } 
}
