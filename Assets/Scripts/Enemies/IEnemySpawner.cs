namespace Assets.Scripts.Enemies
{
    public interface IEnemySpawner
    {
        int EnemySpawned { get; }

        void SpawnEnemy();
    }
}
