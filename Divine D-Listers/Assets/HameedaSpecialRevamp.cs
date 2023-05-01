using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HameedaSpecialRevamp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject self;


    void Start()
    {
        StartCoroutine(Delete()); 
    }

    private IEnumerator Delete() {
        yield return new WaitForSeconds(1f);
        Destroy(self); 
    }
}
