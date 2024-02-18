using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{

    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            if (collision.CompareTag("Player"))
            {
                EventBus.Publish<UIEvent>(new UIEvent(new string[] { "Press J or X to attak" }));
                triggered = true;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
