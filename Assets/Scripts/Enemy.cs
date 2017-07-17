using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    float Speed { get; }
    EnemyState State { get; }

    void Initialize(EnemyConfig playerConfig);
    void Update(float deltaTime);
}

public class Enemy : IEnemy
{
    private GameObject _enemyObject;
    private float _aggroRadius;
    private float _sleepTime;


#region IEnemy
    public float Speed { get; private set; }
    public EnemyState State { get; private set; }

    public void Initialize(EnemyConfig enemyConfig)
    {
        Speed = enemyConfig.Speed;
        _aggroRadius = enemyConfig.AggroRadius;
        _sleepTime = enemyConfig.SleepTime;

        _enemyObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Enemy")) as GameObject;
        
        float heightInPixels = _enemyObject.GetComponent<SpriteRenderer>().bounds.size.y;
        _enemyObject.transform.localScale = new Vector3(enemyConfig.Scale / heightInPixels, enemyConfig.Scale / heightInPixels, 1);
    }

    public void Update(float deltaTime)
    {
        throw new NotImplementedException();
    }
#endregion
}