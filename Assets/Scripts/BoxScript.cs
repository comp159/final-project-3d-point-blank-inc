using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

	/* Different powerup types */
	[SerializeField] private GameObject powerup1;
	[SerializeField] private GameObject powerup2;
	[SerializeField] private GameObject powerup3;
	[SerializeField] private GameObject powerup4;
	[SerializeField] private GameObject powerup5;
	[SerializeField] private GameObject powerup6;
	List<GameObject> stat_powerups = new List<GameObject>();
	List<GameObject> gun_powerups = new List<GameObject>();

	/* Setting probabilities */
	[SerializeField] private int stat_drop;
	[SerializeField] private int gun_drop;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	/* Called upon destruction of box, randomly decides if a powerup is dropped */
	public void spawn_powerup()
	{
		
		stat_powerups.Add(powerup1);
		stat_powerups.Add(powerup2);
		stat_powerups.Add(powerup3);
		stat_powerups.Add(powerup4);
		//gun_powerups.Add(powerup5);
		//gun_powerups.Add(powerup6);

		bool spawned = false;

		/* First, try to drop a stat_powerup */
		if(Random.Range(0, stat_drop) == 0)
		{
			Debug.Log("Spawned Stat Powerup!");
			Instantiate(stat_powerups[Random.Range(0, 4)], this.transform.position, Quaternion.identity);
			spawned = true;
		}

		/* If a stat_powerup isn't drop, try to drop a gun_powerup */
		if(Random.Range(0, stat_drop) == 0 && !spawned)
		{
			Debug.Log("Spawned Gun Powerup!");
			Instantiate(gun_powerups[Random.Range(0, 2)], this.transform.position, Quaternion.identity);
			spawned = true;
		}

	}

}
