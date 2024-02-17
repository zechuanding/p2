using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraSupply : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Attack"))
        {
            PlayerStats.Instance.MP.Add(25);
        }
    }
}
