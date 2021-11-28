using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    private int healCost, attackCost, speedCost, reloadCost;
    private TextMeshProUGUI shopText;
    private Text buyHealText, buyAttackText, buySpeedText, buyReloadText;
    private PlayerController player;
    private Button buyHeal, buyAttack, buyMoveSpeed, buyReloadSpeed;

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

        buyHeal = buyingButtons.Find("Buy Heal").GetComponent<Button>();
        buyAttack = buyingButtons.Find("Buy Attack").GetComponent<Button>();
        buyMoveSpeed = buyingButtons.Find("Buy MoveSpeed").GetComponent<Button>();
        buyReloadSpeed = buyingButtons.Find("Buy ReloadSpeed").GetComponent<Button>();
        
        shopText = transform.Find("Shop Text Background").Find("Shop Text").GetComponent<TextMeshProUGUI>();
        shopText.text = "Welcome! Buy something \nor get out.";

        buyHealText = buyHeal.GetComponentInChildren<Text>();
        buyAttackText = buyAttack.GetComponentInChildren<Text>();
        buySpeedText = buyMoveSpeed.GetComponentInChildren<Text>();
        buyReloadText = buyReloadSpeed.GetComponentInChildren<Text>();

        buyHealText.text = "$" + healCost;
        buyAttackText.text = "S" + attackCost;
        buySpeedText.text = "$" + speedCost;
        buyReloadText.text = "$" + reloadCost;
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
