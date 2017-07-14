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
        Direction = Quaternion.AngleAxis(0, Vector3.zero);

        _playerView = new PlayerView(this, playerConfig);
    }

    private void UpdateInput()
    {
        float dx = CnInputManager.GetAxis("Horizontal");
        float dy = CnInputManager.GetAxis("Vertical");

        Vector2 direction = new Vector2(dx, dy).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        Direction = Quaternion.AngleAxis(angle, Vector3.forward);
        Position = new Vector2(Position.x + direction.x * Speed * Time.deltaTime, Position.y + direction.y * Speed * Time.deltaTime);
    }

    #region IPlayer
    public Vector2 Position { get; private set; }
    public Quaternion Direction { get; private set; }
    public float Speed { get; private set; }
    public int HealthPoint { get; private set; }
    public void Update(float deltaTime)
    {
        if (InputController.AcceptInput)
        {
            UpdateInput();
            _playerView.MoveTo();
        }

        _playerView.Update();
    }
#endregion
}