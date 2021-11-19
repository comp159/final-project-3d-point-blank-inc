using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    private float viewRadius = 99f;
    private NavMeshAgent currAgent;
    private Transform currTarget;
    
    /* Enemy attributes */
    [SerializeField] private int health = 50;
    [SerializeField] private float movement_speed = 2;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attack_speed = 4f;
    [SerializeField] private int money_drop = 1;

    /* Actions to call before first frame */
    void Start()
    {
        //currAgent = GetComponent<NavMeshAgent>();
        //currTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    /* Continous updates per frame */
    void Update()
    {
        //float distance = Vector3.Distance(currTarget.position, transform.position);
        //if (distance <= viewRadius)
        //{
        //    currAgent.SetDestination(currTarget.position);
        //}
    }

    private void FixedUpdate()
    {

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

    public int get_money_drop()
    {
        return money_drop;
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
    
    /* Ease of access to deal damage */
    public void deal_damage(int damage_dealt)
    {
        health -= damage_dealt;
    }
    
}
