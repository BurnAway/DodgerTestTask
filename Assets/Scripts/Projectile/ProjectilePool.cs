using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool
{
    private float _size;
    private Transform _projectilePosition;
    private int _poolSize;
    private ProjectileConfig _config;
    private ProjectileFactory _projectileFactory;
    private Stack<IProjectile> _projectiles;
    private GameObject _parentObject;

    public ProjectilePool(ProjectileConfig config, IPlayer player, float size)
    {
        _size = size;
        _projectilePosition = player.View.transform;
        _poolSize = config.PoolSize;
        _projectileFactory = new ProjectileFactory(config);
    }

    public void Initialize()
    {
        _projectiles = new Stack<IProjectile>(_poolSize);

        _parentObject = new GameObject("ProjectilePool");

        for (int count = 0; count < _poolSize; ++count)
        {
            _projectiles.Push(InstantiateProjectile());
        }
    }

    private IProjectile InstantiateProjectile()
    {
        IProjectile projectile = _projectileFactory.Create(this, _parentObject.transform);
        projectile.Dispose();
        return projectile;
    }

    public IProjectile TakeProjectile()
    {
        if (_projectiles.Count <= 0)
        {
            _projectiles.Push(InstantiateProjectile());
        }

        IProjectile projectile = _projectiles.Pop();
        projectile.Reset(_projectilePosition.position + _projectilePosition.up * _size / 2, _projectilePosition.up);
        return projectile;
    }

    public void DisposeProjectile(IProjectile projectile)
    {
        if (projectile == null)
        {
            return;
        }

        projectile.Dispose();

        _projectiles.Push(projectile);
    }
}