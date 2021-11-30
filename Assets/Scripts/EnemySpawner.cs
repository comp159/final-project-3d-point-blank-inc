using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private int maxEnemies = 5;
    private float roomWidth = 20f;
    private float roomHeight = 20f;

    [SerializeField] private GameObject enemyPrefab;
    private bool inRoom = false;
    private List<GameObject> b;
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (inRoom && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            for (int i = 0; i < b.Count; i++)
            {
               Debug.Log("Destroying");
                Destroy(b[i]);
            }

            inRoom = false;
            player.barriers.Clear();
        }
    }

    public void SpawnEnemies(Vector3 pos, List<GameObject> barriers)
    {
        inRoom = true;
        int enemies = Random.Range(1, maxEnemies);
        for (int i = 0; i < enemies; i++)
        {
            //var position = pos + new Vector3(Random.Range(0, roomHeight), 0, Random.Range(0, roomWidth));
            //GameObject obj = Instantiate(enemyPrefab, position, Quaternion.identity);
            Vector3 enemySpawnPoint = player.gameObject.transform.position + Random.insideUnitSphere * 40f;
            enemySpawnPoint = new Vector3(enemySpawnPoint.x, 0, enemySpawnPoint.z);
            NavMeshHit hit;
            while (!NavMesh.SamplePosition(enemySpawnPoint, out hit, 1f, NavMesh.AllAreas))
            {
                enemySpawnPoint = player.gameObject.transform.position + Random.insideUnitSphere * 40f;
                enemySpawnPoint = new Vector3(enemySpawnPoint.x, 0, enemySpawnPoint.z);
            }
            GameObject obj = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
        }
        
        for (int i = 0; i < barriers.Count; i++)
        {
            barriers[i].GetComponent<BoxCollider>().isTrigger = false;
        }

        b = barriers;
    }
    
}
