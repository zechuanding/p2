using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    [SerializeField] Text healthText;
    [SerializeField] Text guideText;

    private void Awake()
    {
        EventBus.Subscribe<UIEvent>(DisplayGuideWrapper);
    }


    void Start()
    {
        guideText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + PlayerHealth.Instance.HP.Get();
        healthText.text += "\nMP: " + PlayerStats.Instance.MP.Get();
        healthText.text += PlayerController.Instance.cheatMode ? "\nCHEAT MODE ON" : "";
    }

    void DisplayGuideWrapper(UIEvent e)
    {
        StartCoroutine(DisplayGuide(e));
    }
    IEnumerator DisplayGuide(UIEvent e)
    {
        for (int i = 0; i < e.eventText.Length; i++)
        {
            guideText.text = e.eventText[i];
            guideText.text += "\n\n~Press ENTER to continue~";
            Debug.Log("displaying message "+i);
            while (true)
            {
                yield return null;
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) { break; }
            }
        }
        
        guideText.text = "";
    }
}

public class UIEvent
{
    public string[] eventText;
    public UIEvent(string[] _text)
    {
        eventText = _text;
    }
}
