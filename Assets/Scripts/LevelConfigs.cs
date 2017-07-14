﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Настройки игрока </summary>
public class PlayerConfig
{
    public float Scale = 0.05f;
    public float Speed = 0.1f;
    public int HealthPoint = 3;
}

/// <summary> Настройки уровня </summary>
public class LevelConfig
{
    public static float WorldScreenHeight;
    public static float WorldScreenWidth;

    public PlayerConfig PlayerConfig;

    public LevelConfig()
    {
        WorldScreenHeight = Camera.main.orthographicSize * 2f;
        WorldScreenWidth = WorldScreenHeight / Screen.height * Screen.width;

        PlayerConfig = new PlayerConfig();
        PlayerConfig.Scale = WorldScreenWidth * PlayerConfig.Scale;
        PlayerConfig.Speed = WorldScreenWidth * PlayerConfig.Speed;
    }
}