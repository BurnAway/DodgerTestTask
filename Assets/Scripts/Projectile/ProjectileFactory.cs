using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory
{
    private ProjectileConfig _config;

    public ProjectileFactory(ProjectileConfig config)
    {
        _config = config;
    }

    public IProjectile Create(ProjectilePool pool, Transform parent)
    {
        GameObject projectileObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Projectile")) as GameObject;
        projectileObject.transform.SetParent(parent);

        IProjectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Initialize(_config, pool);

        return projectile;
    }
}