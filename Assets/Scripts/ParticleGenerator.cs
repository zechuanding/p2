using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{

    // Singleton
    public static ParticleGenerator Instance;

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

        EventBus.Subscribe<ParticleEvent>(InstantiateParticle);
    }

    [SerializeField] GameObject sparkParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void InstantiateParticle(ParticleEvent e)
    {
        // Define your rotation range limits
        float minRotationZ = 0f;
        float maxRotationZ = 60f;

        // Generate a random rotation around the Y axis
        float randomRotationZ = Random.Range(minRotationZ, maxRotationZ);

        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomRotationZ);

        if (e.type == ParticleEvent.particleT.SPARK)
        {
            GameObject obj = Instantiate(sparkParticle, e.position, randomRotation);
            StartCoroutine(DestoryParticle(obj));
        }
    }

    IEnumerator DestoryParticle(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}


public class ParticleEvent
{
    public enum particleT
    {
        NULL,
        SPARK,
        CREATURE_DAMAGE
    };

    public Vector3 position;
    public particleT type;
    public ParticleEvent(Vector3 position, particleT type=particleT.NULL)
    {
        this.position = position;
        this.type = type;
    }
}