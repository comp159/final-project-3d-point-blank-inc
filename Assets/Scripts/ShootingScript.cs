using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    /* As this is a generalized shooting script, this designates specific qualities for player/enemy shooting */
    [SerializeField] private bool player_shooting;
    
    /* Player-specific shooting characteristics */
	[SerializeField] private PlayerController player;
    [SerializeField] private int clip_size;
    private bool cooldown = false;

	/* Enemy variable, assuming player isn't the parent */
	[SerializeField] private EnemyScript enemy;
    
    /* Generalized shooting characteristics */
    private float firing_speed;
    private int damage;

	/* Debug and testing */
	private int player_counter = 0;
	private int enemy_counter = 0;

    void Start()
    {
        if (player_shooting)
        {
            firing_speed = player.get_attack_speed();
			damage = player.get_damage();
        } 
		else 
		{
			firing_speed = enemy.get_attack_speed();
			damage = enemy.get_damage();
			StartCoroutine("enemy_firing");
		}
    }
    
    void FixedUpdate()
    {
        /* If the player presses the button and $firing_speed has elapsed, initiate a player fire */
        if (Input.GetMouseButtonDown(0) && !cooldown)
        {
            StartCoroutine("player_firing");
        }
    }

    IEnumerator enemy_firing()
    {
        /* While enemy is alive, fire periodically every $firing_speed */
        while (true)
        {
            /* Testing */
            enemy_counter++;
            Debug.Log("Enemy shot: " + enemy_counter);
            
            raycast("Player");
            yield return new WaitForSeconds(firing_speed);
        }
    }
    
    IEnumerator player_firing()
    {
        /* Testing */
        player_counter++;
        Debug.Log("Player shot: " + player_counter);
        
        /* Search for enemy within raycast, and set cooldown for $firing_speed */
        raycast("Enemy");
        cooldown = true;
        yield return new WaitForSeconds(firing_speed);
        cooldown = false;
    }

    private void raycast(string tag)
    {
        /* Set raycast layers (as to avoid hitting colliders within environment) */
        RaycastHit hit;
        int layerMask = 1 << 3;
        layerMask = ~layerMask;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == tag)
            {
                /* The raycast has hit it's intended target, and we will deal damage appropriately */
                if (tag == "Player")
                {
                    hit.collider.GetComponent<PlayerController>().deal_damage(damage);
                    Debug.Log("Detected Player: Health now " + hit.collider.GetComponent<PlayerController>().get_health());
                }
                else if (tag == "Enemy")
                {
                    hit.collider.GetComponent<EnemyScript>().deal_damage(damage);
                    Debug.Log("Detected Enemy: Health now " + hit.collider.GetComponent<EnemyScript>().get_health());
					if (hit.collider.GetComponent<EnemyScript>().get_health() <= 0)
					{
						player.add_money(hit.collider.GetComponent<EnemyScript>().get_money_drop());
						Debug.Log("New Schmoney Balance: " + player.get_money());
						Destroy(hit.collider.gameObject);
					}
                }
            }
            else
            {
                /* Raycast has missed it's intended target, and will not do anything */
				Debug.Log("Hit " + hit.collider.tag);
            }
        }
        else
        {
            /* Raycast has missed it's intended target (completely), and will not do anything */
			Debug.Log("Missed");
        }
        
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.forward) * 1000, Color.white);
    }

}
