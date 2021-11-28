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
    private float speed = 10;
    // Start is called before the first frame update
    /* Player attributes */
    [SerializeField] private int health = 100;
    [SerializeField] private float movement_speed = 4;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attack_speed = 2.5f;
    [SerializeField] private int schmoney = 0;
    public List<GameObject> barriers = new List<GameObject>();
    private MapController mc;
    /* Actions to call before first frame */
    void Start()
    {
        controller = GetComponent<CharacterController>();
        mc = GameObject.FindGameObjectWithTag("MapController").GetComponent<MapController>();
    }
    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            for (int i = 0; i < barriers.Count; i++)
            {
                barriers[i].GetComponent<BoxCollider>().isTrigger = true;
            }
        }
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
         * Found this nifty set of equations on the following forum post, made slight modifications to fit our needs:
         * https://forum.unity.com/threads/rotating-an-object-to-face-the-mouse-location.21342/
         */
        float h = Input.mousePosition.x - Screen.width / 2;
        float v = Input.mousePosition.y - Screen.height / 2;
        float angle = -Mathf.Atan2(v,h) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, angle-90, 0);
        
        /* Check Health */
        if (health <= 0)
        {
            // Call some form of "game over" here
            Destroy(this.gameObject);
        }
        
    }

    /* Getters and Setters */
    public int get_health()
    {
        return health;
    }

    public int get_damage()
    {
        return damage;
    }

    public float get_movement_speed()
    {
        return movement_speed;
    }

    public float get_attack_speed()
    {
        return attack_speed;
    }

    public int get_money()
    {
        return schmoney;
    }

    public void set_health(int input_health)
    {
        health = input_health;
    }

    public void set_damage(int input_damage)
    {
        damage = input_damage;
    }

    public void set_movement_speed(float input_movement_speed)
    {
        movement_speed = input_movement_speed;
    }

    public void set_attack_speed(float input_attack_speed)
    {
        attack_speed = input_attack_speed;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy Spawner"))
        {
            temp_enemy = other.gameObject.transform.position;
            e.SpawnEnemies(temp_enemy, barriers);
        }
    }
    public void set_money(int input_money)
    {
        schmoney = input_money;
    }

    /* Ease of access for specific functions */
    public void deal_damage(int damage_dealt)
    {
        health -= damage_dealt;
    }

    public void add_money(int money_added)
    {
        schmoney += money_added;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stairs"))
        {
            mc.DeleteCurrentFloor();
        }
        if (other.gameObject.CompareTag("EnemyRoom"))
        {
            Debug.Log("Hi");
            UpdateBarriers(other.gameObject);
        }
    }

    private void UpdateBarriers(GameObject g)
    {
        GetChildObject(g.transform, "Enemy Spawner");
    }
    
    public void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                barriers.Add(child.gameObject);
                Debug.Log(child.gameObject.name);
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
}
