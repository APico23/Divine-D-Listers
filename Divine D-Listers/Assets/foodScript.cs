using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodScript : MonoBehaviour
{



    float maxVelocity = 10;

    Rigidbody2D body;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
