using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManagementScript : MonoBehaviour
{
    private TextMeshProUGUI floorNumText;
    private TextMeshProUGUI schmoneyText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI atkPowerText;
    private TextMeshProUGUI reloadSpeedText;
    private TextMeshProUGUI moveSpdText;
    private String floorPrefix = "Current floor: ";
    private String schmoneyPrefix = "Schmoney: $";
    private String healthPrefix = "HP: ";
    private String powerPrefix = "Power: ";
    private String reloadSpeedPrefix = "Reload Spd: ";
    private String moveSpeedPrefix = "Spd: ";
    private int floorNum = 0;
    private int schmoney;
    private int curHealth;
    private int maxHealth;
    private int atkPower;
    private int reloadSpeed;
    private int moveSpd;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        player = temp.GetComponent<PlayerController>();
        
        Transform general_stats = transform.GetChild(0).GetChild(1);
        Transform player_stats = transform.GetChild(0).GetChild(0);
        
        floorNumText = general_stats.Find("Current Floor Text").GetComponent<TextMeshProUGUI>();
        schmoneyText = general_stats.Find("Schmoney Text").GetComponent<TextMeshProUGUI>();
        healthText = player_stats.Find("User Health").GetComponent<TextMeshProUGUI>();
        atkPowerText = player_stats.Find("Attack Power Text").GetComponent<TextMeshProUGUI>();
        reloadSpeedText = player_stats.Find("Attack Speed Text").GetComponent<TextMeshProUGUI>();
        moveSpdText = player_stats.Find("Movement Speed Text").GetComponent<TextMeshProUGUI>();

        schmoney = player.get_money();
        curHealth = player.get_health();
        atkPower = player.get_damage();
        reloadSpeed = (int)player.get_attack_speed();
        moveSpd = (int)player.get_movement_speed();
        
        floorNumText.text = floorPrefix + floorNum;
        schmoneyText.text = schmoneyPrefix + schmoney;
        healthText.text = healthPrefix + curHealth + "/" + maxHealth;
        atkPowerText.text = powerPrefix + atkPower;
        reloadSpeedText.text = reloadSpeedPrefix + reloadSpeed;
        moveSpdText.text = moveSpeedPrefix + moveSpd;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void UpdateAll()
    {
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
        schmoney = player.get_money();
        schmoneyText.SetText(schmoneyPrefix + schmoney);
    }

    private void UpdateHealth()
    {
        curHealth = player.get_health();
        healthText.SetText(healthPrefix + curHealth + "/" + maxHealth);
    }

    private void UpdateAtkPow()
    {
        atkPower = player.get_damage();
        atkPowerText.SetText(powerPrefix + atkPower);
    }

    private void UpdateAtkSpd()
    {
        reloadSpeed = (int)player.get_attack_speed();
        reloadSpeedText.SetText(reloadSpeedPrefix + reloadSpeed);
    }

    private void UpdateMoveSpd()
    {
        moveSpd = (int)player.get_movement_speed();
        moveSpdText.SetText(moveSpeedPrefix + moveSpd);
    }
}
