using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickManage : MonoBehaviour
{

    
    GameObject button;
    public Vector3 change;
    GameObject particles;
    public float change2;

    public counterSpecial count;

    AudioSource charge;

    public void Start()
    {
        button = GameObject.Find("Orb");
        particles = GameObject.Find("magic");
        charge = gameObject.GetComponent<AudioSource>();
    }

    public void OnMouseDown()
    {
        count.count++;
        button.transform.localScale += change;
        var shape = particles.GetComponent<ParticleSystem>().shape;
        shape.radius += change2;
        charge.Play();
        
    }
    

}
