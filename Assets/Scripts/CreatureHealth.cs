using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureHealth : MonoBehaviour
{
    [SerializeField] public int health = 20;
    SpriteRenderer sr;
    CreatureBehaviour behaviour;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        behaviour = GetComponent<CreatureBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (behaviour.alive && collision.CompareTag("Player Attack"))
        {
            health -= 5;
            StartCoroutine(DamageFlash());
            PlayerStats.Instance.MP.Add(25);
            if (health <= 0)
            {
                Dies();
            }
        }
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < 2; i++) {
            sr.color = new Color(1,1,1,0.4f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public virtual void Dies()
    {
        behaviour.alive = false;
        sr.enabled = false;
    }
}
