using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    private int healCost, attackCost, speedCost, reloadCost;
    private TextMeshProUGUI shopText;
    private PlayerController player;
    private TextMeshProUGUI buyHeal, buyAttack, buyMoveSpeed, buyReloadSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        healCost = 5;
        attackCost = 10;
        speedCost = 10;
        reloadCost = 10;
        
        Transform buyingButtons = transform.Find("Buying Buttons");

        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        player = temp.GetComponent<PlayerController>();

        buyHeal = buyingButtons.Find("Buy Heal").Find("Buy Heal Price").GetComponent<TextMeshProUGUI>();
        buyAttack = buyingButtons.Find("Buy Attack").Find("Buy Attack Price").GetComponent<TextMeshProUGUI>();
        buyMoveSpeed = buyingButtons.Find("Buy MoveSpeed").Find("Buy Move Speed Price").GetComponent<TextMeshProUGUI>();
        buyReloadSpeed = buyingButtons.Find("Buy ReloadSpeed").Find("Buy Reload Speed Price").GetComponent<TextMeshProUGUI>();
        
        shopText = transform.Find("Shop Text Background").Find("Shop Text").GetComponent<TextMeshProUGUI>();
        shopText.text = "Welcome! Buy something \nor get out.";
        
        buyHeal.text = "$"+ healCost;
        buyAttack.SetText("$" + attackCost);
        buyMoveSpeed.SetText("$" + speedCost);
        buyReloadSpeed.SetText("$" + reloadCost);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyHeal()
    {
        if (player.get_money() < 5)
        {
            shopText.SetText("Oof, not enough.\nYou need $" + (5 - player.get_money()) + " more" );
        }
        else
        {
            shopText.SetText("Enjoy your purchase!");
            int temp = player.get_health();
            temp = temp + 5;
            player.set_health(temp);
            player.add_money(-5);
        }
    }
}
