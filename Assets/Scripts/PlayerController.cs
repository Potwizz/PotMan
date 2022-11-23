using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public SpawnManager spawner;

    public float speed;

    public int score = 0;

    private float yBound = 4;
    private float xBound = 11;

    public bool hasPowerup = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calling the move player function/method in update
        MovePlayer();

        // Calling the constrain player movement function/method
        ConstrainPlayerMovement();
    }

    // Moves the player based on WASD/arrow keys
    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector2.up * speed * verticalInput);
        playerRb.AddForce(Vector2.right * speed * horizontalInput);
    }

    private void OnTriggerEnter2D(Collider2D other) // Remember to make trigger for 2D
    {
        if (other.CompareTag("powerup")) // Pickup powerup and start timer for spawning a new one
        {
            hasPowerup = true;
            speed = 3;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCooldown());
        }
        else
        {
            hasPowerup = false;
            speed = 1;
        }

        if (other.CompareTag("potato")) // Collect potatos and add it to the score
        {
            Destroy(other.gameObject);
            score++;
            Debug.Log(score);

        }
    }

    IEnumerator PowerupCooldown() // The cooldown for whena powerup should spawn efter having been picked up
    {
        hasPowerup = false;
        yield return new WaitForSeconds(3);
        spawner.SpawnPowerup();
    }

    // Prevent player from leaving the screen on all sides. Boundary
    void ConstrainPlayerMovement()
    {
        if (transform.position.y < -yBound)
        {
            transform.position = new Vector2(transform.position.x, -yBound);
        }

        if (transform.position.y > yBound)
        {
            transform.position = new Vector2(transform.position.x, yBound);
        }

        if (transform.position.x < -xBound)
        {
            transform.position = new Vector2(-xBound, transform.position.y);
        }

        if (transform.position.x > xBound)
        {
            transform.position = new Vector2(xBound, transform.position.y);
        }
    }
}
