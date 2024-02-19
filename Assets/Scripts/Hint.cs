using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{

    bool triggered = false;
    [SerializeField] string[] hintStringArray;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            if (collision.CompareTag("Player"))
            {
                EventBus.Publish<UIEvent>(new UIEvent(hintStringArray));
                triggered = true;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
