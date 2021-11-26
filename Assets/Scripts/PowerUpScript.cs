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
                int temp = player.get_health();
                temp = temp + 1;
                player.set_health(temp);
                Debug.Log("User healed 1 hp, now has "+ temp + " hp");
                Destroy(this.gameObject);
            }

            if (this.gameObject.CompareTag("Stronk"))
            {
                int temp = player.get_damage();
                temp = temp + 1;
                player.set_damage(temp);
                Debug.Log("User buffed attack by 1, now has " + temp);
                Destroy(this.gameObject);
            }

            if (this.gameObject.CompareTag("Schpeed"))
            {
                float temp = player.get_movement_speed();
                temp = temp + 1;
                player.set_movement_speed(temp);
                Debug.Log("User is faster by 1, now has " + temp + " speed");
                Destroy(this.gameObject);
            }

            if (this.gameObject.CompareTag("Reload"))
            {
                //TODO update once the shooting script handles reload speed instead of attack speed
                Destroy(this.gameObject);
            }
        }
    }
}
