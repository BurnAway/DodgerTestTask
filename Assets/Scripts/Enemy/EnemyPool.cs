using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    public List<IEnemy> ActiveEnemy { get; private set; }

    private int _poolSize;
    private EnemyFactory _enemyFactory;
    private Stack<IEnemy> _enemies;
    private Vector2[] _spawnPositions;
    private Rect _rectLocation;
    private GameObject _parentObject;

    public EnemyPool(Location location, EnemyConfig config, GameObject spawnPositions)
    {
        _enemyFactory = new EnemyFactory(location, config);
        _poolSize = config.PoolSize;
        _rectLocation = location.Rect;

        _spawnPositions = new Vector2[spawnPositions.transform.childCount];
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            Transform position = spawnPositions.transform.GetChild(i);
            _spawnPositions[i] = position.position;
        }
    }

    public void Initialize()
    {
        ActiveEnemy = new List<IEnemy>();
        _enemies = new Stack<IEnemy>(_poolSize);
        
        _parentObject = new GameObject("EnemyPool");

        for (int count = 0; count < _poolSize; ++count)
        {
            _enemies.Push(InstantiateEnemy());
        }
    }

    private IEnemy InstantiateEnemy()
    {
        IEnemy enemy = _enemyFactory.Create(_parentObject.transform);
        enemy.Dispose();
        return enemy;
    }

    public IEnemy TakeEnemy()
    {
        if (_enemies.Count <= 0)
        {
            _enemies.Push(InstantiateEnemy());
        }

        IEnemy enemy = _enemies.Pop();
        enemy.Reset(_spawnPositions[UnityEngine.Random.Range(0, 8)], GetRandomPointInArena());

        ActiveEnemy.Add(enemy);

        return enemy;
    }

    public void DisposeEnemy(IEnemy enemy)
    {
        if (enemy == null)
        {
            return;
        }

        enemy.Dispose();
        ActiveEnemy.Remove(enemy);

        _enemies.Push(enemy);
    }
    
    private Vector2 GetRandomPointInArena()
    {
        return new Vector2(UnityEngine.Random.Range(_rectLocation.min.x, _rectLocation.max.x), UnityEngine.Random.Range(_rectLocation.min.y, _rectLocation.max.y));
    }
}