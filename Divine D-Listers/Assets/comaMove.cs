using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comaMove : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    public float moveSpeed = 5f;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        horizontal *= moveSpeed;

        body.velocity = new Vector2(horizontal * moveSpeed, 0);
    }
}
