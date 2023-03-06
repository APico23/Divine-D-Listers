using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float moveSpeed = 5f;

    public Animator anim;
    public float hf = 0.0f;
    public float vf = 0.0f;

    public bool canMove;

    public VectorValue startingPosition;

    GameObject footstepsAudioObject;
    AudioSource footstepsAudio;
    bool audioPlayingNow = false;

    //SpriteRenderer spriteRenderer;
    //public Sprite[] spriteArray;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
        anim = this.GetComponent<Animator>();
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        canMove = true;
        footstepsAudioObject = GameObject.Find("footsteps");
        footstepsAudio = footstepsAudioObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            hf = body.velocity.x > 0.01f ? body.velocity.x : body.velocity.x < -0.01f ? 1 : 0;
            vf = body.velocity.y > 0.01f ? body.velocity.y : body.velocity.y < -0.01f ? 1 : 0;

            if (body.velocity.x < -0.01f)
            {
                this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            anim.SetFloat("Horizontal", hf);
            anim.SetFloat("Vertical", body.velocity.y);
            anim.SetFloat("Speed", vf);

        if (!canMove)
        {
            horizontal = 0;
            vertical = 0;
            body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
            footstepsAudio.Stop();
            audioPlayingNow = false;
        }

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (horizontal != 0 && vertical != 0)
            {

                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }
            if ((horizontal != 0 || vertical != 0) && (audioPlayingNow == false))
            {
                footstepsAudio.Play();
                audioPlayingNow = true;
            }
            if (horizontal == 0 && vertical == 0)
            {
                footstepsAudio.Stop();
                audioPlayingNow = false;
            }

            //if (horizontal != 0)
            //{
            //    if (horizontal > 0)
            //    {
            //        spriteRenderer.sprite = spriteArray[3];
            //    }else
            //    {
            //        spriteRenderer.sprite = spriteArray[2];
            //    }
            //}
            //if (vertical != 0)
            //{
            //    if (vertical > 0)
            //    {
            //        spriteRenderer.sprite = spriteArray[1];
            //    }
            //    else
            //    {
            //        spriteRenderer.sprite = spriteArray[0];
            //    }
            //}
            body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }
    }
}
