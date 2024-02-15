using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AuroraPlatform : MonoBehaviour
{
    [SerializeField] float existingTime = 8;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(PlatformLifeCycle());
    }

    IEnumerator PlatformLifeCycle()
    {
        yield return new WaitForSeconds(existingTime);
        for (int i = 0; i < 5; i++)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.4f);
            yield return new WaitForSeconds(0.2f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            yield return new WaitForSeconds(0.2f);
        }
        
        Destroy(gameObject);
    }
}
