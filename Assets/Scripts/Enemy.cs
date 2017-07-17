using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Location Location { get; }
    float Speed { get; }
    float AggroRadius { get; }
    EnemyState State { get; }
    float InactiveTime { get; }
    Transform Transform { get; }

    void Initialize(EnemyConfig playerConfig);
    void SetState(EnemyState state);
    void Update(float deltaTime);
}

public class Enemy : IEnemy
{
    private GameObject _enemyObject;

    public Enemy(Location location)
    {
        Location = location;
    }

    #region IEnemy
    public Location Location { get; private set; }
    public float Speed { get; private set; }
    public float AggroRadius { get; private set; }
    public EnemyState State { get; private set; }
    public float InactiveTime { get; private set; }
    public Transform Transform { get { return _enemyObject.transform; } }

    public void Initialize(EnemyConfig enemyConfig)
    {
        Speed = enemyConfig.Speed;
        AggroRadius = enemyConfig.AggroRadius;
        InactiveTime = enemyConfig.InactiveTime;

        _enemyObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Enemy")) as GameObject;

        float heightInPixels = _enemyObject.GetComponent<SpriteRenderer>().bounds.size.y;
        _enemyObject.transform.localScale = new Vector3(enemyConfig.Scale / heightInPixels, enemyConfig.Scale / heightInPixels, 1);

        SetState(new WalkState(Vector2.up, this));
        
    }

    public void SetState(EnemyState state)
    {
        if (State != null)
        {
            State.Exit();
        }

        State = state;
        State.Enter();
    }

    public void Update(float deltaTime)
    {
        State.Update();
    }
#endregion
}