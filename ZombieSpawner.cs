using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ZombieSpawner : MonoBehaviour 
{
    public Bounds spawnArea;
    public Enemy normalEnemyPrefab;
    public Enemy upgradedEnemyPrefab;
    public float timeBetweenSpawns;
    public float timeBetweenWaves;
    public float minPlayerDistance;
    public int maxSpawnedEnemies;
    public float upgradedEnemySpawnChance;
    public Queue<Wave> waves;
    public System.Action<int> waveTimerTick;
    public System.Action<int> beginningNextWave;
    public System.Action<int> endOfWave;

    private Wave currentWave;
    private bool isSpawning;

	void Start () 
	{
        SetUpWaves();
        Invoke("BeginNextWave", 1f);
	}
	
	void Update () 
	{
	
	}

    Vector2 GetRandomSpawnPosition()
    {
        Vector3 min = spawnArea.min;
        Vector3 max = spawnArea.max;
        return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }

    bool CanSpawn()
    {
        return Game.state == GameState.IN_PROGRESS && Player.instance != null;
    }

    Enemy TrySpawn(Enemy enemyToSpawn)
    {
        Vector2 pos = GetRandomSpawnPosition();
        if (Vector2.Distance(pos, (Vector2)Player.instance.transform.position) > minPlayerDistance)
            return Spawn(pos, enemyToSpawn);
        else
            return TrySpawn(enemyToSpawn);
    }

    Enemy Spawn(Vector2 pos, Enemy enemyToSpawn)
    {
        return Instantiate(enemyToSpawn, pos, Quaternion.identity) as Enemy;
    }

    void BeginNextWave()
    {
        currentWave = waves.Dequeue();
        if (currentWave != null)
        {
            StartCoroutine("SpawnWave", currentWave);
            if(beginningNextWave!=null)
                beginningNextWave(currentWave.waveNumber);
        }

        
    }

    void SetUpWaves()
    {
        waves = new Queue<Wave>();
        for (int ii = 1; ii < 20; ii++)
        {
            int normalCount = 25 + (int)(15*Mathf.Sqrt(ii));
            int redCount = ii / 2;
            Wave wave = new Wave(ii);
            wave.AddEnemyToWave(EnemyType.NORMAL_ZOMBIE, normalCount);
            wave.AddEnemyToWave(EnemyType.RED_ZOMBIE, redCount);
            waves.Enqueue(wave);
        }
        
    }

    void RandomizeEnemy(Enemy enemy, float min, float max)
    {
        float size = Random.Range(min, max);
        enemy.damage *= size;
        enemy.health *= size;
        enemy.transform.localScale *= Mathf.Sqrt(size);
        enemy.linearMovementSpeed /= Mathf.Sqrt(size);

        enemy.stopMovingDistance *= size;
        enemy.scoreForKilling = (int)(enemy.scoreForKilling * size);
    }

    void BuffEnemy(Enemy enemy, float buffAmount)
    {
        if (buffAmount <= 1.0f)
            return;
        float randAmount = Random.Range(1f, buffAmount);
        enemy.damage *= randAmount;
        enemy.health *= randAmount;
    }

    IEnumerator BetweenWaveTime(int time)
    {
        while (Enemy.count > 0 && !isSpawning)
            yield return new WaitForSeconds(1f);
        if (endOfWave != null)
            endOfWave(currentWave.waveNumber);

        while (time > 0)
        {
            if (waveTimerTick != null)
                waveTimerTick(time);
            time--;
            yield return new WaitForSeconds(1f);
        }

        BeginNextWave();
    }

    IEnumerator SpawnWave(Wave waveToSpawn)
    {
        isSpawning = true;
        while (!waveToSpawn.IsWaveEmpty())
        {
            if (!CanSpawn())
            {
                yield return new WaitForSeconds(1f);
                StartCoroutine("SpawnWave", waveToSpawn);
                yield break;
            }
            EnemyType enemyType = waveToSpawn.GetNewEnemy();
            Enemy spawned;
            if (enemyType == EnemyType.NORMAL_ZOMBIE)
            {
                spawned = TrySpawn(normalEnemyPrefab);
                RandomizeEnemy(spawned, 0.5f, 2f);
            }
            else
                spawned= TrySpawn(upgradedEnemyPrefab);

            BuffEnemy(spawned, Mathf.Log(currentWave.waveNumber, 2));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        isSpawning = false;
        StartCoroutine("BetweenWaveTime", timeBetweenWaves);
    }
}

