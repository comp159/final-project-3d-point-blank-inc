using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private EnemySpawner e;
    private Vector3 dir;
    private Vector3 temp_enemy;
    
    // Start is called before the first frame update
    /* Player attributes */
    [SerializeField] private int base_health = 100;
    private int cur_health;
    [SerializeField] private float movement_speed = 4;
    [SerializeField] private int damage = 1;
    [SerializeField] private float reload_speed = 2.5f;
    [SerializeField] private int schmoney = 0;
    [SerializeField] private int clip_size = 10;
    
    /* Actions to call before first frame */
    void Start()
    {
        cur_health = base_health;
        controller = GetComponent<CharacterController>();
    }

    /* Continous updates per frame */
    void FixedUpdate()
    {
        /* Movement Information */
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        dir.x = horizontal * movement_speed;
        dir.z = vertical * movement_speed;
        controller.Move(dir * Time.deltaTime);
        
        /*
         * Rotation
         * Found this nifty set of equations on the following Youtube video- it offered an improved accuracy compared to
         * our initial rotation equation, and I made slight modifications to fit our specific game:
         * https://www.youtube.com/watch?v=_S91dfkZ4oI
         */

        Plane player_plane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0f;

        if (player_plane.Raycast(ray, out hitDist))
        {
            Vector3 target = ray.GetPoint(hitDist);
            Quaternion target_rotation = Quaternion.LookRotation(target - transform.position);
            target_rotation.x = 0;
            target_rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, 10f * Time.deltaTime);
        }
        
        /* Check Health */
        if (cur_health <= 0)
        {
            // Call some form of "game over" here
            Destroy(this.gameObject);
        }
        
    }

    /* Getters and Setters */
    public int get_base_health()
    {
        return base_health;
    }

    public int get_cur_health()
    {
        return cur_health;
    }

    public int get_damage()
    {
        return damage;
    }

    public float get_movement_speed()
    {
        return movement_speed;
    }

    public float get_reload_speed()
    {
        return reload_speed;
    }

    public int get_money()
    {
        return schmoney;
    }

    public int get_clip_size()
    {
        return clip_size;
    }

    public void set_base_health(int input_health)
    {
        base_health = input_health;
    }
    
    public void set_cur_health(int input_health)
    {
        cur_health = input_health;
    }

    public void set_damage(int input_damage)
    {
        damage = input_damage;
    }

    public void set_movement_speed(float input_movement_speed)
    {
        movement_speed = input_movement_speed;
    }

    public void set_reload_speed(float input_reload_speed)
    {
        reload_speed = input_reload_speed;
    }

    public void set_clip_size(int input_clip_size)
    {
        clip_size = input_clip_size;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Spawner"))
        {
            temp_enemy = other.gameObject.transform.position;
            e.SpawnEnemies(temp_enemy);
            Destroy(other.gameObject);
        }
    }
    public void set_money(int input_money)
    {
        schmoney = input_money;
    }

    /* Ease of access for specific functions */
    public void deal_damage(int damage_dealt)
    {
        cur_health -= damage_dealt;
    }

    public void add_money(int money_added)
    {
        schmoney += money_added;
    }
    
}
