using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLaser : MonoBehaviour
{
    [SerializeField] GameObject beam;
    [SerializeField] float speed = 0.1f;
    [SerializeField] LayerMask layerMask;
    //37 69

    // Start is called before the first frame update
    void Start()
    {
        
    }

    RaycastHit2D hit;
    Vector3 yOffset = new Vector3(0, -0.5f, 0);
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed;
        if (transform.position.y < 37)
        {
            transform.position = new Vector3(transform.position.x, 70, transform.position.z);
        }

        hit = Physics2D.Raycast(transform.position + yOffset, Vector3.right, 20, layerMask);
        beam.transform.localScale = new Vector3(1, hit.distance, 1);

    }
}
