using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private GameObject shopUI;
    private ShopScript shopScript;

    private GameObject shopKeeperSpawn;
    // Start is called before the first frame update
    void Start()
    {
        shopUI = GameObject.FindGameObjectWithTag("ShopScreen");
        shopScript = shopUI.GetComponent<ShopScript>();
        shopUI.SetActive(false);
        Time.timeScale = 1;
        shopKeeperSpawn = GameObject.FindGameObjectWithTag("Shop Keeper Spawn");
        this.transform.position = shopKeeperSpawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("ui should appear active now");
        shopUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void LeaveShop()
    {
        shopScript.Welcome();
        shopUI.SetActive(false);
        Time.timeScale = 1;
    }
}
