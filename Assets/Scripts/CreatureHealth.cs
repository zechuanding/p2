using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureHealth : MonoBehaviour
{
    [SerializeField] private int health = 20;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Attack"))
        {
            health -= 5;
            StartCoroutine(DamageFlash());
            PlayerStats.Instance.MP.Add(25);
        }
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < 2; i++) {
            sr.color *= 2;
            yield return new WaitForSeconds(0.1f);
            sr.color /= 2;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
