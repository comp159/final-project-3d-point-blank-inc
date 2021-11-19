using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;

    /* Player attributes */
    [SerializeField] private int health = 100;
    [SerializeField] private float movement_speed = 4;
    [SerializeField] private int damage = 1;
    [SerializeField] private float attack_speed = 2.5f;
    
    /* Actions to call before first frame */
    void Start()
    {
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
