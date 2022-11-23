using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private float yBound = 4;
    private float xBound = 11;

    private Rigidbody2D enemyRb;
    private GameObject player;

    private bool playerAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (playerAlive == true) 
        {
            Vector2 lookDirection = (player.transform.position - transform.position); // The vector position to the player from the enemy

            enemyRb.AddForce(lookDirection * speed, ForceMode2D.Impulse); // Movement toward the player

            ConstrainEnemyMovement();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            Destroy(collision.gameObject);
            playerAlive = false;
            Debug.Log("Game Over");
        }
    }

    void ConstrainEnemyMovement() // Player bounds
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
