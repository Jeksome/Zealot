using UnityEngine;

public class Level1End : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) => GameManager.Instance.GameOver();
}
