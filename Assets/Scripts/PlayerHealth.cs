using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Stat HP;

    [SerializeField] private bool invincible = false;

    private bool alive = true;
    public bool IsAlive() {  return alive; }

    SpriteRenderer sr;

    public GameObject checkPoint;

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
            if (!invincible && !PlayerController.Instance.cheatMode && e.collision.gameObject.GetComponent<CreatureBehaviour>().alive)
            {
                HP.Add(-1);
                StartCoroutine(DamageFlash());
            }
        }
        else if (e.collision.CompareTag("Acid") || e.collision.CompareTag("Beam"))
        {
            Debug.Log("Player's Hitbox has hit acid!");
            if (!invincible && !PlayerController.Instance.cheatMode)
            {
                HP.Add(-999);
                StartCoroutine(DamageFlash());
            }
        }

        if (e.collision.CompareTag("Check Point"))
        {
            checkPoint = e.collision.gameObject;
            HP.Maximize();
            PlayerStats.Instance.SaveStats();
            EventBus.Publish(new UIEvent(new string[] { "Game Saved" }, true));
        }
    }


    IEnumerator DamageFlash()
    {
        for (int i = 0; i < 2; i++)
        {
            sr.color = new Color(1, 0, 0, 0.8f);
            yield return new WaitForSeconds(0.1f);
            sr.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);
        }

    }



    IEnumerator Dying()
    {
        Debug.Log("You Died!");
        alive = false;
        PlayerController.Instance.AddMoveRestrict();
        transform.Rotate(0, 0, 90 * -PlayerController.Instance.facing.x);
        yield return new WaitForSeconds(1.5f);
        Respawn();
    }


    void Respawn()
    {
        HP.Maximize();
        PlayerController.Instance.ClearAllMoveRestrict();
        PlayerStats.Instance.LoadStats();
        transform.position = checkPoint.transform.position;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        alive = true;

        EventBus.Publish(new PlayerRespawnEvent());
    }
}

public class PlayerRespawnEvent
{

}
