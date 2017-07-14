using System;
using System.Collections.Generic;
using UnityEngine;

class Player : MonoBehaviour, IPlayer
{
    private float _speed;
    private int _healthPoint;
    public GameObject _view;

    public Player(PlayerConfig playerConfig)
    {
        _speed = playerConfig.Speed;
        _healthPoint = playerConfig.HealthPoint;

        _view = Instantiate(Resources.Load("Prefabs/Player")) as GameObject;
        float heightInPixels = _view.GetComponent<SpriteRenderer>().bounds.size.y;
        _view.transform.localScale = new Vector3(LevelConfig.WorldScreenWidth * playerConfig.Scale / heightInPixels, LevelConfig.WorldScreenWidth * playerConfig.Scale / heightInPixels);
    }

    private void Update()
    {
        
    }

#region IPlayer
    public Vector2 Position { get; private set; }
    public void Update(float deltaTime)
    {
    }
#endregion
}