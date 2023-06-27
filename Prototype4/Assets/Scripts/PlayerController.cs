using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    public float speed;
    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;
    public PowerUpType currentPowerUpType = PowerUpType.None;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    [Header("Rocket PowerUp Attributes")]
    public GameObject rocketPrefab;
    [SerializeField] private float powerUpStrength = 15f;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;

    [Header("Smash PowerUp Attributes")]
    [SerializeField] private float cooldownTime;
    [SerializeField] private float smashSpeed;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    private bool smashing = false;
    private float floorY;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(forwardInput * speed * focalPoint.transform.forward);
        powerUpIndicator.transform.position = transform.position;

        if (currentPowerUpType == PowerUpType.Rocket && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }

        if (currentPowerUpType == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing) 
        {
            smashing = true;
            StartCoroutine(Smashing());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            currentPowerUpType = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            powerUpIndicator.SetActive(true);
            if (powerupCountdown != null)
            {
                StopCoroutine(PowerUpCountdown());
            }
            powerupCountdown = StartCoroutine(PowerUpCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUpType == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounceDirection = (collision.gameObject.transform.position - transform.position);
            enemyRb.AddForce(bounceDirection * powerUpStrength, ForceMode.Impulse);
        }
    }

    void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketHoming>().fireRockets(enemy.transform);
        }
    }
    
    private IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerUpType = PowerUpType.None;
        powerUpIndicator.SetActive(false);
    }

    private IEnumerator Smashing()
    {
        var enemies = FindObjectsOfType<Enemy>();
        floorY = transform.position.y;

        float jumpTime = Time.time + cooldownTime;
        while (Time.time < jumpTime)
        {
            playerRb.velocity = new(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        while (transform.position.y > floorY)
        {
            playerRb.velocity = new(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i])
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, 
                                                    transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
        smashing = false;
        if (powerupCountdown != null)
        {
            StopCoroutine(PowerUpCountdown());
        }
        hasPowerUp = false;
        currentPowerUpType = PowerUpType.None;
        powerUpIndicator.SetActive(false);
    }
}
