using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingScript : MonoBehaviour
{
    /* As this is a generalized shooting script, this designates specific qualities for player/enemy shooting */
    [SerializeField] private bool player_shooting;
    
    /* Player-specific shooting characteristics */
	[SerializeField] private PlayerController player;
    private int clip_size;
	private int clip_remaining;
    private bool cooldown = false;

    private AudioSource audio;
    private AudioSource reloadSource;

    private AudioClip shootSound;
    private AudioClip deathSound;
	/* Enemy variable, assuming player isn't the parent */
	[SerializeField] private EnemyScript enemy;
    
    /* Generalized shooting characteristics */
    private float firing_speed;
    private int damage;
	private LineRenderer laser;
    
    /* Debug and testing variables, to be deleted */
    private int enemy_counter = 0;  
    private int player_counter = 0;

	/* Display UI */
	[SerializeField] private TextMeshProUGUI ammo_text;

    void Start()
    {
	    audio = GetComponent<AudioSource>();
	    audio.playOnAwake = false;

	    reloadSource = GameObject.FindGameObjectWithTag("Spotlight").GetComponent<AudioSource>();
	    shootSound = Resources.Load("Shooting Sound") as AudioClip;
	    deathSound = Resources.Load("Enemy Death Sound") as AudioClip;

	    laser = GetComponent<LineRenderer>();
		if (player_shooting)
        {
            firing_speed = player.get_reload_speed();
			damage = player.get_damage();
			clip_size = player.get_clip_size();
			clip_remaining = clip_size;
			ammo_text.text = "Current Ammo: " + clip_remaining + "/" + clip_size;
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
	    /* Update player attributes */
	    if (player_shooting)
	    {
		    firing_speed = player.get_reload_speed();
		    damage = player.get_damage();
		    clip_size = player.get_clip_size();
			if(clip_remaining <= 0)
			{
				ammo_text.text = "Reloading...";
			}
	    } 
	    
        /* Player fires with a left click, and automatically reload upon emptying clip */
        if (Input.GetMouseButtonDown(0) && !cooldown && player_shooting)
        {
            clip_remaining--;
			switch (player.get_shooting_type())
			{
				case 0:
					StartCoroutine("player_firing");
					break;
				case 1:
					StartCoroutine("player_burst");
					break;
				case 2:
					StartCoroutine("player_spread");
					break;
				case 3:
					//Piercing (TBD)
					break;
			}
			if (clip_remaining <= 0)
			{
				StartCoroutine("player_reload");
			}
        }

		/* Allow player to manually reload */
		if (Input.GetButtonDown("Fire3") && !cooldown && player_shooting)
        {
	        StartCoroutine("player_reload");
        }

    }

    IEnumerator enemy_firing()
    {
        /* While enemy is alive, fire periodically every $firing_speed */
        while (true)
        {
            /* Testing */
            audio.PlayOneShot(shootSound);
            enemy_counter++;
            Debug.Log("Enemy shot: " + enemy_counter);
            
            raycast("Player", Vector3.forward);
			laser.enabled = true;
			yield return new WaitForSeconds(0.5f);
			laser.enabled = false;
            yield return new WaitForSeconds(firing_speed);
        }
    }
    
    IEnumerator player_firing()
    {
        /* Testing */
        audio.PlayOneShot(shootSound);
        player_counter++;
        Debug.Log("Player shot: " + player_counter);
        
        /* Search for enemy within raycast*/
        raycast("Enemy", Vector3.forward);
        laser.enabled = true;
		yield return new WaitForSeconds(0.1f);
		laser.enabled = false;
        ammo_text.text = "Current Ammo: " + clip_remaining + "/" + clip_size;
    }

	IEnumerator player_burst()
    {
        /* Testing */
        player_counter++;
        Debug.Log("Player shot: " + player_counter);
        
        /* Search for enemy within raycast*/
        audio.PlayOneShot(shootSound);
        raycast("Enemy", Vector3.forward);
        laser.enabled = true;
		yield return new WaitForSeconds(0.1f);
		laser.enabled = false;
		yield return new WaitForSeconds(0.1f);
		audio.PlayOneShot(shootSound);
		raycast("Enemy", Vector3.forward);
        laser.enabled = true;
		yield return new WaitForSeconds(0.1f);
		laser.enabled = false;
		yield return new WaitForSeconds(0.1f);
		audio.PlayOneShot(shootSound);
		raycast("Enemy", Vector3.forward);
        laser.enabled = true;
		yield return new WaitForSeconds(0.1f);
		laser.enabled = false;
        ammo_text.text = "Current Ammo: " + clip_remaining + "/" + clip_size;
    }

	IEnumerator player_spread()
    {
        /* Testing */
        player_counter++;
        Debug.Log("Player shot: " + player_counter);
        audio.PlayOneShot(shootSound);
        /* Search for enemy within raycast*/
        raycast("Enemy", Quaternion.Euler(0,-20,0) * Vector3.forward);
        laser.enabled = true;
		yield return new WaitForSeconds(0.1f);
		laser.enabled = false;
		audio.PlayOneShot(shootSound);
		raycast("Enemy", Vector3.forward);
        laser.enabled = true;
		yield return new WaitForSeconds(0.1f);
		laser.enabled = false;
		audio.PlayOneShot(shootSound);
		raycast("Enemy", Quaternion.Euler(0,20,0) * Vector3.forward);
        laser.enabled = true;
		yield return new WaitForSeconds(0.1f);
		laser.enabled = false;
        ammo_text.text = "Current Ammo: " + clip_remaining + "/" + clip_size;
    }

	IEnumerator player_reload()
    {
        /* Testing */
        reloadSource.PlayOneShot(reloadSource.clip);
        Debug.Log("Player reloading...");
        /* Prevent player from firing for their reload time (firing_speed) while ammo is restocked */
        ammo_text.text = "Reloading...";
		cooldown = true;
		yield return new WaitForSeconds(firing_speed);
		clip_remaining = clip_size;
        cooldown = false;
        ammo_text.text = "Current Ammo: " + clip_remaining + "/" + clip_size;
    }

    private void raycast(string tag, Vector3 shot)
    {
        /* Set raycast layers (as to avoid hitting colliders within environment) */
        RaycastHit hit;
        int layerMask = 1 << 3;
        layerMask = ~layerMask;
        laser.SetPosition(0, this.transform.position);
        if (Physics.Raycast(transform.position, transform.TransformDirection(shot), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.tag == tag)
            {
                /* The raycast has hit it's intended target, and we will deal damage appropriately */
                if (tag == "Player")
                {
                    hit.collider.GetComponent<PlayerController>().deal_damage(damage);
                    Debug.Log("Detected Player: Health now " + hit.collider.GetComponent<PlayerController>().get_cur_health());
                }
                else if (tag == "Enemy")
                {
                    hit.collider.GetComponent<EnemyScript>().deal_damage(damage);
                    Debug.Log("Detected Enemy: Health now " + hit.collider.GetComponent<EnemyScript>().get_health());
					if (hit.collider.GetComponent<EnemyScript>().get_health() <= 0)
					{
						AudioSource enem_die_noise = hit.collider.GetComponent<AudioSource>();
						enem_die_noise.playOnAwake = false;
						enem_die_noise.clip = deathSound;
						enem_die_noise.PlayOneShot(enem_die_noise.clip);
						player.add_money(hit.collider.GetComponent<EnemyScript>().get_money_drop());
						Debug.Log("New Schmoney Balance: " + player.get_money());
						Destroy(hit.collider.gameObject, enem_die_noise.clip.length);
					}
                }
            }
			else if (hit.collider.tag == "Box")
			{
				/* Handle box being shot by either enemy or player*/
				Debug.Log("Hit Box");
				hit.collider.GetComponent<BoxScript>().spawn_powerup();
				Destroy(hit.collider.gameObject);
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
        
        Debug.DrawRay(transform.position, transform.TransformDirection(shot) * 1000, Color.white);
        laser.SetPosition (1, hit.point+transform.TransformDirection(shot)*50);
    }

}
