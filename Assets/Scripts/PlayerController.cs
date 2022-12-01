using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    private int speed = 3;

    private float yBound = 4;
    private float xBound = 9;

    public int score = 0;
    public int pointValue;

    public bool hasPowerup = false;
    
    // Start is called before the first frame update
    void Start()
    {

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

        transform.Translate(Vector2.up * speed * Time.deltaTime * verticalInput, Space.World);
        transform.Translate(Vector2.right * speed * Time.deltaTime * horizontalInput, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other) // Remember to make trigger for 2D
    {
        if (other.CompareTag("powerup")) // Pickup powerup and start timer for spawning a new one
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            speed = 5;
            StartCoroutine(PowerupCooldown());
        } 

        if (other.CompareTag("potato")) // Collect potatos and add it to the score
        {
            Destroy(other.gameObject);
            gameManager.UpdateScore(pointValue);
            Debug.Log(score);
        }

        if (other.CompareTag("enemy"))
        {
            Destroy(gameObject);
            // playerAlive = false;
            gameManager.GameOver();
            Debug.Log("Game Over");
        }
    }

    IEnumerator PowerupCooldown() // The cooldown for when a powerup should spawn efter having been picked up
    {
        yield return new WaitForSeconds(3);
        hasPowerup = false;
        speed = 3;
        gameManager.SpawnPowerup();
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
