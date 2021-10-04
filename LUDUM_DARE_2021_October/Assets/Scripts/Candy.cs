using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    private CandiesSpawner candiesSpawner;

    private void OnDestroy()
    {
        candiesSpawner = GameObject.Find("Boiler").GetComponent<CandiesSpawner>();
        print("candy" + gameObject.name + gameObject.transform.position);
        candiesSpawner.Spawn();
    }
}
