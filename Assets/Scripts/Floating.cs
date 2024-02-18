using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{

    float startTime, ellapsedTime;
    Vector3 initialPos;
    [SerializeField] float speed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ellapsedTime = Time.time - startTime;
        transform.position = initialPos + new Vector3(0, 0.5f * Mathf.Sin(speed * ellapsedTime), 0);
    }

}
