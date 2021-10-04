using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiesSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject candy;

     private IEnumerator SpawnCandies(GameObject candy, Vector3 startPos)
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(candy, startPos, Quaternion.identity);
    }

    public void Spawn(GameObject g, Vector3 v) => StartCoroutine(SpawnCandies(g,v));
}
