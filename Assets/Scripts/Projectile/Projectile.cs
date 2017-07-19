using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public interface IProjectile
{
    float Speed { get; }
    float Width { get; }
    float Height { get; }
    float LifeTime { get; }

    void Initialize(ProjectileConfig config, ProjectilePool pool);
    void Reset(Vector2 position, Vector2 direction);
    void Dispose();
}

public class Projectile : MonoBehaviour, IProjectile
{
    private ProjectileConfig _config;
    private Vector2 _direction;
    private ProjectilePool _pool;

    public void Initialize(ProjectileConfig config, ProjectilePool pool)
    {
        Speed = config.Speed;
        Width = config.Width;
        Height = config.Height;
        LifeTime = config.LifeTime;

        _config = config;
        _pool = pool;

        float ratioScaleH = _config.Height / GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        float ratioScaleW = _config.Width / GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        transform.localScale = new Vector3(ratioScaleW, ratioScaleH, 1);
    }

    private IEnumerator EndOfLifeProjectile()
    {
        yield return new WaitForSeconds(LifeTime);

        _pool.DisposeProjectile(this);
    }

    public void Update()
    {
        MoveTo(_direction);
    }

    public float Speed { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float LifeTime { get; private set; }

    public void Reset(Vector2 position, Vector2 direction)
    {
        transform.position = position + direction * _config.Height / 2;
        _direction = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.SetActive(true);

        Observable.FromCoroutine(EndOfLifeProjectile).Subscribe();
    }

    public void Dispose()
    {
        gameObject.SetActive(false);
    }

    private void MoveTo(Vector2 direction)
    {
        float positionX = transform.position.x + direction.x * _config.Speed * Time.deltaTime;
        float positionY = transform.position.y + direction.y * _config.Speed * Time.deltaTime;

        transform.position = new Vector2(positionX, positionY);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _pool.DisposeProjectile(this);
    }
}