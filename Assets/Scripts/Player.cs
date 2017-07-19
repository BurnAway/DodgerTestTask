using System.Collections;
using DG.Tweening;
using UniRx;
using UnityEngine;

public interface ITargetable
{
    Vector2 Position { get; }
}

public interface IPlayer
{
    float Speed { get; }
    int HealthPoint { get; }
    PlayerView View { get; }

    void Initialize(PlayerConfig playerConfig, ProjectileConfig projectileConfig);
    void StartFire();
    void EndFire();
}

public class Player : IPlayer, ITargetable
{
    public bool HasFire;

    private float _fireВelay;
    private ProjectilePool _projectilePool;
    private bool _isImmunable;
    private float _immuneTime;

#region IPlayer
    public PlayerView View { get; private set; }
    public float Speed { get; private set; }
    public int HealthPoint { get; private set; }

    public void Initialize(PlayerConfig playerConfig, ProjectileConfig projectileConfig)
    {
        Speed = playerConfig.Speed;
        HealthPoint = playerConfig.HealthPoint;
        _immuneTime = playerConfig.ImmuneTime;
        _fireВelay = playerConfig.FireDelay;

        GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/Player")) as GameObject;

        View = gameObject.GetComponent<PlayerView>();
        View.Initialize(this);

        float heightInPixels = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        gameObject.transform.localScale = new Vector3(playerConfig.Scale / heightInPixels, playerConfig.Scale / heightInPixels, 1);

        _projectilePool = new ProjectilePool(projectileConfig, this, playerConfig.Scale);
        _projectilePool.Initialize();
    }

    /// <summary> Нанесение урона игроку </summary>
    public void ApplyDamage()
    {
        if (_isImmunable)
        {
            return;
        }

        HealthPoint--;

        if (HealthPoint <= 0)
        {
            Die();
            return;
        }

        EventManager.OnPlayerHit();

        Observable.FromCoroutine(SetImmunable).Subscribe();
    }

    /// <summary> Делает игрока неуязвимым после получения урона </summary>
    private IEnumerator SetImmunable()
    {
        _isImmunable = true;

        View.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.2f)
            .SetLoops(Mathf.CeilToInt(_immuneTime / 0.2f))
            .OnComplete(() => View.GetComponent<SpriteRenderer>().color = Color.white);

        yield return new WaitForSeconds(_immuneTime);

        _isImmunable = false;
        DOTween.Kill(View.GetComponent<SpriteRenderer>(), true);
    }

    private void Die()
    {
        HasFire = false;
        EventManager.OnPlayerDied();
        View.transform.DOScale(Vector3.zero, 0.5f).Play();
    }
    
    public void StartFire()
    {
        HasFire = true;
        Observable.FromCoroutine(SpawnProjectile).Subscribe();
    }

    public void EndFire()
    {
        HasFire = false;
    }

    private IEnumerator SpawnProjectile()
    {
        while (true)
        {
            if (!HasFire)
            {
                yield break;
            }

            _projectilePool.TakeProjectile();
            yield return new WaitForSeconds(_fireВelay);
        }
    }

#endregion

#region ITargetable
    public Vector2 Position { get { return View.transform.position; } }
#endregion
}