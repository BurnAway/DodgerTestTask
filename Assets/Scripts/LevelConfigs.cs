using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Настройки уровня </summary>
public class LevelConfig
{
    public static float WorldScreenHeight;
    public static float WorldScreenWidth;

    public PlayerConfig PlayerConfig;
    public LocationConfig LocationConfig;
    public CameraConfig CameraConfig;
    public EnemyConfig EnemyConfig;
    public ProjectileConfig ProjectileConfig;

    public LevelConfig()
    {
        WorldScreenHeight = Camera.main.orthographicSize * 2f;
        WorldScreenWidth = WorldScreenHeight / Screen.height * Screen.width;

        PlayerConfig = new PlayerConfig();
        PlayerConfig.Scale = WorldScreenWidth * PlayerConfig.Scale;
        PlayerConfig.Speed = WorldScreenWidth * PlayerConfig.Speed;

        LocationConfig = new LocationConfig();
        LocationConfig.Height = WorldScreenHeight * 2 * LocationConfig.Height;
        LocationConfig.Width = WorldScreenWidth * 2 * LocationConfig.Width;

        CameraConfig = new CameraConfig();
        CameraConfig.BorderMove = 1 - 2 * CameraConfig.BorderMove;

        EnemyConfig = new EnemyConfig();
        EnemyConfig.Scale = WorldScreenWidth * EnemyConfig.Scale;
        EnemyConfig.Speed = WorldScreenWidth * EnemyConfig.Speed;
        EnemyConfig.AggroRadius = WorldScreenWidth * EnemyConfig.AggroRadius;
        EnemyConfig.BounceDistance = WorldScreenWidth * EnemyConfig.BounceDistance;

        ProjectileConfig = new ProjectileConfig();
        ProjectileConfig.Width = WorldScreenWidth * ProjectileConfig.Width;
        ProjectileConfig.Height = WorldScreenWidth * ProjectileConfig.Height;
        ProjectileConfig.Speed = WorldScreenWidth * ProjectileConfig.Speed;
    }
}

/// <summary> Настройки игрока </summary>
public class PlayerConfig
{
    public float Scale = 0.05f;
    public float Speed = 0.1f;
    public int HealthPoint = 3;
    public float FireDelay = 0.5f;
    public float ImmuneTime = 2f;
}

/// <summary> Настройки локации </summary>
public class LocationConfig
{
    public float Width = 1;
    public float Height = 1;
    public float EnemySpawnDelay = 4;
}

/// <summary> Настройки камеры </summary>
public class CameraConfig
{
    public float BorderMove = 0.25f;
}

/// <summary> Настройки противника </summary>
public class EnemyConfig
{
    public float Scale = 0.05f;
    public float Speed = 0.1f;
    public float AggroRadius = 0.2f;
    public float BounceDistance = 0.06f;
    public float InactiveTime = 3f;
    public float DeathTime = 1f;
    public int PoolSize = 10;
}

/// <summary> Настройки снарядов </summary>
public class ProjectileConfig
{
    public float Width = 0.01f;
    public float Height = 0.05f;
    public float Speed = 0.2f;
    public float LifeTime = 6f;
    public int PoolSize = 10;
}