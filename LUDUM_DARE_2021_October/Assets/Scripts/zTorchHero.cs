using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zTorchHero : MonoBehaviour
{
    [SerializeField] private GameObject hero;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.transform.position.z + " torch");
        var k = gameObject.transform.position;
        Debug.Log(hero.transform.position.z);
        gameObject.transform.position = new Vector3(hero.transform.position.x, hero.transform.position.y, -3);
        Debug.Log(gameObject.transform.position.z);
    }
}
