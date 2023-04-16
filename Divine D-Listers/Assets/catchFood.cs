using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catchFood : MonoBehaviour
{

    AudioSource chomp;
    public counterSpecial count;

    private void Start()
    {
        chomp = gameObject.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        chomp.Play();
        count.count++;
        Destroy(other.gameObject, 0.2f);

    }

}
