using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyCounter;
    private int enemiesLeft;
    
    private void Start()
    {
        enemiesLeft = 3;
        Zombie.enemyKilled += ReduceEnemyCount;
    }

    private void Update()
    {
        enemyCounter.text = $"Enemies left: {enemiesLeft}";
    }

    private void ReduceEnemyCount()
    {
        enemiesLeft -= 1;
    }

    private void OnDisable()
    {
        Zombie.enemyKilled -= ReduceEnemyCount;
    }
}
