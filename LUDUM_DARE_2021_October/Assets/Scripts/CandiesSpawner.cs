using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandiesSpawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject candy;
    [SerializeField] private GameObject[] candySpawner;

    private Vector3 startPos;

    private void Start()
    {
        //int i = Random.Range(0, candySpawner.Length);

        //startPos = candySpawner[i].transform.position;

        for(int i = 0; i< candySpawner.Length; i++)
            Instantiate(candy, candySpawner[i].transform.position, Quaternion.identity);

        startPos.x = 0;
        startPos.y = 0;
        startPos.z = 0;
    }

    private IEnumerator SpawnCandies()
    {
        yield return new WaitForSeconds(spawnDelay);
        //int[] p;
        //int o = 0;

        //for (int j = 0; j < candySpawner.Length; j++)
        //    for (int i = 0; i < GameObject.FindGameObjectsWithTag("Candy").Length; i++)
        //    {
        //        if (GameObject.FindGameObjectsWithTag("Candy")[i].transform.position == candySpawner[j].transform.position)
        //        {
        //            p[o] = j;
        //        }
        //    }

        //for (int j = 0; j < candySpawner.Length; j++)
        //    for (int i = 0; i < p.Length; i++)
        //        if (j != p[i])
        //            Instantiate(candy, startPos, Quaternion.identity);

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Candy").Length; i++)
            Destroy(GameObject.FindGameObjectsWithTag("Candy")[i]);

        for (int i = 0; i < candySpawner.Length; i++)
            Instantiate(candy, candySpawner[i].transform.position, Quaternion.identity);
    }

    public void Spawn() => StartCoroutine(SpawnCandies());
}
