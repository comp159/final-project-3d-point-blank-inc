using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarScript : MonoBehaviour
{
    private GameObject SHOPUI;
    private GameObject PAUSEUI;
    private GameObject HealthBarFill;
    private Image HealthBarFillImage;
    private GameObject Player;
    private GameObject HealthBarBackground;

    // Start is called before the first frame update
    void Start()
    {
        SHOPUI = GameObject.FindGameObjectWithTag("ShopScreen");
        PAUSEUI = GameObject.FindGameObjectWithTag("PauseScreen");
        Player = GameObject.FindGameObjectWithTag("Player");
        HealthBarBackground = gameObject.transform.GetChild(0).gameObject;
        HealthBarFill = HealthBarBackground.transform.GetChild(0).gameObject;
        HealthBarFillImage = HealthBarFill.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFillImage.fillAmount = (float)Player.gameObject.GetComponent<PlayerController>().get_cur_health() 
                                                             / Player.gameObject.GetComponent<PlayerController>().get_base_health();
        
    }
}
