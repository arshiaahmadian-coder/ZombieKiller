using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] int spawnNumber;
    [SerializeField] GameObject fatherZombiePrefab;
    [SerializeField] GameObject sonZombiePrefab;
    [SerializeField] TMP_Text waveText;
    [SerializeField] float changeToNightInterval;
    [SerializeField] float spawnInterval;

    private bool isDay = true;
    private bool canSpawn = true;
    private bool changeToNightState = false;
    private int _waveNumber = 0;
    private int waveNumber = 0;
    private float timer;

    private void Start()
    {
        waveText.text = waveNumber.ToString();
        FindAllAliveZombies();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            canSpawn = true;
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
        int _spawnNumber = waveManager();

        for(int i = 0; i < _spawnNumber; i++)
        {
            do {
                index = randomIndex(spawnPoints.Length);
            } while (index == lastIndex);
            lastIndex = index;
            Instantiate(fatherZombiePrefab, spawnPoints[index]);
        }

        if(_spawnNumber % 2 == 0)
        {
            do {
                index = randomIndex(spawnPoints.Length);
            } while (index == lastIndex);
            Instantiate(sonZombiePrefab, spawnPoints[index]);
        }
    }

    private int waveManager()
    {
        if (isDay)
        {
            if (++_waveNumber >= 2)
            {
                // change to night
                waveNumber++;
                spawnNumber++;
                _waveNumber = 0;
                waveText.text = waveNumber.ToString();
                isDay = !isDay;
                canSpawn = true;
                changeToNightState = true;
                return spawnNumber;
            } else
            {
                // return day value
                return (int)spawnNumber / 2;
            }
        } else {
            if (++_waveNumber >= 3)
            {
                // change to day
                DayNightManager.instance.ChangeToDay();
                waveNumber++;
                spawnNumber++;
                _waveNumber = 0;
                waveText.text = waveNumber.ToString();
                isDay = !isDay;
                canSpawn = true;
                return (int)spawnNumber / 2;
            } else
            {
                // return night value
                return spawnNumber;
            }
        }
    }

    public void FindAllAliveZombies()
    {
        ZombieHealth[] aliveZombies = FindObjectsByType<ZombieHealth>(FindObjectsSortMode.None);

        if(aliveZombies.Length - 1 <= 0 && canSpawn && changeToNightState == false)
        {
            SpawnZombies();
            canSpawn = false;
        }

        if(aliveZombies.Length - 1 <= 0 && canSpawn && changeToNightState == true)
        {
            Invoke(nameof(ChangeToNight), changeToNightInterval);
        }
    }

    private void ChangeToNight()
    {
        DayNightManager.instance.ChangeToNight();
        changeToNightState = false;
        FindAllAliveZombies();
    }
}
