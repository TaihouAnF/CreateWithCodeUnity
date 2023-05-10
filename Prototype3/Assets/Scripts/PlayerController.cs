using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private GameManager gameManager;
    public float jumpForce = 500;
    public float doubleJumpForce = 400;
    public float gravityModifier = 1;
    public bool isOnGround = true;
    public bool doubleJump = true;
    public bool isGameOver = false;
    public bool dashing = false;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            isOnGround = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && doubleJump && !isGameOver)
        {
            playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            playerAnim.Play("Running_Jump", 3, 0f);
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            doubleJump = false;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dashing = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        // In this case, although dashing will always be true when pressing shift, the code will always go to first case first so we don't have to worry about going to the second case
        else if (dashing)   
        {
            dashing = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        // Fixed when after double jumping and hitting obstacles and still playing dirt.
        if (isGameOver)
        {
            dirtParticle.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            doubleJump = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !isGameOver)
        {
            isGameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            dirtParticle.Stop();
            gameManager.GameOver();
        }
        
    }
}
