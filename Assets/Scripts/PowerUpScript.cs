using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        player = temp.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0,100 * Time.deltaTime,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("Heal"))
            {
                int temp = player.get_cur_health();
                temp = temp + 5;
                player.set_cur_health(temp);
                Debug.Log("User healed 5 hp, now has "+ temp + " hp");
                Destroy(this.gameObject);
            }

            if (this.gameObject.CompareTag("Stronk"))
            {
                int temp = player.get_damage();
                temp = temp + 5;
                player.set_damage(temp);
                Debug.Log("User buffed attack by 1, now has " + temp);
                Destroy(this.gameObject);
            }

            if (this.gameObject.CompareTag("Schpeed"))
            {
                float temp = player.get_movement_speed();
                temp = temp + 5;
                player.set_movement_speed(temp);
                Debug.Log("User is faster by 1, now has " + temp + " speed");
                Destroy(this.gameObject);
            }

            if (this.gameObject.CompareTag("Reload"))
            {
                float temp = player.get_reload_speed();
                temp = temp - (temp * 0.1f);
                if (temp <= 0)
                {
                    player.set_reload_speed(0.1f);
                }
                else
                {
                    player.set_reload_speed(temp);
                }
                Debug.Log("User can reload by " + temp + " speed now");
                Destroy(this.gameObject);
            }
        }
    }
}
