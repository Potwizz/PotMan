using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
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
        ChargeY();
    }

    public void ChargeY()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

}
