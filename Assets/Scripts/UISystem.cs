using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{

    [SerializeField] Text healthText;
    [SerializeField] Text guideText;
    [SerializeField] Image guideBackground;
    [SerializeField] Image MPBar;
    float MPFraction;

    [SerializeField] Image[] heartArray;

    // Singleton
    public static UISystem Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        EventBus.Subscribe<UIEvent>(DisplayGuideWrapper);
    }


    void Start()
    {
        guideText.text = "";
        guideBackground.enabled = false;
    }

    int HP;
    // Update is called once per frame
    void Update()
    {
        HP = PlayerHealth.Instance.HP.Get();

        healthText.text = "MP: " + PlayerStats.Instance.MP.Get() + "/" + PlayerStats.Instance.MP.GetMax();
        healthText.text += "\nHealth: " + HP + "/" + PlayerHealth.Instance.HP.GetMax();
        healthText.text += PlayerController.Instance.cheatMode ? "\nCHEAT MODE ON" : "";

        MPFraction = (float)PlayerStats.Instance.MP.Get() / (float)PlayerStats.Instance.MP.GetMax();
        MPBar.rectTransform.sizeDelta = new Vector2(200 * MPFraction, 12);


        if (heartArray.Length > 0)
        {
            for (int i = 0; i < heartArray.Length; i++)
            {
                    heartArray[i].enabled = i < HP;
            }
        }
    }

    Coroutine c;
    void DisplayGuideWrapper(UIEvent e)
    {
        if (c != null)
        {
            StopCoroutine(c);   // Overwrite the previous guide text
        }
        c = StartCoroutine(DisplayGuide(e));
    }
    IEnumerator DisplayGuide(UIEvent e)
    {
        if (!e.temporal)
        {
            PlayerController.Instance.AddMoveRestrict();
            guideBackground.enabled = true;
        }

        guideBackground.color = new Color(0, 0, 0, 0);
        guideText.color = new Color(1, 1, 1, 0);

        for (int i = 0; i < e.eventText.Length; i++)
        {
            guideText.text = e.eventText[i];

            Debug.Log("displaying message "+i);

            // Fade in
            for (int a = 0; a < 10; a++)
            {
                guideText.color += new Color(0, 0, 0, 0.1f);
                if (i == 0)
                    guideBackground.color += new Color(0, 0, 0, 0.05f);
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
                    if (Input.GetButtonUp("Jump") || Input.GetButtonDown("Attack")) 
                    {
                        break; 
                    }
                }
            }
            
            // Fade out
            for (int a = 0; a < 10; a++)
            {
                guideText.color += new Color(0, 0, 0, -0.1f);
                if (i == e.eventText.Length-1)
                    guideBackground.color += new Color(0, 0, 0, -0.06f);
                yield return null;
                yield return null;
            }
        }

        if (!e.temporal)
            PlayerController.Instance.RemoveMoveRestrict();

        guideText.text = "";
        guideBackground.enabled = false;
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
