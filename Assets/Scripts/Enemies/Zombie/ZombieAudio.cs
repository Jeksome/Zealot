using UnityEngine;

public class ZombieAudio : EnemyAudio
{
    private void Awake() => source = GetComponent<AudioSource>();
}
