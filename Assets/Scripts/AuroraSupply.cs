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
            EventBus.Publish<ParticleEvent>(new ParticleEvent(Vector3.Lerp(collision.transform.position, transform.position, 0.8f), ParticleEvent.particleT.SPARK));
        }
    }
}
