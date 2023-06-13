using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool hasPowerUp = false;
    public GameObject powerUpIndicator;
    public powerUpType currentPowerUpType = powerUpType.None;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    private float powerUpStrength = 15f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    
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

        if (currentPowerUpType == powerUpType.Rocket && Input.getKeyDown(KeyCode.F))
        {
            launchRockets();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            currentPowerUpType = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            powerUpIndicator.gameObject.SetActive(true);
            if (powerupCountdown != null)
            {
                StopCoroutine(PowerUpCountdown());
            }
            powerupCountdown = StartCoroutine(PowerUpCountdown());
        }
    }

    private IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerUpType = powerUpType.None;
        powerUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUpType == powerUpType.Pushback)
        {
            Debug.Log("Player collided with: " + collision.gameObject.name + " with powerup set to " + currentPowerUpType.ToString());

            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounceDirection = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(bounceDirection * powerUpStrength, ForceMode.Impulse);
        }
    }

    void launchRockets()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketHoming>().fireRockets(enemy.transform);
        }
    }
}
