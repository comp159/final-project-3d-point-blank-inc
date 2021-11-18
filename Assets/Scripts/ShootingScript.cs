using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    /* As this is a generalized shooting script, this designates specific qualities for player/enemy shooting */
    [SerializeField] private bool player_shooting;
    
    /* Player-specific shooting characteristics */
    [SerializeField] private int clip_size;
    private bool cooldown = false;
    
    /* Generalized shooting characteristics */
    [SerializeField] private float firing_speed;
    [SerializeField] private float damage;
    
    /* Debug and testing variables, to be deleted */
    private int enemy_counter = 0;  
    private int player_counter = 0;
    private LineRenderer laser;
    
    void Start()
    {
        if (!player_shooting)
        {
            StartCoroutine("enemy_firing");
        }
        laser = GetComponent<LineRenderer>();
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
        laser.enabled = true;
        yield return new WaitForSeconds(firing_speed);
        laser.enabled = false;
        cooldown = false;
        
    }

    private void raycast(string tag)
    {
        /* Set raycast layers (as to avoid hitting colliders within environment) */
        RaycastHit hit;
        int layerMask = 1 << 3;
        layerMask = ~layerMask;
        laser.SetPosition(0, transform.position);
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == tag)
            {
                /* The raycast has hit it's intended target, and we will deal damage appropriately */
                if (tag == "Player")
                {
                    //hit.GetComponent<PlayerScript>().deal_damage(damage)
                    Debug.Log("Detected Player");
                }
                else if (tag == "Enemy")
                {
                    //hit.GetComponent<EnemyScript>().deal_damage(damage)
                    Debug.Log("Detected Enemy");
                }
            }
            else
            {
                /* Raycast has missed it's intended target, and will not do anything */
            }
        }
        else
        {
            /* Raycast has missed it's intended target (completely), and will not do anything */
        }
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        laser.SetPosition (1, hit.point+transform.TransformDirection(Vector3.forward)*50);
        
    }

}
