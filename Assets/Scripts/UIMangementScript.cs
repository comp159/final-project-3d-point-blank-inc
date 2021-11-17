using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMangementScript : MonoBehaviour
{
    private TextMeshProUGUI floorNumText;
    private TextMeshProUGUI schmoneyText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI atkPowerText;
    private TextMeshProUGUI atkSpeedText;
    private TextMeshProUGUI moveSpdText;
    private String floorPrefix = "Current floor: ";
    private String schmoneyPrefix = "Schmoney: $";
    private String healthPrefix = "HP: ";
    private String powerPrefix = "Atk Power: ";
    private String attackSpeedPrefix = "Atk Spd: ";
    private String moveSpeedPrefix = "Spd: ";
    private int floorNum = 0;
    private int schmoney = 0;
    private int curHealth = 0;
    private int maxHealth = 0;
    private int atkPower = 0;
    private int atkSpeed = 0;
    private int moveSpd = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        floorNumText = transform.GetChild(0).GetChild(1).Find("Current Floor Text").GetComponent<TextMeshProUGUI>();
        schmoneyText = transform.GetChild(0).GetChild(1).Find("Schmoney Text").GetComponent<TextMeshProUGUI>();
        healthText = transform.GetChild(0).GetChild(0).Find("User Health").GetComponent<TextMeshProUGUI>();
        atkPowerText = transform.GetChild(0).GetChild(0).Find("Attack Power Text").GetComponent<TextMeshProUGUI>();
        atkSpeedText = transform.GetChild(0).GetChild(0).Find("Attack Speed Text").GetComponent<TextMeshProUGUI>();
        moveSpdText = transform.GetChild(0).GetChild(0).Find("Movement Speed Text").GetComponent<TextMeshProUGUI>();

        floorNumText.text = floorPrefix + floorNum;
        schmoneyText.text = schmoneyPrefix + schmoney;
        healthText.text = healthPrefix + curHealth + "/" + maxHealth;
        atkPowerText.text = powerPrefix + atkPower;
        atkSpeedText.text = attackSpeedPrefix + atkSpeed;
        moveSpdText.text = moveSpeedPrefix + moveSpd;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO update the stat values with the function calls from the actual user's stats
        UpdateFloor();
        UpdateSchmoney();
        UpdateHealth();
        UpdateAtkPow();
        UpdateAtkSpd();
        UpdateMoveSpd();
    }

    private void UpdateFloor()
    {
    }

    private void UpdateSchmoney()
    {
    }

    private void UpdateHealth()
    {
    }

    private void UpdateAtkPow()
    {
    }

    private void UpdateAtkSpd()
    {
    }

    private void UpdateMoveSpd()
    {
    }
}
