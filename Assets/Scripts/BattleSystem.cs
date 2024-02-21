using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BattleSystem : MonoBehaviour
{

    //[SerializeField]
    [SerializeField] GameObject attackRight;
    [SerializeField] GameObject attackLeft;
    [SerializeField] GameObject attackUp;

    PlayerController controller;


    [SerializeField] float coolDown = 0.25f;
    float lastAttackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetCanMove() && Input.GetButtonDown("Attack") && Time.time > (lastAttackTime + coolDown))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GameObject attack = null;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w")) 
        {
            attack = attackUp;

            // Flip the sprite according to player facing
            Vector3 scale = attackUp.transform.localScale;
            scale.y = (controller.facing == Vector3.right) ? -1 : 1;
            attackUp.transform.localScale = scale;
        }
        else if (controller.facing == Vector3.right)
        {
            attack = attackRight;
        }
        else if (controller.facing == Vector3.left)
        {
            attack = attackLeft;
        }

        //Debug.Log("Player facing is " + controller.facing);
        if (attack != null)
        {
            attack.SetActive(true);
            lastAttackTime = Time.time;
            
            // Slash duration
            yield return new WaitForSeconds(0.25f);
            attack.SetActive(false);
        }
        
    }
}
