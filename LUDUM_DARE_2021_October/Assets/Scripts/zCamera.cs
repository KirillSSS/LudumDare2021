using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var k = gameObject.transform.position;
        gameObject.transform.position = new Vector3(k.x, k.y, -200);
    }
}
