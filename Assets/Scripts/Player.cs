using System;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

class Player : IPlayer
{
    private PlayerView _playerView;

    public Player()
    {
    }

    public void Initialize(PlayerConfig playerConfig)
    {
        Speed = playerConfig.Speed;
        HealthPoint = playerConfig.HealthPoint;
        Position = Vector2.zero;

        _playerView = new PlayerView(this, playerConfig);
    }

    #region IPlayer
    public Vector2 Position { get; private set; }
    public float Speed { get; private set; }
    public int HealthPoint { get; private set; }
    public void Update(float deltaTime)
    {
        if (InputController.AcceptInput)
        {
            _playerView.MoveTo();
        }
    }
#endregion
}