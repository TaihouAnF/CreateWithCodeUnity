using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketHoming : MonoBehaviour
{
    private Transform target;
    private float speed = 15f;
    private bool homing;

    private float pushStrength = 15f;
    private float aliveTimer = 5f;
    // Update is called once per frame
    void Update()
    {
        if (homing && target)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
            transform.LookAt(target);
        }
    }
    public void fireRockets(Transform targetTo)
    {
        target = targetTo;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (target) 
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 pushAwayDirection = -collision.GetContact(0).normal;
                enemyRb.AddForce(pushStrength * pushAwayDirection, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
