using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stat MP;
    public Stat ATK;

    // Singleton
    public static PlayerStats Instance;
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

        MP = new Stat(100);
        ATK = new Stat(5);
    }
    void Start()
    {
        StartCoroutine(AutoRefillMP());
    }


    void Update()
    {
        
    }


    public void LoadStats()
    {
        MP.Load();
        ATK.Load();
    }

    public void SaveStats()
    {
        MP.Save();
        ATK.Save();
    }

    [SerializeField] float refillInterval = 1;
    IEnumerator AutoRefillMP()
    {
        while (true)
        {
            yield return new WaitForSeconds(refillInterval);
            if (PlayerHealth.Instance.IsAlive())
                MP.Add(1);
        }
    }

}
