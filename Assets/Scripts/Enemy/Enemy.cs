using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    Location Location { get; }
    float Speed { get; }
    float AggroRadius { get; }
    float DeathTime { get; }
    EnemyState State { get; }
    EnemyView View { get; }
    float Scale { get; }
    float InactiveTime { get; }
    float BounceDistance { get; }
    Transform Transform { get; }

    void Initialize(EnemyConfig playerConfig);
    void SetState(EnemyState state);
    void Dispose();
    void Reset(Vector2 position, Vector2 point);
    void Tick();
}

public class Enemy : IEnemy
{
    private GameObject _enemyObject;
    private bool _isActive;

    public Enemy(Location location)
    {
        Location = location;
    }

#region IEnemy

    public Location Location { get; private set; }
    public float Speed { get; private set; }
    public float AggroRadius { get; private set; }
    public float DeathTime { get; private set; }
    public EnemyState State { get; private set; }
    public EnemyView View { get; private set; }
    public float Scale { get; private set; }
    public float InactiveTime { get; private set; }
    public float BounceDistance { get; private set; }
    public Transform Transform { get { return _enemyObject.transform; } }

    public void Initialize(EnemyConfig enemyConfig)
    {
        Speed = enemyConfig.Speed;
        AggroRadius = enemyConfig.AggroRadius;
        InactiveTime = enemyConfig.InactiveTime;
        BounceDistance = enemyConfig.BounceDistance;
        DeathTime = enemyConfig.DeathTime;

        _enemyObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Enemy")) as GameObject;

        View = _enemyObject.GetComponent<EnemyView>();
        View.Initialize(this);

        float heightInPixels = _enemyObject.GetComponent<SpriteRenderer>().bounds.size.y;
        Scale = enemyConfig.Scale / heightInPixels;

        _enemyObject.transform.localScale = new Vector3(Scale, Scale, 1);

        SetState(new WalkState(Vector2.up, this));
    }

    /// <summary> Назначает состояние противникуы </summary>
    public void SetState(EnemyState state)
    {
        if (State != null)
        {
            State.Exit();
        }

        State = state;
        State.Enter();
    }

    /// <summary> Отключение противника </summary>
    public void Dispose()
    {
        _isActive = false;
        Transform.position = Vector3.zero;
        _enemyObject.SetActive(false);
    }
    
    /// <summary> Сброс параметров противника </summary>
    public void Reset(Vector2 position, Vector2 point)
    {
        _isActive = true;
        _enemyObject.transform.localScale = new Vector3(Scale, Scale, 1);
        Transform.position = position;

        Vector2 direction = (point - position).normalized;
        State = new WalkState(direction, this);
        
        _enemyObject.SetActive(true);
    }

    public void Tick()
    {
        if (!_isActive)
        {
            return;
        }

        State.Update();
    }
#endregion
}