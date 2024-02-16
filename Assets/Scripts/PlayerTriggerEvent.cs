using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerEvent
{
    public Collider2D collision;
    public PlayerTriggerEvent(Collider2D _collision)
    {
        collision = _collision;
    }
}

