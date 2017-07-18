using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
    void OnCollisionEnter(Collision2D collision);
}

public enum TypeState
{
    
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
    public virtual void OnCollisionEnter(Collision2D collision) { }
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
    
    public override void OnCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(LiteralConstant.EnemyTag))
        {
            Vector2 direction = (Vector2)Owner.Transform.position - collision.contacts.First().point;
            Owner.SetState(new BounceState(Owner, direction, this));
        }
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

    public override void OnCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(LiteralConstant.PlayerTag))
        {
            Owner.SetState(new InactiveState(Owner, this));
        }

        if (collision.gameObject.CompareTag(LiteralConstant.EnemyTag))
        {
            Vector2 direction = (Vector2)Owner.Transform.position - collision.contacts.First().point;
            Owner.SetState(new BounceState(Owner, direction, this));
        }
    }
}

/// <summary> Состояние бездействия после столкновения с игроком </summary>
public class InactiveState : EnemyState
{
    private float _inactiveTime;
    private EnemyState _prevState;

    public InactiveState(IEnemy owner, EnemyState prevState)
        : base(owner)
    {
        _inactiveTime = owner.InactiveTime;
        _prevState = prevState;

        Observable.FromCoroutine(Waiting).Subscribe();
    }
    
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(_inactiveTime);

        Owner.SetState(_prevState);
    }
    
    public override void OnCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(LiteralConstant.EnemyTag))
        {
            Vector2 direction = (Vector2)Owner.Transform.position - collision.contacts.First().point;
            Owner.SetState(new BounceState(Owner, direction, this));
        }
    }
}

/// <summary> Состояние отскока при столкновении друг с другом</summary>
public class BounceState : EnemyState
{
    private EnemyState _prevState;
    private Vector2 _direction;
    private Vector2 _startPosition;

    public BounceState(IEnemy owner, Vector2 direction, EnemyState prevState)
        : base(owner)
    {
        _direction = direction.normalized;
        Debug.Log("Bounce ctor");
        _prevState = prevState;
        _startPosition = Owner.Transform.position;
    }

    public override void Update()
    {
        Debug.Log(Vector2.Distance(Owner.Transform.position, _startPosition));
        Debug.Log(Owner.BounceDistance);
        if (Vector2.Distance(Owner.Transform.position, _startPosition) < Owner.BounceDistance)
        {
            Bounce(_direction);
            return;
        }

        Owner.SetState(_prevState);
    }

    /// <summary> Осуществляет отскок в заданном направлении </summary>
    private void Bounce(Vector2 direction)
    {
        // Скорость при отскоке увеличивается
        float bounceSpeed = Owner.Speed * 3;

        float positionX = Owner.Transform.position.x + direction.x * bounceSpeed * Time.deltaTime;
        float positionY = Owner.Transform.position.y + direction.y * bounceSpeed * Time.deltaTime;

        Owner.Transform.position = new Vector2(positionX, positionY);
    }
}