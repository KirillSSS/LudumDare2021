using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zStart : MonoBehaviour
{
    
    void Start()
    {
        var k = gameObject.transform.position;
        gameObject.transform.position = new Vector3(k.x, k.y, (k.y - 60) * 0.001f);
    }

    
}
