using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    GameObject Player;
    PlayerHealth playerHealth;

    [SerializeField] Text healthText;
    [SerializeField] Text guideText;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        playerHealth = Player.GetComponent<PlayerHealth>();
    }


    void Start()
    {
        guideText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + playerHealth.HP.Get();
        healthText.text += "\nMP: " + PlayerStats.Instance.MP.Get();
        healthText.text += PlayerController.Instance.cheatMode ? "\nCHEAT MODE ON" : "";
    }

    IEnumerator DisplayGuide()
    {
        //guideText.text = e.guideText;
        while (true)
        {   
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) { break; }
            yield return null;
        }
        guideText.text = "";
    }
}
