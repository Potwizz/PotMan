using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float yAxis = -6f;
    private float xAxis = -15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < xAxis)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < yAxis)
        {
            Destroy(gameObject);
        }
    }
}
