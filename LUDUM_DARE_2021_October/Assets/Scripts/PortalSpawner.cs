using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] neededSpawner;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float personDelay;
    [SerializeField] private int enemiesInOneWave;
    [SerializeField] private int waves;
    [SerializeField] private GameObject timer;

    private int currentEnemyIndex = 0;
    private int currentWaveIndex;
    private int enemiesLeftToSpawn;
    private int currentSpawnerIndex = 0;

    private bool isFirst;

    private void Start()
    {
        enemiesLeftToSpawn = enemiesInOneWave;
        isFirst = true;
        print("start");
        LaunchWave();
    }

    private IEnumerator SpawnEnemyInWave()
    {
        if (enemiesLeftToSpawn > 0)
        {
            //print(isFirst);

            if (isFirst)
            {
                timer.GetComponent<timeWave>().startTimer(spawnDelay);
                yield return new WaitForSecondsRealtime(spawnDelay*1.35f);
            }
            else
                yield return new WaitForSecondsRealtime(personDelay);

            //print(enemy.Length + " ----> " + currentEnemyIndex);
            //print(enemy.Length + " ====> " + currentSpawnerIndex);

            Instantiate(enemy[currentEnemyIndex],
                neededSpawner[currentSpawnerIndex].transform.position,
                Quaternion.identity);

            enemiesLeftToSpawn--;

            currentEnemyIndex = Random.Range(0, enemy.Length);
            //print("enemy: " + currentEnemyIndex +", "+ enemy.Length);

            currentSpawnerIndex = Random.Range(0, neededSpawner.Length);
            //print("spawner: " + currentSpawnerIndex + ", " + neededSpawner.Length);

            isFirst = false;

            StartCoroutine(SpawnEnemyInWave());
        }
        else 
        {
            if (currentWaveIndex < waves - 1)
            {
                //print("end");
                currentWaveIndex++;
                enemiesLeftToSpawn = enemiesInOneWave;
                currentEnemyIndex = 0;
                currentSpawnerIndex = 0;
                isFirst = true;
            }
        }
    }

    public void LaunchWave() => StartCoroutine(SpawnEnemyInWave());
}


//[SerializeField] private Waves[] waves;

//private int currentEnemyIndex;
//private int currentWaveIndex;
//private int enemiesLeftToSpawn;

//private void Start()
//{
//    enemiesLeftToSpawn = waves[0].WaveSettings.Length;
//    LaunchWave();
//}

//private IEnumerator SpawnEnemyInWave()
//{
//    if (enemiesLeftToSpawn > 0)
//    {
//        yield return new WaitForSeconds(waves[currentWaveIndex].WaveSettings[currentEnemyIndex].SpawnDelay);

//        Instantiate(waves[currentWaveIndex].WaveSettings[currentEnemyIndex].Enemy,
//            waves[currentWaveIndex].WaveSettings[currentEnemyIndex].NeededSpawner.transform.position,
//            Quaternion.identity);

//        enemiesLeftToSpawn--;
//        currentEnemyIndex++;
//        StartCoroutine(SpawnEnemyInWave());
//    }
//    else
//    {
//        if (currentWaveIndex < waves.Length - 1)
//        {
//            currentWaveIndex++;
//            enemiesLeftToSpawn = waves[currentWaveIndex].WaveSettings.Length;
//            currentEnemyIndex = 0;
//        }
//    }
//}

//public void LaunchWave()
//{
//    StartCoroutine(SpawnEnemyInWave());
//}
//}

//[System.Serializable]
//public class Waves
//{
//    [SerializeField] private WaveSettings[] waveSettings;
//    public WaveSettings[] WaveSettings { get => waveSettings; }
//}

//[System.Serializable]
//public class WaveSettings
//{
//    [SerializeField] private GameObject enemy;
//    public GameObject Enemy { get => enemy; }

//    [SerializeField] private GameObject neededSpawner;
//    public GameObject NeededSpawner { get => neededSpawner; }

//    [SerializeField] private float spawnDelay;
//    public float SpawnDelay { get => spawnDelay; }
//}