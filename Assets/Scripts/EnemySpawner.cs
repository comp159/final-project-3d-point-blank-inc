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

	/* Initial Enemy Stats */
	[SerializeField] private int health = 30;
    [SerializeField] private float movement_speed = 3.5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attack_speed = 4f;
    [SerializeField] private int money_drop = 1;

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

	/* Reset the information found within the prefab */
	public void init_enemy_data(){
		enemy_spawnData.set_health(health);
		enemy_spawnData.set_movement_speed(movement_speed);
		enemy_spawnData.set_damage(damage);
		enemy_spawnData.set_attack_speed(attack_speed);
		enemy_spawnData.set_money_drop(money_drop);
	}

	/* Increase enemy difficulty */
	public void generate_enemy_data(int floor_num){

		/* Health */
		enemy_spawnData.set_health((int) Math.Ceiling(enemy_spawnData.get_health() + (.1 * floor_num)));
		if (floor_num % 2 == 0)
		{
			enemy_spawnData.set_health((int) Math.Ceiling((double)enemy_spawnData.get_health() + 1));
		}

		/* Movement Speed */
		if (floor_num % 2 == 0)
		{
			enemy_spawnData.set_movement_speed((float) Math.Ceiling((double)enemy_spawnData.get_movement_speed() + (0.03 * floor_num)));
		}
		

		/* Damage */
		if (floor_num % 2 == 0)
		{
			enemy_spawnData.set_damage((int) Math.Ceiling((double)enemy_spawnData.get_damage() + 1));
		}
		
		/* Attack Speed */
		if (floor_num % 2 == 1)
		{
			enemy_spawnData.set_attack_speed((float) enemy_spawnData.get_attack_speed() - (enemy_spawnData.get_attack_speed() / 50));
		}
		
		/* Money Drop */
		if (floor_num % 2 == 1)
		{
			enemy_spawnData.set_money_drop((int) Math.Ceiling((double)enemy_spawnData.get_money_drop() + 1));
		}
		
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
