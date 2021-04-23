using UnityEngine;

public class AntDemonAudio : EnemyAudio
{
    private void Awake() => source = GetComponent<AudioSource>();
    public override void PlayAttackSound() => StartJob(attack, false);
}
