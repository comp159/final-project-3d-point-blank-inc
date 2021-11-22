using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private GameObject enemy;

    private GameObject healthBarBackground;

    private GameObject healthBarFill;

    private Image healthBarFillImage;

    private float maxHP;

    private float currHP;
    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.transform.parent.gameObject;
        maxHP = enemy.GetComponent<EnemyScript>().get_health();
        healthBarBackground = gameObject.transform.GetChild(0).gameObject;
        healthBarFill = gameObject.transform.GetChild(1).gameObject;
        healthBarFillImage = healthBarFill.gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarFillImage.fillAmount = enemy.GetComponent<EnemyScript>().get_health() / maxHP;
    }
}
