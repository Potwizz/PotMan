using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBot : MonoBehaviour
{
    private GameManager gameManager;

    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChargeX();
    }

    public void ChargeX()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
