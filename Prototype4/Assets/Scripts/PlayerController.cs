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
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Debug.Log("Collide with " + collision.gameObject.name +
                    "with power up is:" + hasPowerUp);
            
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 bounceDirection = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(bounceDirection * powerUpStrength, ForceMode.Impulse);
        }
    }
}
