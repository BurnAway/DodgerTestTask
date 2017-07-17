using System;
using System.Collections.Generic;
using CnControls;
using UnityEngine;

public interface IPlayer
{
    float Speed { get; }
    int HealthPoint { get; }
    PlayerView View { get; }

    void Initialize(PlayerConfig playerConfig);
    void Update(float deltaTime);
}

public class Player : IPlayer, ITargetable
{
    public PlayerView View { get; private set; }
    
    public void Initialize(PlayerConfig playerConfig)
    {
        Speed = playerConfig.Speed;
        HealthPoint = playerConfig.HealthPoint;

        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Player")) as GameObject;

        View = gameObject.GetComponent<PlayerView>();
        View.Initialize(this);

        float heightInPixels = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        gameObject.transform.localScale = new Vector3(playerConfig.Scale / heightInPixels, playerConfig.Scale / heightInPixels, 1);
    }

#region IPlayer
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

#region ITargetable
    public Vector2 Position { get { return View.transform.position; }}

    #endregion
}