
using UnityEngine;

public class EnemyFactory
{
    private Location _location;
    private EnemyConfig _config;

    public EnemyFactory(Location location, EnemyConfig config)
    {
        _location = location;
        _config = config;
    }

    public IEnemy Create(Transform parent)
    {
        IEnemy enemy = new Enemy(_location);

        enemy.Initialize(_config);
        enemy.View.transform.SetParent(parent);

        return enemy;
    }
}