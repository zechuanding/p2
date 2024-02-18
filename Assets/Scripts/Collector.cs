using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<PlayerTriggerEvent>(OnPlayerHitboxTriggerd);
    }

    void OnPlayerHitboxTriggerd(PlayerTriggerEvent e)
    {
        Collider2D collision = e.collision;
        if (collision.CompareTag("Collectible"))
        {
            if (collision.name == "Platform Skill Pickup")
            {
                PlayerController.Instance.hasPlatformAbility = true;
                Destroy(collision.gameObject);
                string[] messageList = new string[] {
                    "You acquired a new skill: Aurora Platform",
                    "Press DOWN or K to create a platform",
                    "It will cost your MP, so use wisely"
                };
                EventBus.Publish(new UIEvent(messageList));
            }

            else if (collision.name == "MP Upgrade")
            {
                PlayerStats.Instance.MP.AddMax(100);
                PlayerStats.Instance.MP.Maximize();
                EventBus.Publish(new UIEvent(new string[] {"MP limit increased!"}));
                Destroy(collision.gameObject);
            }
        }
    }
}
