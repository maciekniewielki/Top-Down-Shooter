using System.Collections.Generic;

public class Wave
{
    public int waveNumber;

    private Queue<EnemyType> enemiesToSpawn;

    public Wave(int waveNumber)
    {
        this.waveNumber = waveNumber;
        enemiesToSpawn = new Queue<EnemyType>();
    }

    public void AddEnemyToWave(EnemyType enemyToAdd, int count=1)
    {
        for (int ii = 0; ii < count; ii++)
            enemiesToSpawn.Enqueue(enemyToAdd);
    }

    public bool IsWaveEmpty()
    {
        return enemiesToSpawn.Count == 0;
    }

    public int GetCurrentCapacity()
    {
        return enemiesToSpawn.Count;
    }

    public EnemyType GetNewEnemy()
    {
        return enemiesToSpawn.Dequeue();
    }
}
