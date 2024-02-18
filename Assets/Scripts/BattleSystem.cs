using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{

    //[SerializeField]
    [SerializeField] GameObject attackRight;
    [SerializeField] GameObject attackLeft;

    PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.GetCanMove() && (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.J)))
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        GameObject attack = null;
        if (controller.facing == Vector3.right)
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
            yield return new WaitForSeconds(0.2f);
            attack.SetActive(false);
        }
        
    }
}
