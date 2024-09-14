using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject enemyPrefab;
    public int numberOfEnemies = 10;
    public float minDistanceFromPlayer = 20f;
    public float terrainSize = 256f;
    public float maxSpawnHeight = 100f;
    public LayerMask terrainLayer;


    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 spawnPos = GetValidSpawnPosition();
            if (spawnPos != Vector3.zero)
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        for (int attempts = 0; attempts < 50; attempts++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(-terrainSize / 2, terrainSize / 2),
                maxSpawnHeight,
                Random.Range(-terrainSize / 2, terrainSize / 2)
            );

            if (Vector3.Distance(new Vector3(randomPos.x, 0, randomPos.z),
                                 new Vector3(playerTransform.position.x, 0, playerTransform.position.z))
                >= minDistanceFromPlayer)
            {
                if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, maxSpawnHeight, terrainLayer))
                {
                    return hit.point;
                }
            }
        }

        Debug.LogWarning("Could not find a valid spawn position after 50 attempts.");
        return Vector3.zero;
    }
}