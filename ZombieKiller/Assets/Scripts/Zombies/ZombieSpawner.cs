using JetBrains.Annotations;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int spawnNumber;
    [SerializeField] float spawnInterval;
    [SerializeField] GameObject fatherZombiePrefab;
    [SerializeField] GameObject sonZombiePrefab;

    private float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            SpawnZombies();
            timer = spawnInterval;
        }
    }

    private int randomIndex(int maxRange)
    {
        return Random.Range(0, maxRange);;
    }

    private void SpawnZombies()
    {
        int lastIndex = -1;
        int index;

        for(int i = 0; i < spawnNumber; i++)
        {
            do {
                index = randomIndex(spawnPoints.Length);
            } while (index == lastIndex);
            lastIndex = index;
            Instantiate(fatherZombiePrefab, spawnPoints[index]);
        }

        if(spawnNumber % 2 == 0)
        {
            do {
                index = randomIndex(spawnPoints.Length);
            } while (index == lastIndex);
            Instantiate(sonZombiePrefab, spawnPoints[index]);
        }
        
        spawnNumber++;
    }
}
