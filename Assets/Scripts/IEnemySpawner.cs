namespace Assets.Scripts
{
    public interface IEnemySpawner
    {
        int EnemySpawned { get; }

        void SpawnEnemy();
    }
}
