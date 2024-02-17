using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        healthText.text = "Health: " + PlayerHealth.Instance.HP.Get()  + "/" + PlayerHealth.Instance.HP.GetMax();
        healthText.text += "\nMP: " + PlayerStats.Instance.MP.Get() + "/" + PlayerStats.Instance.MP.GetMax();
        healthText.text += PlayerController.Instance.cheatMode ? "\nCHEAT MODE ON" : "";
    }

    void DisplayGuideWrapper(UIEvent e)
    {
        StartCoroutine(DisplayGuide(e));
    }
    IEnumerator DisplayGuide(UIEvent e)
    {
        guideText.color = new Color(1, 1, 1, 0);
        for (int i = 0; i < e.eventText.Length; i++)
        {
            guideText.text = e.eventText[i];
            if (!e.temporal)
                guideText.text += "\n\n~Press ENTER to continue~";


            Debug.Log("displaying message "+i);

            // Fade in
            for (int a = 0; a < 10; a++)
            {
                guideText.color += new Color(0, 0, 0, 0.1f);
                yield return null;
                yield return null;
            }

            if (e.temporal)
            {
                yield return new WaitForSeconds(1);
            }
            else
            {
                while (true)
                {
                    yield return null;
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) { break; }
                }
            }
            
            // Fade out
            for (int a = 0; a < 10; a++)
            {
                guideText.color += new Color(0, 0, 0, -0.1f);
                yield return null;
                yield return null;
            }
        }
        
        guideText.text = "";
    }
}

public class UIEvent
{
    public string[] eventText;
    public bool temporal;
    public UIEvent(string[] _text, bool _temporal=false)
    {
        eventText = _text;
        temporal = _temporal;
    }
}
