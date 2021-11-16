using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 dir;

    private float speed = 4;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* Movement Information */
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        dir.x = horizontal * speed;
        dir.z = vertical * speed;
        controller.Move(dir * Time.deltaTime);
        
        /*
         * Rotation
         * Found this nifty set of equations on the following forum post:
         * https://forum.unity.com/threads/rotating-an-object-to-face-the-mouse-location.21342/
         */
        float h = Input.mousePosition.x - Screen.width / 2;
        float v = Input.mousePosition.y - Screen.height / 2;
        float angle = -Mathf.Atan2(v,h) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (0, angle, 0);
    }
}
