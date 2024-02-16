using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Stat HP;

    [SerializeField] private bool invincible = false;

    private bool alive = true;

    SpriteRenderer sr;

    GameObject checkPoint;

    // Singleton
    public static PlayerHealth Instance;
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

        HP = new Stat(5);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<PlayerTriggerEvent>(OnPlayerHitboxTriggerd);
        sr = GetComponent<SpriteRenderer>();

        // Set the intial check point
        checkPoint = GameObject.Find("Check Point 0");
    }

    private void Update()
    {
        if (alive && HP.Get() <= 0)
        {
            StartCoroutine(Dying());
        }
    }


    void OnPlayerHitboxTriggerd(PlayerTriggerEvent e)
    {
        if (e.collision.CompareTag("Enemy"))
        {
            Debug.Log("Player's Hitbox has hit an enemy!");
            if (!invincible && !PlayerController.Instance.cheatMode)
            {
                HP.Add(-1);
                StartCoroutine(DamageFlash());
            }
        }
    }


    IEnumerator DamageFlash()
    {
        for (int i = 0; i < 2; i++)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            yield return new WaitForSeconds(0.1f);
        }

    }



    IEnumerator Dying()
    {
        Debug.Log("You Died!");
        alive = false;
        PlayerController.Instance.AddMoveRestrict();
        yield return new WaitForSeconds(3);
        Respawn();
    }


    void Respawn()
    {
        HP.Load();
        transform.position = checkPoint.transform.position;
        PlayerController.Instance.ClearAllMoveRestrict();
        PlayerStats.Instance.LoadStats();
    }
}
