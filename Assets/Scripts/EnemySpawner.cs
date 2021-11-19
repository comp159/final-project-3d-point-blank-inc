using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private int maxEnemies = 5;
    private float roomWidth = 20f;
    private float roomHeight = 20f;

    [SerializeField] private GameObject enemyPrefab;
    
    public void SpawnEnemies(Vector3 pos)
    {
        int enemies = Random.Range(1, maxEnemies);
        for (int i = 0; i < enemies; i++)
        {
            var position = pos + new Vector3(Random.Range(0, roomHeight), 0, Random.Range(0, roomWidth));
            GameObject obj = Instantiate(enemyPrefab, position, Quaternion.identity);
        }
    }
    
}
