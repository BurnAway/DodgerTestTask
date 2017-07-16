using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    private LevelConfig _levelConfig;
    private float _width;
    private float _height;

    private IPlayer _player;
    
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _levelConfig = new LevelConfig();

        InitializeLocation();

        _player = new Player();
        _player.Initialize(_levelConfig.PlayerConfig);
    }

    private void InitializeLocation()
    {
        _width = _levelConfig.LocationConfig.Width;
        _height = _levelConfig.LocationConfig.Height;

        Bounds bounds = GetComponent<SpriteRenderer>().sprite.bounds;
        float ratioScaleH = _height / bounds.size.y;
        float ratioScaleW = _width / bounds.size.x;
        transform.localScale = new Vector3(ratioScaleW, ratioScaleH, 1);
    }

    void Update()
    {
        _player.Update(Time.deltaTime);
    }
}
