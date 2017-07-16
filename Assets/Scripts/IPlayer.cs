using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    Vector2 Position { get; }
    float Speed { get; }
    int HealthPoint { get; }
    PlayerView View { get; }

    void Initialize(PlayerConfig playerConfig);
    void Update(float deltaTime);
}