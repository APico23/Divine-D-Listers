using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExounosSpecial : MonoBehaviour
{
    public GameObject self;


    void Start()
    {
        StartCoroutine(Delete());
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(2.9f);
        Destroy(self);
    }
}
