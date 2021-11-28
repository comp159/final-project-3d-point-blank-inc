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
        Welcome();

        buyHealText = buyHeal.GetComponentInChildren<Text>();
        buyAttackText = buyAttack.GetComponentInChildren<Text>();
        buySpeedText = buyMoveSpeed.GetComponentInChildren<Text>();
        buyReloadText = buyReloadSpeed.GetComponentInChildren<Text>();

        buyHealText.text = "$" + healCost;
        buyAttackText.text = "S" + attackCost;
        buySpeedText.text = "$" + speedCost;
        buyReloadText.text = "$" + reloadCost;
    }

    public void BuyHeal()
    {
        if (player.get_money() < healCost)
        {
            shopText.SetText("Oof, not enough.\nYou need $" + (healCost - player.get_money()) + " more" );
        }
		else if (player.get_cur_health() == player.get_base_health())
		{
			shopText.SetText("You know you're fully healed right?\nMaybe I should sell intelligence boosts too...");
		}
        else
        {
            shopText.SetText("Hopefully that heals\nyour bullet wounds!");
            int temp = player.get_cur_health();
            temp = temp + 5;
            player.set_cur_health(temp);
            player.add_money(-healCost);
        }
    }

    public void Welcome()
    {
        shopText.text = "Welcome! Buy something \nor get out.";
    }

    public void BuyAttack()
    {
        if (player.get_money() < attackCost)
        {
            shopText.SetText("Oof, not enough.\nYou need $" + (attackCost - player.get_money()) + " more" );
        }
        else
        {
            shopText.SetText("Go get those gains!");
            int temp = player.get_damage();
            temp = temp + 5;
            player.set_damage(temp);
            player.add_money(-attackCost);
        }
    }

    public void BuySpeed()
    {
        if (player.get_money() < speedCost)
        {
            shopText.SetText("Oof, not enough.\nYou need $" + (speedCost - player.get_money()) + " more" );
        }
        else
        {
            shopText.SetText("Enjoy the clout!");
            float temp = player.get_movement_speed();
            temp = temp + 5;
            player.set_movement_speed(temp);
            player.add_money(-speedCost);
        }
    }

    public void BuyReload()
    {
        if (player.get_money() < reloadCost)
        {
            shopText.SetText("Oof, not enough.\nYou need $" + (reloadCost - player.get_money()) + " more" );
        }
        else
        {
            shopText.SetText("Thanks for your patronage!\nYou look all jittery now!");
            float temp = player.get_reload_speed();
            temp = temp - (temp * 0.1f);
            if (temp <= 0)
            {
                player.set_reload_speed(0.1f);
            }
            else
            {
                player.set_reload_speed(temp);
            }
            player.add_money(-reloadCost);
        }
    }
}
