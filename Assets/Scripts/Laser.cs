using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{


    [SerializeField] GameObject beam;
    [SerializeField] float delayTime = 0;
    [SerializeField] float duration = 3;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
    }


    IEnumerator Fire()
    {
        yield return new WaitForSeconds(delayTime);
        while (true)
        {
            beam.SetActive(!beam.activeSelf);
            yield return new WaitForSeconds(duration);
        }
        
    }
}
