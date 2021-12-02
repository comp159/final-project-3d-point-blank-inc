using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private GameObject shopUI;
    private ShopScript shopScript;
    private GameObject temp;
    private GameObject shopKeeperSpawn;
    // Start is called before the first frame update
    void Start()
    {
        shopUI = GameObject.Find("ShopUI");
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

    public void ChangePosition(GameObject map)
    {
        Debug.Log(map);
        GetChildObject(map.transform, "Shop Keeper Spawn");
        Debug.Log(temp);
        transform.position = temp.transform.position;
    }
    
    private void GetChildObject(Transform parent, string _tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                temp = child.gameObject;
            }
            if (child.childCount > 0)
            {
                GetChildObject(child, _tag);
            }
        }
    }
}
