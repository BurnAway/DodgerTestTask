using System;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public class Player : IPlayer
{
    public PlayerView View { get; private set; }

    public Player()
    {
    }

    public void Initialize(PlayerConfig playerConfig)
    {
        Speed = playerConfig.Speed;
        HealthPoint = playerConfig.HealthPoint;
        Position = Vector2.zero;

        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Player")) as GameObject;

        View = gameObject.GetComponent<PlayerView>();
        View.Initialize(this);

        float heightInPixels = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        gameObject.transform.position = Position;
        gameObject.transform.localScale = new Vector3(playerConfig.Scale / heightInPixels, playerConfig.Scale / heightInPixels, 1);
    }

    #region IPlayer
    public Vector2 Position { get; private set; }
    public float Speed { get; private set; }
    public int HealthPoint { get; private set; }
    public void Update(float deltaTime)
    {
        if (InputController.AcceptInput)
        {
            View.MoveTo();
        }
    }
#endregion
}