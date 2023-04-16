using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterSpecial : MonoBehaviour
{
    public int count;

    void Start()
    {
        count = 0;
    }

    public int getCount()
    {
        return count/10;
    }
}
