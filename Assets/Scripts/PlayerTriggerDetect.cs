using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerDetect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventBus.Publish<PlayerTriggerEvent>(new PlayerTriggerEvent(collision));
    }
}

public class PlayerTriggerEvent
{
    public Collider2D collision;
    public PlayerTriggerEvent(Collider2D _collision)
    {
        collision = _collision;
    }
}
