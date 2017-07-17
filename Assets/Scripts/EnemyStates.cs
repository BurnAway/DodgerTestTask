using System;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}

public abstract class EnemyState : IEnemyState
{
    protected IEnemy Owner;

    protected EnemyState(IEnemy owner)
    {
        Owner = owner;
    }

#region IEnemyState
    public virtual void Enter(){}

    public virtual void Update() { }

    public virtual void Exit() { }
#endregion

    /// <summary> Передвигает в указанном направлении </summary>
    protected void Move(Vector2 direction)
    {
        float positionX = Owner.Transform.position.x + direction.x * Owner.Speed * Time.deltaTime;
        float positionY = Owner.Transform.position.y + direction.y * Owner.Speed * Time.deltaTime;

        Owner.Transform.position = new Vector2(positionX, positionY);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Owner.Transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

/// <summary> Состояние движения </summary>
public class WalkState : EnemyState
{
    private Vector2 _direction;

    public WalkState(Vector2 direction, IEnemy owner)
        : base(owner)
    {
        _direction = direction.normalized;
    }

    public override void Update()
    {
        if (Vector2.Distance(Owner.Transform.position, Owner.Location.Player.View.transform.position) <=
            Owner.AggroRadius)
        {
            Owner.SetState(new HuntState(Owner.Location.Player, Owner));
        }

        Move(_direction);
    }
}

/// <summary> Состояние преследования игрока </summary>
public class HuntState : EnemyState
{
    private readonly ITargetable _target;

    public HuntState(ITargetable target, IEnemy owner) : base(owner)
    {
        _target = target;
    }
    
    public override void Update()
    {
        Vector2 direction = _target.Position - (Vector2)(Owner.Transform.position);
        direction = direction.normalized;

        Move(direction);
    }
}

/// <summary> Состояние бездействия после столкновения с игроком </summary>
public class InactiveState : EnemyState
{
    private float _inactiveTime;
    private IEnemyState _prevState;

    public InactiveState(IEnemy owner, IEnemyState prevState)
        : base(owner)
    {
        _inactiveTime = owner.InactiveTime;
        _prevState = prevState;
    }
}

/// <summary> Состояние отскока при столкновении друг с другом</summary>
public class BounceState : EnemyState
{
    private readonly IEnemyState _prevState;
    private readonly Vector2 _direction;
    private readonly Vector2 _startPosition;

    public BounceState(IEnemy owner, Vector2 direction, IEnemyState prevState)
        : base(owner)
    {
        _direction = direction.normalized;
        _prevState = prevState;
        _startPosition = Owner.Transform.position;
    }

}