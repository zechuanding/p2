using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Ahhhhh I hate this script
 * It needs to check if the boss is alive, and check the position of boss, and check the player's position
 * Can someone tell me how to improve this?
 */
public class BossHPBar : MonoBehaviour
{
    [SerializeField] Image HPBarContainer, HPBar;
    [SerializeField] GameObject boss;

    CreatureHealth bossHealth;
    CreatureBehaviour bossBehaviour;

    [SerializeField] Color green, yellow, red;

    private void Start()
    {
        bossHealth = boss.GetComponent<CreatureHealth>();
        bossBehaviour = boss.GetComponent<CreatureBehaviour>();
    }


    private void Update()
    {
        // Display HP bar only when player is near the boss
        if (bossBehaviour.alive && (boss.transform.position - PlayerController.Instance.transform.position).magnitude < 20)
        {
            HPBarContainer.enabled = true;
            HPBar.enabled = true;
        }
        else
        {
            HPBarContainer.enabled = false;
            HPBar.enabled = false;
        }

        // Set bar length
        float healthFraction = ((float)bossHealth.health / (float)bossHealth.maxHealth);
        HPBar.rectTransform.sizeDelta = new Vector2(400 * healthFraction, 12);

        // Set bar color
        HPBar.color = (healthFraction < 0.2f) ? red : (healthFraction < 0.4f) ? yellow : green;
    }
}
