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
    private bool AIActive;
    private int shootDelay = 50; //1 second in fixedUpdate
    private int shootCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        currAgent = GetComponent<NavMeshAgent>();
        currTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shootCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(currTarget.position, transform.position);
        if (distance <= viewRadius)
        {
            currAgent.SetDestination(currTarget.position);
            AIActive = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (AIActive)
        {
            shootCounter++;
        }

        if (shootCounter % shootDelay == 0)
        {
            shootCounter = 0;
            //Shoot();
        }
    }
}
