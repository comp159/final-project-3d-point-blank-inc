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
	private GameObject enemy_spawn;
	private EnemyScript enemy_spawnData;
    private bool inRoom = false;
    private List<GameObject> b;
    private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		enemy_spawn = enemyPrefab;
		enemy_spawnData = enemy_spawn.GetComponent<EnemyScript>();
		init_enemy_data();
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

	public void init_enemy_data(){
		enemy_spawnData.set_health(30);
		enemy_spawnData.set_movement_speed(2f);
		enemy_spawnData.set_damage(1);
		enemy_spawnData.set_attack_speed(5);
		enemy_spawnData.set_money_drop(1);
	}

	public void generate_enemy_data(int floor_num){
		enemy_spawnData.set_health((int) Math.Round(enemy_spawnData.get_health() * 1.05));
		enemy_spawnData.set_movement_speed((float) Math.Round((1.5 * floor_num)));
		enemy_spawnData.set_damage((int) Math.Round(enemy_spawnData.get_damage() * (1.05 * floor_num)));
		enemy_spawnData.set_attack_speed((float) Math.Round(enemy_spawnData.get_attack_speed() / (1.05 * floor_num)));
		enemy_spawnData.set_money_drop((int) Math.Round(enemy_spawnData.get_money_drop() * (1.05 * floor_num)));
	}

    public void SpawnEnemies(Vector3 pos, List<GameObject> barriers)
    {
        inRoom = true;
        int enemies = Random.Range(1, maxEnemies);
        for (int i = 0; i < enemies; i++)
        {
            //var position = pos + new Vector3(Random.Range(0, roomHeight), 0, Random.Range(0, roomWidth));
            //GameObject obj = Instantiate(enemy_spawn, position, Quaternion.identity);
            Vector3 enemySpawnPoint = player.gameObject.transform.position + Random.insideUnitSphere * 40f;
            enemySpawnPoint = new Vector3(enemySpawnPoint.x, 0, enemySpawnPoint.z);
            NavMeshHit hit;
            while (!NavMesh.SamplePosition(enemySpawnPoint, out hit, 1f, NavMesh.AllAreas))
            {
                enemySpawnPoint = player.gameObject.transform.position + Random.insideUnitSphere * 40f;
                enemySpawnPoint = new Vector3(enemySpawnPoint.x, 0, enemySpawnPoint.z);
            }
            GameObject obj = Instantiate(enemy_spawn, hit.position, Quaternion.identity);
        }
        
        for (int i = 0; i < barriers.Count; i++)
        {
            barriers[i].GetComponent<BoxCollider>().isTrigger = false;
        }

        b = barriers;
    }
    
}
