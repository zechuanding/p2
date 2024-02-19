using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<PlayerRespawnEvent>(OnPlayerRespawn);
        EventBus.Subscribe<BossDieEvent>(OnBossDie);
        StartCoroutine(AddMP());
    }

    IEnumerator AddMP()
    {
        while (true)
        {
            if (playerInBossRoom)
            {
                yield return new WaitForSeconds(0.2f) ;
                PlayerStats.Instance.MP.Add(1);
            }
            else
            {
                yield return null;
            }
        }
    }


    public bool playerInBossRoom = false;


    // ========================== Enter room ==========================
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerInBossRoom && collision.CompareTag("Player"))
        {
            playerInBossRoom = true;
            ZoomCameraWrapper(12);
            if (!bossDied)
            {
                roomDoorLeft.SetActive(true);
            }
        }
    }

    // ========================== Exit room ==========================
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInBossRoom = false;
            ZoomCameraWrapper(8);
        }
    }


    void ZoomCameraWrapper(float sizeTo = 8)
    {
        if (c != null)
            StopCoroutine(c);
        c = StartCoroutine(ZoomCamera(sizeTo));
    }
    Coroutine c;
    IEnumerator ZoomCamera(float sizeTo = 8)
    {
        float zoomDuration = 1;
        float zoomStartTime = Time.time;
        float sizeFrom = Camera.main.orthographicSize;
        while (Time.time-zoomStartTime < zoomDuration)
        {
            Camera.main.orthographicSize = Mathf.Lerp(sizeFrom, sizeTo, (Time.time - zoomStartTime) / zoomDuration);
            yield return null;
        }
        Camera.main.orthographicSize = sizeTo;
    }


    [SerializeField] GameObject roomDoorLeft;
    [SerializeField] GameObject roomDoorRight;

    void OnPlayerRespawn(PlayerRespawnEvent e)
    {
        roomDoorLeft.SetActive(false);
    }

    void OnBossDie(BossDieEvent e)
    {
        roomDoorLeft.SetActive(false);
        roomDoorRight.SetActive(false);
        bossDied = true;
    }

    bool bossDied = false;
}
