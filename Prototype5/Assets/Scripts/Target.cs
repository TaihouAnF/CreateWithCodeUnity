using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Target Attribute")]
    private Rigidbody targetRb;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxTorque;
    [SerializeField] private float xRange;
    [SerializeField] private float ySpawnPos;
    [SerializeField] private int score;
    [SerializeField] private int decreaseScore;
    [SerializeField] private ParticleSystem explosionParticle;
    
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Initialization
    private Vector3 RandomForce()
    {
        return Random.Range(minSpeed, maxSpeed) * Vector3.up;
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    #endregion

    #region Behavior
    private void OnMouseDown()
    {
        if (gameManager.isGameAlive) {
            gameManager.UpdateScore(score);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateScore(decreaseScore);
        }
    }
    #endregion
}
