using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLaser : MonoBehaviour
{
    [SerializeField] GameObject beam;
    [SerializeField] float speed = 3;
    [SerializeField] LayerMask layerMask;

    float lowY = 37;
    float highY = 81;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<LeverEvent>(OnLeverTriggered);
    }

    RaycastHit2D hit;
    Vector3 yOffset = new Vector3(0, -0.5f, 0);
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (speed > 0 && transform.position.y < lowY)
        {
            transform.position = new Vector3(transform.position.x, highY, transform.position.z);
        }
        else if (speed < 0 && transform.position.y > highY)
        {
            transform.position = new Vector3(transform.position.x, lowY, transform.position.z);
        }

        hit = Physics2D.Raycast(transform.position + yOffset, Vector3.right, 20, layerMask);
        beam.transform.localScale = new Vector3(1, hit.distance, 1);

    }

    void OnLeverTriggered(LeverEvent e)
    {
        speed = -speed;
    }
}
