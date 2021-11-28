using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;
    private ShopScript shopScript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("ShopUI");
        shopScript = temp.GetComponent<ShopScript>();
        shopUI.SetActive(false);
        Time.timeScale = 1;
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
