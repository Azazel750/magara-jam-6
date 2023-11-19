using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwan : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float initialSpawnInterval = 2f; // Initial time interval between spawns
    public int[] enemiesPerStage; // Number of enemies per stage
    public float timeBetweenStages = 10f; // Time between stages

    private int currentStage = 0;

    void Start()
    {
        StartCoroutine(StageBasedEnemySpawn());
    }

    IEnumerator StageBasedEnemySpawn()
    {
        while (currentStage < enemiesPerStage.Length)
        {
            int enemiesToSpawn = enemiesPerStage[currentStage];

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomSpawnPointIndex];
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(initialSpawnInterval);
            }

            currentStage++;

            if (currentStage < enemiesPerStage.Length)
            {
                yield return new WaitForSeconds(timeBetweenStages);
            }
        }
    }
}
