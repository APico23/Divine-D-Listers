using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickManage : MonoBehaviour
{

    float totalClicks;
    GameObject button;
    public Vector3 change;
    GameObject particles;
    public float change2;

    AudioSource charge;

    public void Start()
    {
        button = GameObject.Find("Orb");
        particles = GameObject.Find("magic");
        charge = gameObject.GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        ++totalClicks;
        Debug.Log(totalClicks);
        button.transform.localScale += change;
        var shape = particles.GetComponent<ParticleSystem>().shape;
        shape.radius += change2;
        charge.Play();
        
    }
    

}
