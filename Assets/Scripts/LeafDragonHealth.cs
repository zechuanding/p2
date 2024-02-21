using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeafDragonHealth : CreatureHealth
{
    public override void Dies()
    {
        GetComponent<CreatureBehaviour>().alive = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        EventBus.Publish<BossDieEvent>(new BossDieEvent());
        StartCoroutine(FadeOut());
    }


    //HealthBar
    private void Update()
    {
        if ((PlayerController.Instance.transform.position - transform.position).magnitude < 16)
        {

        }
    }

    IEnumerator FadeOut()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        for (int i = 10; i >= 0; i--)
        {
            sr.color = new Color(1, 1, 1, i * 0.1f);
            yield return new WaitForSeconds(0.2f);
        }
        sr.enabled = false;
    }
}

class BossDieEvent{ }