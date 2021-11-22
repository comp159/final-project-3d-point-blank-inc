using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.rotation = Camera.main.gameObject.transform.rotation;
        gameObject.transform.LookAt(camera.gameObject.transform);
    }
}
